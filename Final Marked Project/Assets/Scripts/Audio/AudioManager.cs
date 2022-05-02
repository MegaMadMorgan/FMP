using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //initialising Variables
    public Sound[] sounds;
    public string time = "DAY";
    public int DayTrackList = 0;
    public float TrackRunTime;

    public AudioMixerGroup SoundMixerGroup;
    public AudioMixerGroup MusicMixerGroup;

    void Awake()
    {
        // setup sound settings for each sound
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = MusicMixerGroup;
        }

        // obj is an object with the tag of "music"
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        // if more then one, destroy it // if scene isnt the game scene, destroy it!
        if (objs.Length > 1 || SceneManager.GetActiveScene().name != "City_Centre")
        {
            Destroy(this.gameObject);
        }
    }

    public void Update()
    {
        // make the track time countdown regardless of pause
        TrackRunTime -= Time.unscaledDeltaTime;

        // play music in the day
        if (time == "DAY")
        {
            if ((DayTrackList == 0 || DayTrackList == 4) && TrackRunTime <= 0)
            {
                play("DayMusic1");
                TrackRunTime = 147;
                DayTrackList += 1;
            }
            if ((DayTrackList == 1 || DayTrackList == 5) && TrackRunTime <= 0)
            {
                play("DayMusic2");
                TrackRunTime = 178;
                DayTrackList += 1;
            }
            if ((DayTrackList == 2 || DayTrackList == 6) && TrackRunTime <= 0)
            {
                play("DayMusic3");
                TrackRunTime = 156;
                DayTrackList += 1;
            }
            if ((DayTrackList == 3 || DayTrackList == 7) && TrackRunTime <= 0)
            {
                play("DayMusic4");
                TrackRunTime = 169;
                DayTrackList = 0;
            }
        }
        else // play music in the night
        {
            if ((DayTrackList == 4 || DayTrackList == 0) && TrackRunTime <= 0)
            {
                play("NightMusic1");
                TrackRunTime = 248;
                DayTrackList += 1;
            }
            if ((DayTrackList == 5 || DayTrackList == 1) && TrackRunTime <= 0)
            {
                play("NightMusic2");
                TrackRunTime = 157;
                DayTrackList += 1;
            }
            if ((DayTrackList == 6 || DayTrackList == 2) && TrackRunTime <= 0)
            {
                play("NightMusic3");
                TrackRunTime = 145;
                DayTrackList += 1;
            }
            if ((DayTrackList == 7 || DayTrackList == 3) && TrackRunTime <= 0)
            {
                play("NightMusic4");
                TrackRunTime = 451;
                DayTrackList = 4;
            }
        }

        // check if it's day or night by seeing which hour it is in the lighting manager
        if (GameObject.Find("GameManager").GetComponent<LightingManager>().TimeOfDay > 6 && GameObject.Find("GameManager").GetComponent<LightingManager>().TimeOfDay < 18)
        {
            time = "NIGHT";
        }
        else
        {
            time = "DAY";
        }

        // destroy this if not in the game scene!
        if (SceneManager.GetActiveScene().name != "City_Centre")
        {
            Destroy(this.gameObject);
        }
    }

    // function to play a song in any script
    public void play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    
}
