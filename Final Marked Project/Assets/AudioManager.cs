using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public string time = "DAY";
    public int DayTrackList = 0;
    public float TrackRunTime;

    public AudioMixerGroup SoundMixerGroup;
    public AudioMixerGroup MusicMixerGroup;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = MusicMixerGroup;
        }

        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1 || SceneManager.GetActiveScene().name != "City_Centre")
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void Update()
    {
        TrackRunTime -= Time.unscaledDeltaTime;
        if (time == "DAY")
        {
            if (DayTrackList == 0 && TrackRunTime <= 0)
            {
                play("DayMusic1");
                TrackRunTime = 147;
                DayTrackList += 1;
            }
            if (DayTrackList == 1 && TrackRunTime <= 0)
            {
                play("DayMusic2");
                TrackRunTime = 178;
                DayTrackList += 1;
            }
            if (DayTrackList == 2 && TrackRunTime <= 0)
            {
                play("DayMusic3");
                TrackRunTime = 156;
                DayTrackList += 1;
            }
            if (DayTrackList == 3 && TrackRunTime <= 0)
            {
                play("DayMusic4");
                TrackRunTime = 169;
                DayTrackList = 0;
            }
        }

        if (SceneManager.GetActiveScene().name != "City_Centre")
        {
            Destroy(this.gameObject);
        }
    }

    public void play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    
}
