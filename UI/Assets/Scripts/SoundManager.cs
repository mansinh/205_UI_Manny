using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    bool isMute = false;
    AudioSource musicSource;
    List<AudioSource> soundSources;
    float musicVolume = 0.5f;
    float soundVolume = 0.5f;
     

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
       
        soundSources = new List<AudioSource>();
        GameObject[] soundObjects = GameObject.FindGameObjectsWithTag("SoundSource");
        foreach (GameObject g in soundObjects)
        {
            AudioSource[] soundSource = g.GetComponents<AudioSource>();
            if (soundSource.Length > 0)
            {
                foreach (AudioSource source in soundSource)
                {
                    soundSources.Add(source);
                }
            }
           
        }

        if (PlayerPrefs.GetInt("initialized") == 0) {
            print("init");
            SetMusicVolume(0.5f);
            SetSoundVolume(0.5f);
            isMute = false;
            
            PlayerPrefs.SetInt("isMute",1);
            PlayerPrefs.SetInt("initialized", 1);
        }
       
        SetMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
        SetSoundVolume(PlayerPrefs.GetFloat("soundVolume"));

        isMute = PlayerPrefs.GetInt("isMute") == 0;
        SetMute();
    }
  
    public void ToggleMute(bool isOn) {
        if (isOn) { 
            
            PlayerPrefs.SetInt("isMute", 0);
            isMute = true;
        }
        else{
            PlayerPrefs.SetInt("isMute", 1);            
            isMute = false;
        }
        SetMute();
    }

    public void SetMute() {
    
        AudioListener.pause = isMute;
       
    }

    public void SetMusicVolume(float volume) {
        
        PlayerPrefs.SetFloat("musicVolume", volume);
        musicSource.volume = volume;
        musicVolume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        PlayerPrefs.SetFloat("soundVolume", volume);
        soundVolume = volume;
        foreach (AudioSource source in soundSources) {
            source.volume = volume;
        }
        if (soundSources.Count > 0)
        {
            soundSources[(int)(soundSources.Count * Random.value)].Play();
        }
    }
    public bool getMute()
    {
        
        return isMute;
    }
    public float getMusicVolume()
    {
        
        return musicVolume;
    }
    public float getSoundVolume() {
        
        return soundVolume;
    }
}
