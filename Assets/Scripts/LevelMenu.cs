using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Saved", 1);
        //Debug.Log("Unlocked level: " + unlockedLevel);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        GameObject music = GameObject.Find("Audio Source");
        if (music != null)
        {
            Destroy(music);
        }
        UnlockedLevel();
        string levelName = "Level" + levelId;
        SceneManager.LoadScene(levelName);
        PlayerPrefs.SetInt("ChoosenLevel", levelId);
    }

    private void UnlockedLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Saved", 1);
        //Debug.Log("Unlocked level: " + unlockedLevel);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }


}
