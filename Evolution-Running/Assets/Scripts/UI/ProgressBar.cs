using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private float currentProgress;
    private float newProgress;



    public void SetMaxProgress(int progress)
    {
        slider.maxValue = progress;
        slider.value = 0;

        fill.color = gradient.Evaluate(0f);
    }



    public void SetProgress(int progress)
    {
        //Debug.Log("HEEEERRRE");
        newProgress = progress;
        currentProgress = slider.value;
        Debug.Log(currentProgress + "  " + newProgress);
        StopCoroutine(smoothProgress());
        StartCoroutine(smoothProgress());
        //slider.value = progress;



        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    IEnumerator smoothProgress()
    {
        float incr = 0.1f;
        if (currentProgress > newProgress)
            incr = -0.1f;
        while (Mathf.Abs(currentProgress-newProgress)>0.1f)
        {
            currentProgress += incr;
            slider.value = currentProgress;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            yield return new WaitForSeconds(0.05f);
        }
        
    }
    
}
