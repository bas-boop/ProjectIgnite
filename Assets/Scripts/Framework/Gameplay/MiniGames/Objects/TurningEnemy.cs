using UnityEngine.Events;
using UnityEngine;
using TMPro;
using Framework.Attributes;

namespace Framework.Gameplay.MiniGames.Objects
{
    public sealed class TurningEnemy : MonoBehaviour
    {
        [SerializeField] private bool _isWatching;
        private SpriteRenderer _spriteRenderer; //temporary until art implementation
        [SerializeField,RangeVector2(2,3 , 3,5)] private Vector2 lookInterval;
        private Timer _timer;
        [SerializeField] private UnityEvent onCaught = new();

        private void Start() 
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _timer = GetComponent<Timer>();
        }

        public void OnMiniGameActive() => SetTimer();

        public void OnMinigameDeactivate() => _timer.SetCanCount(false);

        

        public void RoundBehaviour()
        {
            _timer.ResetTimer();
            Turn();
            SetTimer();
        }

        /// <summary>
        /// shows when the character is turned.( the color is temporary until art implementation)
        /// </summary>
        private void Turn()
        {
            _isWatching = !_isWatching;
            _spriteRenderer.color = _isWatching ? Color.red : Color.green; 
        }

        private void SetTimer()
        {
            float randomTime = Random.Range(lookInterval.x, lookInterval.y);
            _timer.SetTimerTarget(randomTime);
            _timer.SetCanCount(true);
        }

        public void CheckIsCaught()
        {
            if (_isWatching)
                onCaught?.Invoke();
        }
    }
}
