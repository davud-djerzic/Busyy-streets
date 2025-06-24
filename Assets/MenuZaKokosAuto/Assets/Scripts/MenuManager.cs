using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public Slider master, sfx, music;
    public AudioMixer mixer;
    public TextMeshProUGUI usernameText;

    void Start()
    {
        usernameText.text = PlayerPrefs.GetString("username", "Player");
        LoadVolumeSettings();
        PlayerPrefs.SetInt("BuyCar0", 1); // cuvamo kupljeno auto
                                          //CarSelection1.currentCar = 0;

        if (PlayerPrefs.GetInt("NeedToIncreaseSavedCar") == 1)
        {
            CarSelection1.currentCar = PlayerPrefs.GetInt("SavedCar", 0) + 1; // ako je kupljeno auta sa manjim indeksom od trenutno koristenog auta, uveca se index za 1 trenutnog auta 
            // jer je kupljeno auto dodano u listu prije koristenog auta
        }
        else
        {
            CarSelection1.currentCar = PlayerPrefs.GetInt("SavedCar", 0);
        }
    }

    public void SetMasterVolume()
    {
        float sliderValue = master.value; // vrednost od 0.0001 do 1
        float dB = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("masterValue", dB);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    public void SetMusicVolume()
    {
        float sliderValue = music.value;
        float dB = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("musicValue", dB);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSFXVolume()
    {
        float sliderValue = sfx.value;
        float dB = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("SFXValue", dB);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float savedValue = PlayerPrefs.GetFloat("MasterVolume");
            master.value = savedValue;
            mixer.SetFloat("masterValue", Mathf.Log10(savedValue) * 20);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float savedValue = PlayerPrefs.GetFloat("MusicVolume");
            music.value = savedValue;
            mixer.SetFloat("musicValue", Mathf.Log10(savedValue) * 20);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float savedValue = PlayerPrefs.GetFloat("SFXVolume");
            sfx.value = savedValue;
            mixer.SetFloat("SFXValue", Mathf.Log10(savedValue) * 20);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
