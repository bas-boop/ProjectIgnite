using UnityEngine;
using UnityEngine.UI;

using Framework;

namespace UI.World
{
    public sealed class WeaponTimerUI : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        [SerializeField] private Image background;
        [SerializeField] private Gradient gradient;
        public float Progress { get; set; }

        private bool _shouldUpdate;
        
        private void Update()
        {
            if (!_shouldUpdate)
                return;
            
            if (timer)
                Progress = 1 - timer.GetCurrentTimerPercentage();
            
            background.fillAmount = Progress;
            background.color = gradient.Evaluate(Progress);
        }

        private void OnEnable() => _shouldUpdate = true;

        private void OnDisable() => _shouldUpdate = false;

        public void SetTimer(Timer target) => timer = target;
    }
}