using Player;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private Shoot shoot;
    
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask layerMask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
            DetectObjects();
    }

    private void DetectObjects()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (Collider2D hitCollider in hitColliders)
        {
            shoot.ShootBullet(transform.position);
            print("Hit: " + hitCollider.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}