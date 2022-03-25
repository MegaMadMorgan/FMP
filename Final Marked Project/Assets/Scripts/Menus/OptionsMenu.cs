using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Base Option drag-ins")]
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    [Header("static variables")]
    public static float SFXVol;
    public Slider SFXSlider;
    public static float MusVol;
    public Slider MusSlider;
    public static bool FullScreen = true;
    public Toggle FulScrn;
    public static int reso = -1;
    public static int quality = 4;
    public TMPro.TMP_Dropdown graphicsDropdown;

    private void Start()
    {
        SFXSlider.value = SFXVol;
        MusSlider.value = MusVol;
        FulScrn.isOn = FullScreen;
        graphicsDropdown.value = quality;

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        if (reso == -1)
        {
            resolutionDropdown.value = currentResolutionIndex;
        }
        else
        {
            resolutionDropdown.value = reso;
        }
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        reso = resolutionIndex;
    }

    public void SetSFXVolume (float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        SFXVol = volume;
    }

    public void SetMusicVolume (float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        MusVol = volume;
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        quality = qualityIndex;
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        FullScreen = isFullScreen;
    }
}
