using UnityEngine;

using Framework.Animation;
using Framework.Extensions;

namespace Player
{
    public sealed class Movement : MonoBehaviour
    {
        [SerializeField] private AnimationController animationController;
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 3;

        private bool _isWalking;
        private bool _wasWalking;
        
        private void Awake()
        {
            if (!rigidbody2D)
                rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            switch (_isWalking)
            {
                case true 
                when !_wasWalking:
                    _wasWalking = true;
                    animationController.PlayAnimation("Walk");
                    break;
                case false 
                     when _wasWalking:
                    _wasWalking = false;
                    animationController.PlayAnimation("Idle");
                    break;
            }
        }

        public void Move(Vector2 input)
        {
            input.y = 0;
            _isWalking = input.x != 0;
            
            if (_isWalking)
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