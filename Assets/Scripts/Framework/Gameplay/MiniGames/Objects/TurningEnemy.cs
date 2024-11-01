using UnityEngine.Events;
using UnityEngine;

namespace Framework.Gameplay.MiniGames.Objects
{
    public class TurningEnemy : MonoBehaviour
    {
        [SerializeField] private UnityEvent onCaught = new();
        [HideInInspector] public bool minigameActive;
        private float _lookInterval;
        [SerializeField] private bool _isWatching;
        private Timer timer;

        private void Start()
        {
            timer = GetComponent<Timer>();
        }

        public void StartMinigame()
        {
            minigameActive = true;
            SetTimer();
            timer.SetTimerTarget(_lookInterval);
            timer.SetCanCount(true);
        }

        public void StopMinigame()
        {
            minigameActive = false;
            timer.SetCanCount(false);
        }

        public void RoundBehaviour()
        {
            timer.ResetTimer();
            Turn();
            SetTimer();
            timer.SetTimerTarget(_lookInterval);
            timer.SetCanCount(true);
        }

        private void Turn() => _isWatching = !_isWatching;

        private void SetTimer() => _lookInterval = Random.Range(2, 4);

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
