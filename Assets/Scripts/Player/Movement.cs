using UnityEngine;

using Framework.Extensions;

namespace Player
{
    public sealed class Movement : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 3;

        private void Awake()
        {
            if (!rigidbody2D)
                rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 input)
        {
            if (input.x != 0)
            {
                float scaleX = input.x > 0 ? 1 : -1;
                transform.localScale = new (scaleX, transform.localScale.y, transform.localScale.z);
            }
            
            float speedDirection = speed * input.x;
            Vector2 targetVelocity = rigidbody2D.velocity;
            
            targetVelocity.SetX(speedDirection);
            rigidbody2D.velocity = targetVelocity;
        }
    }
}