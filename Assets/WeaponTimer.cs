using UnityEngine;
using UnityEngine.UI;

public class WeaponTimer : MonoBehaviour
{
    [SerializeField, Tooltip("the visual slider for the timer")] private Slider slider;
    private float timer;
    private float currentTime;

    private void Start()
    {
        SetTimer(10f);
    }
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
            print("the timer ran out");
        }
    }
}
