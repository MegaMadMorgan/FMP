using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    //initialising Variables

    [Header("Base Option drag-ins")]
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    [Header("static variables")]
    public static float SFXVol;
    public Slider SFXSlider;
    public static float MusVol;
    public Slider MusSlider;
    public static float MosSen = 200;
    public Slider MosSlider;
    public static bool FullScreen = true;
    public Toggle FulScrn;
    public static int reso = -1;
    public static int quality = 4;
    public TMPro.TMP_Dropdown graphicsDropdown;

    private void Start()
    {
        // setting values from pre-established ones
        SFXSlider.value = SFXVol;
        MusSlider.value = MusVol;
        MosSlider.value = MosSen;
        FulScrn.isOn = FullScreen;
        graphicsDropdown.value = quality;
        resolutions = Screen.resolutions;

        // clear resolution drop down for checking the resolutions again
        resolutionDropdown.ClearOptions();

        // set options as a new list
        List<string> options = new List<string>();

        // set resolution index to zero
        int currentResolutionIndex = 0;

        // for each resolution, get it's width and height and add it to the list as a string
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // add an option to the resolutions dropdown
        resolutionDropdown.AddOptions(options);

        //  set resolutions if no resolutions
        if (reso == -1)
        {
            resolutionDropdown.value = currentResolutionIndex;
        }
        else
        {
            resolutionDropdown.value = reso;
        }
        // and refresh it
        resolutionDropdown.RefreshShownValue();
    }

    // setting resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        reso = resolutionIndex;
    }

    // setting audio volume
    public void SetSFXVolume (float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        SFXVol = volume;
    }

    // setting music volume
    public void SetMusicVolume (float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        MusVol = volume;
    }

    // setting mouse sensitivity 
    public void SetMouseSensitivity(float Sensitivity)
    {
        MosSen = Sensitivity;
    }

    // setting game quality
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        quality = qualityIndex;
    }

    // setting fullscreen on or off
    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        FullScreen = isFullScreen;
    }
}
