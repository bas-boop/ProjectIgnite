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

        private bool _shouldUpdate;
        
        private void Update()
        {
            if (!_shouldUpdate)
                return;
            
            float t = 1 - timer.GetCurrentTimerPercentage();
            background.fillAmount = t;
            background.color = gradient.Evaluate(t);
        }

        private void OnEnable() => _shouldUpdate = true;

        private void OnDisable() => _shouldUpdate = false;
    }
}