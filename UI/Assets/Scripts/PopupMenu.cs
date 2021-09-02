using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//*****************************************************************************************************
//Applies a smooth popping in and out animation to UI object
//*****************************************************************************************************
public class PopupMenu : MonoBehaviour
{
    [SerializeField] float popDuration = 0.3f;
    CanvasGroup canvasGroup;
    
    void Start()
    {
        this.canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }


    //Fade in and make it interactable and blocking UI below
    public void PopIn()
    {
        StartCoroutine(Fade(1));
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    //Fade to clear and make it uninteractable and not blocking UI below
    public void PopOut()
    {
        StartCoroutine(Fade(0));
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }


    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;

        for (float i = 0; i <= popDuration; i += 0.01f)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, i / popDuration);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        canvasGroup.alpha = targetAlpha;
    }
}
