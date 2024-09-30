using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask layerMask; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectObjects();
        }
    }

    void DetectObjects()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (Collider2D hitCollider in hitColliders)
        {
            print("Hit: " + hitCollider.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}