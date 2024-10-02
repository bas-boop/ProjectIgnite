using UnityEngine;

namespace Player
{
    public sealed class Shoot : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private float shootPower = 4;

        public void ShootBullet(Vector2 shootPos)
        {
            Vector2 shootDirection = (shootPos - (Vector2)transform.position).normalized;
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            
            newBullet.GetComponent<Rigidbody2D>().velocity = shootDirection * shootPower;
        }
    }
}