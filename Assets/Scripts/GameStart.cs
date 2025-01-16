using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void buttonClick()
    {
        int level = PlayerPrefs.GetInt("Saved", 1);
        SceneManager.LoadScene(level);
    }

    public void loadShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void loadGarage()
    {
        SceneManager.LoadScene("Garage");
    }


    public void buttonReset()
    {
        PlayerPrefs.SetInt("Saved", 1);
        SceneManager.LoadScene(0);
    }

}
