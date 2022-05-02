using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    //initialising Variables
    public Sound[] sounds;
    public AudioMixerGroup SoundMixerGroup;
    void Awake()
    {
        // setup sound settings for each sound
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = UnityEngine.Random.Range(0f, 3f);
            s.source.outputAudioMixerGroup = SoundMixerGroup;
        }

        // obj is an object with the tag of "music"
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Sound");

        // if scene isnt the game scene, destroy it!
        if (SceneManager.GetActiveScene().name != "City_Centre")
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySound(string name)
    {
        // find in array sound
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // set pitch for specific sounds
        if (s.name == "DeepPunch" || s.name == "DeepStab" || s.name == "DeepSlice")
        {
            s.source.pitch = 0.5f;
        }
        else if (s.name == "LaserSlash")
        {
            s.source.pitch = 2.5f;
        }
        else
        {
            s.source.pitch = UnityEngine.Random.Range(0.8f, 1.5f);
        }

        // play sound
        s.source.Play();

        // function to play a song in any scrip
        if (SceneManager.GetActiveScene().name != "City_Centre")
        {
            Destroy(this.gameObject);
        }
    }
}

    