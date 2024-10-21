using UnityEngine;
using UnityEngine.UI;

public class WeaponTimer : MonoBehaviour
{
    [SerializeField, Tooltip("the visual slider for the timer")] private Slider slider;
    private float timer;
    private float currentTime;

    public void SetTimer(float ammount)
    {
        timer = ammount;
        slider.maxValue = ammount;
    }

    private void Update()
    {
        if (currentTime < timer) 
        {
            currentTime += Time.deltaTime;  
            slider.value = currentTime;  
        }
        else
        {
            Debug.Log("the timer ran out");
        }
    }
}
