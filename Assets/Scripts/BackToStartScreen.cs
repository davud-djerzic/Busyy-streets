using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStartScreen : MonoBehaviour
{

    public void backToStartScreen()
    {
        SceneManager.LoadScene(0);
        //CarSelection1.currentCar = PlayerPrefs.GetInt("SavedCar", 0); pogledati kasnije
        //Profile.currentCar = PlayerPrefs.GetInt("SavedCar", 0);
    }

}
