using UnityEngine;

using Framework.Extensions;

namespace Player
{
    public sealed class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 3;

        private void Awake()
        {
            if (!rigidbody2D)
                rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float input = Input.GetAxisRaw("Horizontal");

            if (input != 0)
            {
                Vector2 targetScale = Vector2.one;
                targetScale.x = input;
                transform.localScale = targetScale;
            }
            
            float yesSpeed = speed * input;
            
            Vector2 targetVelocity = rigidbody2D.velocity;
            targetVelocity.SetX(yesSpeed);
            rigidbody2D.velocity = targetVelocity;
        }
    }
}