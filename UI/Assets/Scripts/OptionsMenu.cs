using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionsMenu :PopupMenu
{
    
    [SerializeField] Slider musicSlider, soundSlider;
    [SerializeField] Toggle muteToggle;
    [SerializeField] SoundManager soundManager;

    //Set UI elements to match saved option settings
    public void Init()
    {
        musicSlider.value = soundManager.getMusicVolume();
        soundSlider.value = soundManager.getSoundVolume();
        muteToggle.isOn = soundManager.getMute();
    }
}
