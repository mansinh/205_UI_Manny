using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] Image valueImage, previousImage, background;


    public  float maxValue = 100;
    public float currentValue = 100;
    float previousValue = 100;
    float change;
    Vector3 position;
    [SerializeField] float maxShakeAmp, shakeDuration, showChangeDelay, changeDuration;
    [SerializeField] Color glowColor, maxColor, minColor;
    [SerializeField] float testChange;
    [SerializeField] bool glowOnRestore;
    bool isChanging = false;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void Init(float maxValue)
    {
        this.maxValue = maxValue;
        
        currentValue = previousValue = maxValue;
        valueImage.color = maxColor;
        position = transform.position;
        
    }

    private void Update()
    {
        /*
        if (Input.GetKeyUp(KeyCode.X)) {
            ChangeValue(-testChange*Random.value);
           
            
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            ChangeValue(testChange * Random.value);

        }
        */
     
        
    }

    public void ChangeValue(float change) {
        this.change = change;
        
        previousValue = currentValue;
        currentValue = Mathf.Clamp(currentValue+change,0,maxValue);

        if (!isChanging)
        {
            if (change <= 0)
            {
                if (maxShakeAmp > 0)
                {

                    StartCoroutine(Shake());
                }

                StartCoroutine(ShowReduction());
                StartCoroutine(ShowChange());
            }
            else
            {
                if (glowOnRestore) {
                    StartCoroutine(Glow());
                }
                StartCoroutine(ShowChange());
            }
        }
        

      
    }

    IEnumerator ShowChange()
    {
        isChanging = true;
        float startLength = previousValue / maxValue;
        float endLength = currentValue / maxValue;
        float duration = Mathf.Abs(changeDuration * change / maxValue);

        if (duration > 0 && duration != Mathf.Infinity)
        {
            for (float i = 0; i <= duration; i += 0.01f)
            {
                endLength = currentValue / maxValue;
                float currentLength = Mathf.Lerp(startLength, endLength, i / duration);
                if (!glowOnRestore || change < 0)
                {
                    valueImage.color = Color.Lerp(minColor, maxColor, currentLength);
                }
                
                valueImage.transform.localScale = new Vector3(currentLength, 1, 1);

                yield return new WaitForSecondsRealtime(0.01f);
            }
            valueImage.transform.localScale = new Vector3(endLength, 1, 1);
        }
        isChanging = false;
    }
    
    IEnumerator ShowReduction()
    {
        yield return new WaitForSeconds(showChangeDelay);

       
        
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


    IEnumerator Shake()
    {
        float duration = Mathf.Abs(shakeDuration * change / maxValue);
        for (float i = 0; i <= duration; i += 0.01f)
        {
            transform.position = new Vector3(Random.value,Random.value,0) * maxShakeAmp * change / maxValue + position;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        transform.position = position;
    }

    IEnumerator Glow()
    {
     
        float endLength = currentValue / maxValue;
        valueImage.color = glowColor;
        Color endColor = Color.Lerp(minColor, maxColor, endLength);
        for (float i = 0; i <= showChangeDelay; i += 0.01f)
        {
            valueImage.color = Color.Lerp(glowColor, endColor, i/showChangeDelay);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        valueImage.color = endColor;
       
    }
}
