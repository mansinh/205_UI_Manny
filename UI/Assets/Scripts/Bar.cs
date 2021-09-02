using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//*****************************************************************************************************
// General points bar with animations
//*****************************************************************************************************
public class Bar : MonoBehaviour
{

    public float maxValue = 100;
    public float currentValue = 100;
    float previousValue = 100;
    float change;
    Vector3 position;
    [SerializeField] Image valueImage, previousImage, background;
    [SerializeField] float maxShakeAmp, shakeDuration, showChangeDelay, changeDuration;
    [SerializeField] Color glowColor, maxColor, minColor;
    [SerializeField] float testChange;
    [SerializeField] bool glowOnRestore;
    bool isChanging = false;

    //Initialize full bar
    public void Init(float maxValue)
    {
        this.maxValue = maxValue;
        currentValue = previousValue = maxValue;
        valueImage.color = maxColor;
        position = transform.position;
    }

    //Apply animations on value changed
    public void ChangeValue(float change)
    {
        this.change = change;
        //Apply changes
        previousValue = currentValue;
        currentValue = Mathf.Clamp(currentValue + change, 0, maxValue);

        //Start change animations if they are not already occuring
        if (!isChanging)
        {
            if (change <= 0)
            {
                //Play shake animation and show reduction animation if the change is negative
                if (maxShakeAmp > 0)
                {
                    StartCoroutine(Shake());
                }
                StartCoroutine(ShowReduction());
            }
            else
            {
                //Play glow animation if change is positive
                if (glowOnRestore)
                {
                    StartCoroutine(Glow());
                }

            }
            //Fill or deplete bar over time set by changeDuration
            StartCoroutine(ShowChange());
        }
    }

    //Fill or deplete bar over time from previous to current value 
    IEnumerator ShowChange()
    {
        //Set change animation is playing so changes occuring during animation doesn't interrupt this animation
        isChanging = true;

        //Calculate bar length at the start and end of the animation
        float startLength = previousValue / maxValue;
        float endLength = currentValue / maxValue;

        //The time it takes for the animation to play 
        float duration = Mathf.Abs(changeDuration * change / maxValue);

        if (duration > 0 && duration != Mathf.Infinity)
        {
            for (float i = 0; i <= duration; i += 0.01f)
            {
                //Calculate the length of the bar for given time 
                endLength = currentValue / maxValue;
                float currentLength = Mathf.Lerp(startLength, endLength, i / duration);

                if (!glowOnRestore || change < 0)
                {
                    //Interpolate colour between colour at empty value(minColor) and color at full value(maxColor)
                    valueImage.color = Color.Lerp(minColor, maxColor, currentLength);
                }

                //Set the length of the bar
                valueImage.transform.localScale = new Vector3(currentLength, 1, 1);

                yield return new WaitForSecondsRealtime(0.01f);
            }
            valueImage.transform.localScale = new Vector3(endLength, 1, 1);
        }
        isChanging = false;
    }

    //Show the amount of bar reduced by showing a shadow of the bar at its previous value
    IEnumerator ShowReduction()
    {
        //Show change for some duration before disappearing
        yield return new WaitForSeconds(showChangeDelay);

        //Disappear behind current value
        float startLength = previousValue / maxValue;
        float endLength = currentValue / maxValue;
        float duration = Mathf.Abs(changeDuration * change / maxValue);

        if (showChangeDelay > 0)
        {
            for (float i = 0; i <= duration; i += 0.01f)
            {
                float currentLength = Mathf.Lerp(startLength, endLength, i / duration);
                previousImage.transform.localScale = new Vector3(currentLength, 1, 1);
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
        previousImage.transform.localScale = new Vector3(endLength, 1, 1);

    }

    //Shake the bar proportional to how much the bar value changed
    IEnumerator Shake()
    {
        float duration = Mathf.Abs(shakeDuration * change / maxValue);
        for (float i = 0; i <= duration; i += 0.01f)
        {
            transform.position = new Vector3(Random.value, Random.value, 0) * maxShakeAmp * change / maxValue + position;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        transform.position = position;
    }

    //Make the bar glow for some duration
    IEnumerator Glow()
    {
        float endLength = currentValue / maxValue;
        valueImage.color = glowColor;
        Color endColor = Color.Lerp(minColor, maxColor, endLength);
        for (float i = 0; i <= showChangeDelay; i += 0.01f)
        {
            valueImage.color = Color.Lerp(glowColor, endColor, i / showChangeDelay);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        valueImage.color = endColor;
    }
}
