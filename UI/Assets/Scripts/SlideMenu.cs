using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*****************************************************************************************************
//Applies a sliding in and out animation to a UI object
//*****************************************************************************************************
public class SlideMenu : MonoBehaviour
{
    [SerializeField] RectTransform activePosition;
    [SerializeField] RectTransform inactivePosition;
    [SerializeField] RectTransform uiComponent;
    [SerializeField] float duration;

    public void SlideIn()
    {
        StartCoroutine(SlideTo(uiComponent, activePosition.anchoredPosition, duration));
    }
    public void SlideOut()
    {
        StartCoroutine(SlideTo(uiComponent, inactivePosition.anchoredPosition, duration));
    }

    //Slide from current position to end position over time duration
    IEnumerator SlideTo(RectTransform t, Vector3 endPosition, float duration)
    { 
        Vector3 startPosition = t.anchoredPosition;

        for (float i = 0; i <= duration; i += 0.01f)
        {
            //Smoothly lerp in cubic time for a snappy animation
            t.anchoredPosition = Vector3.Lerp(startPosition, endPosition, Mathf.Pow(i / duration, 3));
            yield return new WaitForSecondsRealtime(0.01f);
        }
        t.anchoredPosition = endPosition;
    }
}
