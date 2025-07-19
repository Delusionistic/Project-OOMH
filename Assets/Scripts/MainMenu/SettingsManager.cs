using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Resolution")]

    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedRes;
    public TextMeshProUGUI resolutionLabel;

    public Toggle fullscreenTog;

    [Header ("Audio")]

    public AudioMixer mainMixer;

    public static float masterVolume;
    public static float musicVolume;
    public static float SFXVolume;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;


    void Start()
    {
        if (PlayerPrefs.GetInt("FirstTimeOpening") == 0)
        {
            ResetVolumePrefs();
            UpdateMixerVolume();
            PlayerPrefs.SetInt("FirstTimeOpening", 1);
        }

        Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        SFXVolume = PlayerPrefs.GetFloat("effectsVolume");

        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("effectsVolume");
        UpdateMixerVolume();

        fullscreenTog.isOn = Screen.fullScreen;

        bool foundRes = false;
        for(int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedRes = i;
                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedRes = resolutions.Count - 1;
            UpdateResLabel();
        }

        ApplyGraphics();
    }


    public void PlayPressed()
    {
        SceneManager.LoadScene("SampleScene");

        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXSliderValue", SFXVolume);

        PlayerPrefs.Save();
    }

    public void QuitPressed()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXSliderValue", SFXVolume);

        PlayerPrefs.Save();
        Application.Quit();
    }

    public void MasterVolumeChange(float value)
    {
        masterVolume = value;
        UpdateMixerVolume();
    }

    public void MusicVolumeChange(float value)
    {
        musicVolume = value;
        UpdateMixerVolume();
    }

    public void SFXVolumeChange(float value)
    {
        SFXVolume = value;
        UpdateMixerVolume();
    }

    public void UpdateMixerVolume()
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(SFXVolume) * 20);
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(musicVolume) * 20);

        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("effectsVolume", SFXVolume);

        PlayerPrefs.Save();
    }

    void ResetVolumePrefs()
    {
        PlayerPrefs.SetFloat("masterVolume", 1);
        PlayerPrefs.SetFloat("musicVolume", 1);
        PlayerPrefs.SetFloat("effectsVolume", 1);

        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        SFXVolume = PlayerPrefs.GetFloat("effectsVolume");

        PlayerPrefs.Save();
    }

    public void ResLeft()
    {
        selectedRes--;

        if (selectedRes < 0)
        {
            selectedRes = 0;
        }

        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedRes++;

        if(selectedRes > resolutions.Count - 1)
        {
            selectedRes = resolutions.Count - 1;
        }

        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedRes].horizontal.ToString() + " x " + resolutions[selectedRes].vertical.ToString();
    }

    public void ApplyGraphics()
    {
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreenTog.isOn);
    }

    public void TurnCursorOn(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TurnCursorOff(){
        Cursor.visible = false;
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
