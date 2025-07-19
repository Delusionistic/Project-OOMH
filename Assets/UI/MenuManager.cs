using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Resolution")]
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedRes;
    public TMP_Text resolutionLabel;
    public Toggle fullscreenTog;

    [Header("Audio")]
    public AudioMixer mainMixer;
    public static float masterVolume;
    public static float musicVolume;
    public static float SFXVolume;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstTimeOpening"))
        {
            ResetVolumePrefs();
            UpdateMixerVolume();
            PlayerPrefs.SetInt("FirstTimeOpening", 1);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        SFXVolume = PlayerPrefs.GetFloat("effectsVolume");

        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        SFXSlider.value = SFXVolume;

        UpdateMixerVolume();

        fullscreenTog.isOn = Screen.fullScreen;

        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
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
    }

    public void PlayPressed()
    {
        SceneManager.LoadScene("Level 1");

        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("effectsVolume", SFXVolume);

        PlayerPrefs.Save();
    }

    public void QuitPressed()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("effectsVolume", SFXVolume);

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
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(SFXVolume) * 20);

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

        masterVolume = 1;
        musicVolume = 1;
        SFXVolume = 1;

        PlayerPrefs.Save();
    }

    public void ResLeft()
    {
        selectedRes = Mathf.Max(selectedRes - 1, 0);
        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedRes = Mathf.Min(selectedRes + 1, resolutions.Count - 1);
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedRes].horizontal + " x " + resolutions[selectedRes].vertical;
    }

    public void ApplyGraphics()
    {
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreenTog.isOn);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
