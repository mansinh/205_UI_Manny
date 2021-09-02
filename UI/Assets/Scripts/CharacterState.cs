using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*****************************************************************************************************
// Controls the state of the character
//*****************************************************************************************************
public class CharacterState : MonoBehaviour
{
    [SerializeField] Transform bargroup;
    public Bar staminaBar;
    public Bar healthBar;
    public ExpBar experienceBar;
    float totalExp;

    public int level = 1;
    [SerializeField] float maxStamina, maxHealth, staminaRegen, healthRegen;


    // Initialize states
    void Start()
    {
        staminaBar.Init(maxStamina);
        healthBar.Init(maxHealth);
        experienceBar.Init(50f * Mathf.Pow(2f, level));
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Damaging"))
        {
            //Reduce health and animate health bar when touching something damaging
            print("Ouch");
            StartCoroutine(Shake(bargroup, 0.5f, -30, healthBar.maxValue, 20));
            healthBar.ChangeValue(-30);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Healing"))
        {
            //Restore health and animate health bar when touching healing
            print("Whew");
            healthBar.ChangeValue(20);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Points"))
        {
            //Fill experience points bar when touching points
            print("Yippee");
            float expChange = 30;
            totalExp += expChange;
            experienceBar.ChangeValue(expChange);
            Destroy(other.gameObject);
        }
    }
    //Calculate the experience points needed to get to the next level
    public float GetNextLevelExp(int level)
    {
        this.level = level;
        return 50f * Mathf.Pow(2f, level);
    }

    //Apply shake animation to character bar group in HUD when damaged
    IEnumerator Shake(Transform t, float shakeDuration, float change, float maxValue, float maxShakeAmp)
    {
        Vector3 position = t.position;
        float duration = Mathf.Abs(shakeDuration * change / maxValue);
        for (float i = 0; i <= duration; i += 0.01f)
        {
            t.position = new Vector3(Random.value, Random.value, 0) * maxShakeAmp * change / maxValue + position;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        t.position = position;
    }

    //Regain stamina when player is not moving
    public void RegainStamina() {
        if (staminaBar.currentValue < staminaBar.maxValue)
        {
            staminaBar.ChangeValue(staminaRegen * Time.deltaTime);
        }
    }

    //Use stamina when the player is sprinting
    public void UseStamina(float cost) {
        staminaBar.ChangeValue(cost * Time.deltaTime);
    }
}
