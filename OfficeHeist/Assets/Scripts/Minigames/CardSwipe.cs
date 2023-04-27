using System;
using UnityEngine;
using UnityEngine.UI;

public class CardSwipe : MonoBehaviour
{
    public Slider slider;
    public float offset;
    
    float minTime = 0.3f;
    float maxTime = 0.6f;
    DateTime startTime;

    public void OnPointerDown()
    {
        startTime = DateTime.Now;
    }
    
    public void OnPointerUp()
    {
        if (slider.value < 1 - offset) 
        {
            resetSlider();
            Debug.Log("Swipe unsuccessful");
            return;
        }
        
        float timeElapsed = (float)(DateTime.Now - startTime).TotalSeconds;
        Debug.Log("Time elapsed: " + timeElapsed);
        if (timeElapsed < minTime)
        {
            resetSlider();
            Debug.Log("Too Fast");
            return;
        }
        if (timeElapsed > maxTime)
        {
            resetSlider();
            Debug.Log("Too Slow");
            return;
        }
        Debug.Log("Swipe successful");
    }
    
    void resetSlider()
    {
        slider.value = 0f;
    }
}
