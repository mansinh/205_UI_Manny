using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] Transform bargroup;
    public Bar staminaBar;
    public Bar healthBar;
    public ExpBar experienceBar;
    float totalExp;

    public int level = 1;
    [SerializeField] float maxStamina, maxHealth, staminaRegen, healthRegen;


    // Start is called before the first frame update
    void Start()
    {

        staminaBar.Init(maxStamina);
        healthBar.Init(maxHealth);
        experienceBar.Init(50f * Mathf.Pow(2f, level));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damaging"))
        {
            print("Ouch");
            StartCoroutine(Shake(bargroup, 0.5f, -30, healthBar.maxValue, 20));
            healthBar.ChangeValue(-30);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Healing"))
        {
            print("Whew");
            healthBar.ChangeValue(20);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Points"))
        {
            print("Yippee");
            float expChange = 30;
            totalExp += expChange;
            experienceBar.ChangeValue(expChange);
            Destroy(other.gameObject);
        }
    }
    public float GetNextLevelExp(int level)
    {
        this.level = level;
        return 50f * Mathf.Pow(2f, level);
    }




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

    public void RegainStamina() {
        if (staminaBar.currentValue < staminaBar.maxValue)
        {
            staminaBar.ChangeValue(staminaRegen * Time.deltaTime);
        }
    }

    public void UseStamina(float cost) {
        staminaBar.ChangeValue(cost * Time.deltaTime);
    }
}
