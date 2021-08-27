using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    bool mute = false;
    AudioSource musicSource;
    [SerializeField] List<AudioSource> soundSources = new List<AudioSource>();
    float musicVolume = 0.5f;
    float soundVolume = 0.5f;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = musicVolume;
       
        GameObject[] soundObjects = GameObject.FindGameObjectsWithTag("SoundSource");
        foreach (GameObject g in soundObjects) {
            AudioSource[] soundSource = g.GetComponents<AudioSource>();
            if (soundSource.Length>0) {
                foreach (AudioSource source in soundSource)
                {
                    soundSources.Add(source);
                    source.volume = soundVolume;
                }
            }
        }
    }
    public void ToggleMute() {
        if (mute) { 
            AudioListener.volume = 1;
            mute = false;
        }
        else{
            AudioListener.volume = 0;
            mute = true;
        }
        
    }
    public void SetMusicVolume(float volume) {
        musicSource.volume = volume;
        musicVolume = volume;
    }

    public void SetSoundVolume(float volume)
    {
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
        return mute;
    }
    public float getMusicVolume()
    {
        return musicVolume;
    }
    public float getSoundVolume() {
        return soundVolume;
    }
}
