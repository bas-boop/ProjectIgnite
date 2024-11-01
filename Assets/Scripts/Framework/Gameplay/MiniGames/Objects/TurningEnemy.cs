using UnityEngine.Events;
using UnityEngine;
using TMPro;

namespace Framework.Gameplay.MiniGames.Objects
{
    public sealed class TurningEnemy : MonoBehaviour
    {
        [SerializeField] private UnityEvent onCaught = new();
        [HideInInspector] public bool minigameActive;
        [SerializeField] private bool _isWatching;
        private SpriteRenderer _spriteRenderer; //temporary until art implementation
        private float _lookInterval;
        private Timer timer;

        private void Start() 
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            timer = GetComponent<Timer>(); 
        }

        public void OnMiniGameActive()
        {
            minigameActive = true;
            SetTimer();
        }

        public void OnMinigameDeactivate()
        {
            minigameActive = false;
            timer.SetCanCount(false);
        }

        public void RoundBehaviour()
        {
            timer.ResetTimer();
            Turn();
            SetTimer();
        }

        /// <summary>
        /// shows when the character is turned.( the color is temporary until art implementation)
        /// </summary>
        private void Turn()
        {
            _isWatching = !_isWatching;
            if (_isWatching )
                _spriteRenderer.color = Color.red;
            else
                _spriteRenderer.color = Color.green;
        }

        private void SetTimer()
        {
            _lookInterval = Random.Range(2, 4);
            timer.SetTimerTarget(_lookInterval);
            timer.SetCanCount(true);
        }

        public void CheckIsCaught()
        {
            if (_isWatching)
            {
                print("caught");
                onCaught?.Invoke();
            }  
        }
    }
}
