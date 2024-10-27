using UnityEngine;
using UnityEngine.UI;

using Framework;
public class WeaponUIHandler : MonoBehaviour
{
    [SerializeField, Tooltip("the visual slider for the timer")] private Slider slider;

    private Timer _weaponTimer;
    private bool canUpdateValues;

    private void Start()
    {
        _weaponTimer = GetComponent<Timer>();
        _weaponTimer.SetCanCount(true);
    }

    /// <summary>
    /// sets the slider values to match the current timer values
    /// </summary>
    public void SetSliderValues()
    {
        slider.maxValue = _weaponTimer.GetTimerTarget();
        canUpdateValues = true;
    }

    private void Update()
    {
        if (canUpdateValues)
            slider.value = _weaponTimer.GetCurrentTimerLength();

    }
}
