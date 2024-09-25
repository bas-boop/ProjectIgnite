using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class QuickTimeEvent : MonoBehaviour
{
    private KeyCode requiredKeyCode;
    private bool isReady;
    private float currentTime;

    public TextMeshProUGUI promptText;
    public TextMeshProUGUI timerText;

    public void SetQTE(KeyCode key, float timelimit)
    {
        requiredKeyCode = key;
        currentTime = timelimit; 
        isReady = true;
        promptText.text = requiredKeyCode.ToString();
    }

    private void Update()
    {
        if (isReady)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                timerText.text = ((int)currentTime).ToString();
                if (Input.GetKeyDown(requiredKeyCode))
                {
                    Debug.Log("Success");
                    SliderManager.sliderManager.SubtractFromSlider();
                    Destroy(gameObject);
                }
            }
            else
            {
                Fail();
            }
        }
    }

    private void Fail()
    {
        SliderManager.sliderManager.AddToSlider();
        Debug.Log("OOp! Time ran out.");
        Destroy(gameObject);
    }
}
