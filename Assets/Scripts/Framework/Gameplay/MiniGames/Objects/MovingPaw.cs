using UnityEngine;

namespace Framework.Gameplay.MiniGames.Objects
{
    public sealed class MovingPaw : MonoBehaviour
    {
        [SerializeField] private Vector2 direction = Vector2.one;

        [SerializeField, Range(0, 5)] private float forwardStep = 0.5f;
        [SerializeField, Range(0, 5)] private float backwardsSpeed = 0.5f;
        [SerializeField, Range(-1, -15)] private float backwardLimit = -5f;
        
        private Vector3 _initialPosition;
        private bool _canMove = true;

        private void Start() => _initialPosition = transform.position;

        private void Update() => MoveSlowlyBack();

        public void Stop() => _canMove = false;
        
        public void MoveForward()
        {
            if (!_canMove)
                return;
            
            Vector3 stepForwardPosition = transform.position + (Vector3)(direction.normalized * forwardStep);
            transform.position = stepForwardPosition;
        }

        private void MoveSlowlyBack()
        {
            Vector3 backwardPosition = transform.position - (Vector3)(direction.normalized * (backwardsSpeed * Time.deltaTime));
            
            if (Vector3.Distance(_initialPosition, backwardPosition) < Mathf.Abs(backwardLimit))
                transform.position = backwardPosition;
        }
    }
}