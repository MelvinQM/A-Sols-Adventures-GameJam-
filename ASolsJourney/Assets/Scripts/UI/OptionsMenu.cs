using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetMusicVolume(float volume)
    {
        string groupName = "MusicVolume";
        Debug.Log($"Set {groupName} volume to: " + volume);
        mixer.SetFloat(groupName, volume);
    }
    public void SetGameVolume(float volume)
    {
        string groupName = "GameVolume";
        Debug.Log($"Set {groupName} volume to: " + volume);
        mixer.SetFloat(groupName, volume);
    }

    public void SetMasterVolume(float volume)
    {
        string groupName = "MasterVolume";
        Debug.Log($"Set {groupName} volume to: " + volume);
        mixer.SetFloat(groupName, volume);
    }


    

    public void SetFullscreen(bool isFullScreen)
    {
        Debug.Log("Set screen fullscreen: " + isFullScreen);
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Debug.Log("Set resolution to index: " + resolutionIndex);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
