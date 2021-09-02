using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the sound settings such as the volume for music and sound effects and mute setting

public class SoundManager : MonoBehaviour
{
    bool isMute = false;
    AudioSource musicSource;
    List<AudioSource> soundSources;
    float musicVolume = 0.5f;
    float soundVolume = 0.5f;
    
    private void Awake()
    {
        //Find music source
        musicSource = GetComponent<AudioSource>();
       
        //Fine all sound effect sources
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

        //If this is the first time the game has initialized, set the options to their default values, otherwise load previous settings
        if (PlayerPrefs.GetInt("initialized") == 0)
        {
            //Set and save volume settings and apply to all sound sources
            SetMusicVolume(0.5f);
            SetSoundVolume(0.5f);


            //Set and save mute settings
            isMute = false;
            PlayerPrefs.SetInt("isMute", 1);

            //Game has been initialized before
            PlayerPrefs.SetInt("initialized", 1);
        }
        else {
            //Load volume settings
            SetMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
            SetSoundVolume(PlayerPrefs.GetFloat("soundVolume"));

            //Load mute setting
            isMute = PlayerPrefs.GetInt("isMute") == 0;
        }       
        //Apply mute 
        SetMute();
    }
    
    //Set and save mute setting with toggle 
    public void ToggleMute(bool isOn) {
        if (isOn) {          
            PlayerPrefs.SetInt("isMute", 0);
            isMute = true;
        }
        else{
            PlayerPrefs.SetInt("isMute", 1);            
            isMute = false;
        }
        //Apply mute 
        SetMute();
    }

    public void SetMute() { 
        AudioListener.pause = isMute;     
    }

    //Set and save music volume so it persists over scene changes
    public void SetMusicVolume(float volume) {
        PlayerPrefs.SetFloat("musicVolume", volume);
        musicSource.volume = volume;
        musicVolume = volume;
    }

    //Set and save sound effects volume so it persists over scene changes
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
