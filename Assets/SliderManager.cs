using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider slider;
    public static SliderManager sliderManager;

    private void Start()
    {
        SliderManager.sliderManager = this;
    }
    public  void SubtractFromSlider()
    {
            slider.value = Mathf.Max(slider.value - 3, slider.minValue); 
    }

    public  void AddToSlider()
    {
            slider.value = Mathf.Min(slider.value + 3, slider.maxValue); 
    }
}
