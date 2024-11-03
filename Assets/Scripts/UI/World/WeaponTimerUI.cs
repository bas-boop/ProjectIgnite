using UnityEngine;
using UnityEngine.UI;

using Environment;
using Framework;

namespace UI.World
{
    public sealed class WeaponTimerUI : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        [SerializeField] private Image background;
        [SerializeField] private Gradient gradient;
        [SerializeField] private bool isUI;
        
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

        private void OnEnable()
        {
            _shouldUpdate = true;
            
            if (isUI)
                return;
            
            ScreenChecker a = FindObjectOfType<ScreenChecker>();
            a.Add(transform);
        }

        private void OnDisable() => _shouldUpdate = false;
    }
}