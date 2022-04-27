using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public AudioMixerGroup SoundMixerGroup;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = UnityEngine.Random.Range(0f, 3f);
            s.source.outputAudioMixerGroup = SoundMixerGroup;
        }

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Sound");

        if (SceneManager.GetActiveScene().name != "City_Centre")
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
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
        s.source.Play();
        if (SceneManager.GetActiveScene().name != "City_Centre")
        {
            Destroy(this.gameObject);
        }
    }
}

    