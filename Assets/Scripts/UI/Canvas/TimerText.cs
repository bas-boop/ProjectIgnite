using UnityEngine;
using TMPro;

using Framework;

namespace UI.Canvas
{
    public class TimerText : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        [SerializeField] private TMP_Text text;

        private void Update()
        {
            int currentTimerLength = (int) timer.GetCurrentTimerLength();
            string currentTimerString = currentTimerLength.ToString();
            text.text = currentTimerString;
        }
    }
}