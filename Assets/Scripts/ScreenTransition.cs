using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField] Image fadeToColorImage;
    [SerializeField] Color fadeToColor;
    [SerializeField] Volume volume;  
    [SerializeField] float saturationIn, saturationOut;
    [SerializeField] float transitionSpeed = 1;
    [SerializeField] float startDelay = 3;

    VolumeProfile profile;
    ColorAdjustments colorAdjustments;

    // Start is called before the first frame update
    void Start()
    {
        if(volume) profile = volume.profile;
        if(profile)profile.TryGet(out colorAdjustments);
        
        
    }

    public void StartFadeIn() {
        fadeToColorImage.color = fadeToColor;
        if (colorAdjustments) colorAdjustments.saturation.value = saturationOut;
        StartCoroutine(DelayedStart(startDelay));
        
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator DelayedStart(float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        FadeIn();
    }

    IEnumerator FadeOutCoroutine()
    {
        fadeToColorImage.enabled = true;
        for (float i = 0; i <= transitionSpeed; i += 0.01f)
        {
            fadeToColorImage.color = Color.Lerp(Color.clear,fadeToColor, i / transitionSpeed);
            if (colorAdjustments) colorAdjustments.saturation.value = Mathf.Lerp(saturationIn, saturationOut, i / transitionSpeed*2);       
            yield return new WaitForSecondsRealtime(0.01f);
        }
        fadeToColorImage.color = fadeToColor;
        if (colorAdjustments) colorAdjustments.saturation.value =saturationOut;
        
    }

    IEnumerator FadeInCoroutine()
    {
        for (float i = 0; i <= transitionSpeed; i += 0.01f)
        {
            fadeToColorImage.color = Color.Lerp(fadeToColor, Color.clear,  i / transitionSpeed);

            if (colorAdjustments) colorAdjustments.saturation.value = Mathf.Lerp(saturationOut, saturationIn, i / transitionSpeed*2);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        fadeToColorImage.color = Color.clear;
        if (colorAdjustments) colorAdjustments.saturation.value = saturationIn;
        fadeToColorImage.enabled = false;
    }
}
