using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Update is called once per frame
    void Update()
    {
        
    }

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
       

        if (!isChanging)
        {
            isChanging = true;
            StartCoroutine(IncreaseValue());
        }
    }


    IEnumerator IncreaseValue()
    {
        


        float startLength = previousValue / maxValue;
        
        
        material.SetFloat("_Glow", 1);

        
        float added = previousValue;
       
        while(change> 0 )
        {

            change -= 1;
            added += 1;
            if (added >= maxValue) {
                added = 0;
                level++;
                maxValue = character.GetNextLevelExp(level);
                print(maxValue + " " +added + " " + change);
                levelText.SetText("LV " + level);
            }



            material.SetFloat("_Percentage", added/maxValue);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        currentValue = added;
        //material.SetFloat("_Percentage", added / maxValue);
        
        isChanging = false;
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
}
