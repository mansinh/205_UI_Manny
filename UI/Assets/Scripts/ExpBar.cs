using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//*****************************************************************************************************
// Radial bar that shows experience points. When the bar is full the level increases.
//*****************************************************************************************************
public class ExpBar : MonoBehaviour
{
    public float maxValue;
    [SerializeField] Material material;
    [SerializeField] float changeDuration;
    [SerializeField] TMP_Text levelText;
    public float progress;
    public float currentValue;
    public int level = 1;
    float previousValue;
    float change;
    bool isChanging = false;
    [SerializeField] CharacterState character;

    public void Init(float maxValue) {
        this.maxValue = maxValue;
        material.SetFloat("_Percentage", 0);
    }
    public void SetLevel(int level) {
        this.level = level;
    }

    public void ChangeValue(float change) {
        this.change += change;
        previousValue = currentValue;
        //Start change animation if not already playing
        if (!isChanging)
        {
            isChanging = true;
            StartCoroutine(IncreaseValue());
        }
    }

    //Animations for increasing the value of the bar
    IEnumerator IncreaseValue()
    {
        float startLength = previousValue / maxValue;
        //Make the leading edge of the bar to glow while increasing
        material.SetFloat("_Glow", 1);
        float added = previousValue;
       
        //Increase bar to changed value over time
        while(change> 0 )
        { 
            change -= 1;
            added += 1;

            //If bar is filled, increase the level and show animation
            if (added >= maxValue) {
                added = 0;
                level++;
                levelText.SetText("LV " + level);

                //Set the max value of bar to the amount of experience points needed to reach the next level
                maxValue = character.GetNextLevelExp(level);
                
                //Animate the bar to flash when full
                material.SetFloat("_Flash", 1);
                material.SetFloat("_Percentage", 1);
                StartCoroutine(FlashFade());
                yield return new WaitForSecondsRealtime(0.5f);
            }
            //Set current value to bar
            material.SetFloat("_Percentage", added/maxValue);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        currentValue = added;
        //material.SetFloat("_Percentage", added / maxValue);
        
        isChanging = false;

        //Make the leading edge of the bar to stop glowing
        StartCoroutine(GlowFade());
    }

    IEnumerator GlowFade() {
        for (float i = 0; i <= 0.5; i += 0.01f)
        {          
            material.SetFloat("_Glow", Mathf.Lerp(1, 0, i / 0.5f));
            yield return new WaitForSecondsRealtime(0.01f);
        }
        material.SetFloat("_Glow", 0);
    }
    IEnumerator FlashFade()
    {
        for (float i = 0; i <= 0.5; i += 0.01f)
        {
            material.SetFloat("_Flash", Mathf.Lerp(1, 0, i / 0.5f));
            yield return new WaitForSecondsRealtime(0.01f);
        }
        material.SetFloat("_Flash", 0);
    }
}
