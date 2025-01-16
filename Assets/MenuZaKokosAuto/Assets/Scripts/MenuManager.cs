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
        float masterVolume = master.value;
        mixer.SetFloat("masterValue", masterVolume);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }

    public void SetMusicVolume()
    {
        float musicVolume = music.value;
        mixer.SetFloat("musicValue", musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSFXVolume()
    {
        float sfxVolume = sfx.value;
        mixer.SetFloat("SFXValue", sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            master.value = masterVolume;
            mixer.SetFloat("masterValue", masterVolume);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            music.value = musicVolume;
            mixer.SetFloat("musicValue", musicVolume);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            sfx.value = sfxVolume;
            mixer.SetFloat("SFXValue", sfxVolume);
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
