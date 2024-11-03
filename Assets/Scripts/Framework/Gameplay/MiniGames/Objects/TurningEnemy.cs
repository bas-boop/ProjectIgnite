using UnityEngine.Events;
using UnityEngine;

using Framework.Attributes;
using Framework.Extensions;
using Random = UnityEngine.Random;

namespace Framework.Gameplay.MiniGames.Objects
{
    public sealed class TurningEnemy : MonoBehaviour
    {
        [SerializeField] private bool isWatching;
        [SerializeField,RangeVector2(2,3 , 3,5)] private Vector2 lookInterval;
        [SerializeField] private UnityEvent onCaught = new();

        private Timer _timer;

        private void Awake() => _timer = GetComponent<Timer>();

        public void OnMiniGameActive() => SetTimer();

        public void OnMinigameDeactivate() => _timer.SetCanCount(false);

        public void RoundBehaviour()
        {
            _timer.ResetTimer();
            Turn();
            SetTimer();
        }

        /// <summary>
        /// shows when the character is turned.
        /// </summary>
        private void Turn()
        {
            isWatching = !isWatching;

            Vector3 scale = transform.localScale;
            scale.InvertX();
            transform.localScale = scale;
        }

        private void SetTimer()
        {
            float randomTime = Random.Range(lookInterval.x, lookInterval.y);
            _timer.SetTimerTarget(randomTime);
            _timer.SetCanCount(true);
        }

        public void CheckIsCaught()
        {
            if (isWatching)
                onCaught?.Invoke();
        }
    }
}
