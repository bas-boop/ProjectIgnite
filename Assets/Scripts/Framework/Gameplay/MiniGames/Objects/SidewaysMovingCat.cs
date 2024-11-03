using UnityEngine;
using UnityEngine.Events;

namespace Framework.Gameplay.MiniGames.Objects
{
    public sealed class SidewaysMovingCat : MonoBehaviour
    {
        [SerializeField , Range(0.5f , 2f)] private float movementStep = 0.5f;
        [SerializeField] private Transform goalPosition;
        [SerializeField, Tooltip("will be performed everytime the player decides to move")] private UnityEvent onMove = new();
        [SerializeField] private UnityEvent onGoalReached = new();
        private Vector3 _startPosition;
        private bool _canMove;

        private void Start() => _startPosition = transform.position;

        public void OnReset() => transform.position = _startPosition;

        /// <summary>
        /// will decide if you can move the object or not
        /// </summary>
        /// <param name="target"> the value the bool will be set to</param>
        public void ToggleMinigameActive(bool target) => _canMove = target;

        public void PerformStep()
        {
            if (!_canMove)
                return;
            
                Vector3 nextStepPosition = new Vector3(transform.position.x - movementStep, transform.position.y, transform.position.z);
                transform.position = nextStepPosition;
                onMove?.Invoke();
                CheckIfGoalReached();
        }

        public void CheckIfGoalReached()
        {
            if (transform.position.x <= goalPosition.position.x)
                onGoalReached?.Invoke();
        }
    }
}