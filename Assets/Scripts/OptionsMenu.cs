using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    
    [SerializeField] Slider musicSlider, soundSlider;
    [SerializeField] Toggle muteToggle;
    [SerializeField] SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        musicSlider.value = soundManager.getMusicVolume();
        soundSlider.value = soundManager.getSoundVolume();
        muteToggle.isOn = soundManager.getMute();
    }
}
