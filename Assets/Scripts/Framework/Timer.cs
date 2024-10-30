using UnityEngine;
using UnityEngine.Events;

namespace Framework
{
    public sealed class Timer : MonoBehaviour
    {
        private const int FULL_PERCENTAGE = 100;

        [SerializeField] private bool isCountingUp;
        [SerializeField] private bool canCountOnStart;
        [SerializeField] private bool canCount;
        [SerializeField, Tooltip("Time in seconds to count down form.")] private float startingTime;
        [SerializeField, Tooltip("Time in seconds to count up to.")] private float timerThreshold;

        private float _currentTimer;
        private bool _isStarting = true;

        #region Events

        [SerializeField] private UnityEvent onTimerDone = new();
        [SerializeField] private UnityEvent onTimerPassedThreshold = new();
        [SerializeField] private UnityEvent onStart = new();
        [SerializeField] private UnityEvent onReset = new();

        #endregion

        private void Start()
        {
            if (canCountOnStart)
                _currentTimer = isCountingUp 
                    ? timerThreshold 
                    : startingTime;
            SetCanCount(canCountOnStart);
        }

        private void Update() => Counting();

        /// <summary>
        /// Reset the timer to startingTime and calls the onReset event.
        /// </summary>
        public void ResetTimer()
        {
            onReset?.Invoke();
            _currentTimer = startingTime;
            _isStarting = true;
        }

        /// <summary>
        /// Set the canCount property, when setting it to true it will start counting, otherwise is stops.
        /// </summary>
        /// <param name="target">The target for the property</param>
        public void SetCanCount(bool target) => canCount = target;

        /// <summary>
        /// Set the timer length, when resetting it will use this number.
        /// </summary>
        /// <param name="target">The target amount for the timer</param>
        public void SetTimerLength(float target) => _currentTimer = target;

        /// <summary>
        /// Get the timer it's current length.
        /// </summary>
        /// <returns>The current timer length</returns>
        public float GetCurrentTimerLength() => _currentTimer;

        /// <summary>
        /// Calculates the percentage of the current timer relative to the progress.
        /// </summary>
        /// <returns>Return a number between 0-1</returns>
        public float GetCurrentTimerPercentage()
        {
            float timerLimit = isCountingUp ? timerThreshold : startingTime;
            return Mathf.Clamp01(_currentTimer / timerLimit);
        }

        private void Counting()
        {
            if (!canCount)
                return;

            if (_isStarting)
            {
                _isStarting = false;
                onStart?.Invoke();
            }

            _currentTimer = isCountingUp
                ? _currentTimer + Time.deltaTime
                : _currentTimer - Time.deltaTime;

            switch (isCountingUp)
            {
                case false
                    when _currentTimer <= 0:
                    onTimerDone?.Invoke();
                    SetCanCount(false);
                    break;
                case true
                    when _currentTimer > timerThreshold:
                    onTimerPassedThreshold?.Invoke();
                    SetCanCount(false);
                    break;
            }
        }
    }
}