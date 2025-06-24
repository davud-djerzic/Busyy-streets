using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TimerChangeUsername : MonoBehaviour 
{ 
    public static TimerChangeUsername Instance { get; private set; }
    private float timeElapsed;

    public static bool isOkayToStartCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
       
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        if (seconds > 9)
        {
            ChangeUsername.isAllowToChangeUsername = true;
            timeElapsed = 0;
            PlayerPrefs.SetFloat("PlayedTime", PlayerPrefs.GetFloat("PlayedTime", 0) + seconds);
            finishTimeAchievement();
        }
       // Debug.Log(seconds);
        //Debug.Log(PlayerPrefs.GetFloat("PlayedTime", 0));
    }

    public float GetTimeElapsed()
    {
        return Mathf.FloorToInt(timeElapsed);
    }


    public void finishTimeAchievement()
    {
        if (PlayerPrefs.GetFloat("PlayedTime", 0) == 60 && PlayerPrefs.GetInt("finishedActionPlayedTime1", 0) == 0) // ako smo kupili prvo auto
        {
            AchievementAnimation.earnedCoins1 = 25;
            PlayerPrefs.SetInt("finishedActionPlayedTime1", 1);  // onemogucuje ponovo pokretanje ovog if statementa
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 25);
            setAchievementBehaviour("Achievement12", "finishedActionPlayedTime1", "TIME MASTERY I", "You have successfully passed mastery I");
        }
        else if (PlayerPrefs.GetFloat("PlayedTime", 0) < 60)
        {
            PlayerPrefs.SetFloat("percentageOfPlayedTime", PlayerPrefs.GetFloat("PlayedTime", 0) / 60.0f);
        }

        if (PlayerPrefs.GetFloat("PlayedTime", 0) == 180 && PlayerPrefs.GetInt("finishedActionPlayedTime2", 0) == 0) // ako smo kupili prvo auto
        {
            AchievementAnimation.earnedCoins1 = 50;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 50);
            PlayerPrefs.SetInt("finishedActionPlayedTime2", 1);  // onemogucuje ponovo pokretanje ovog if statementa

            setAchievementBehaviour("Achievement13", "finishedActionPlayedTime2", "TIME MASTERY II", "You have successfully passed mastery II");
        }
        else if (PlayerPrefs.GetFloat("PlayedTime", 0) < 180 && PlayerPrefs.GetFloat("PlayedTime", 0) >= 60)
        {
            PlayerPrefs.SetFloat("percentageOfPlayedTime", (PlayerPrefs.GetFloat("PlayedTime", 0) - 60.0f) / (180.0f - 60.0f));
        }

        if (PlayerPrefs.GetFloat("PlayedTime", 0) == 360 && PlayerPrefs.GetInt("finishedActionPlayedTime3", 0) == 0) // ako smo kupili prvo auto
        {
            AchievementAnimation.earnedCoins1 = 100;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 100);
            PlayerPrefs.SetInt("finishedActionPlayedTime3", 1);  // onemogucuje ponovo pokretanje ovog if statementa

            setAchievementBehaviour("Achievement14", "finishedActionPlayedTime3", "TIME MASTERY III", "You have successfully passed mastery III");
        }
        else if (PlayerPrefs.GetFloat("PlayedTime", 0) < 360 && PlayerPrefs.GetFloat("PlayedTime", 0) >= 180)
        {
            PlayerPrefs.SetFloat("percentageOfPlayedTime", (PlayerPrefs.GetFloat("PlayedTime", 0) - 180.0f) / (360.0f - 180.0f));
        }

        if (PlayerPrefs.GetFloat("PlayedTime", 0) == 660 && PlayerPrefs.GetInt("finishedActionPlayedTime4", 0) == 0) // ako smo kupili prvo auto
        {
            AchievementAnimation.earnedCoins1 = 150;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 150);
            PlayerPrefs.SetInt("finishedActionPlayedTime4", 1);  // onemogucuje ponovo pokretanje ovog if statementa

            setAchievementBehaviour("Achievement15", "finishedActionPlayedTime4", "TIME MASTERY IV", "You have successfully passed mastery IV");
        }
        else if (PlayerPrefs.GetFloat("PlayedTime", 0) < 660 && PlayerPrefs.GetFloat("PlayedTime", 0) >= 360)
        {
            PlayerPrefs.SetFloat("percentageOfPlayedTime", (PlayerPrefs.GetFloat("PlayedTime", 0) - 360.0f) / (660.0f - 360.0f));
        }


        if (PlayerPrefs.GetFloat("PlayedTime", 0) <= 1080 && PlayerPrefs.GetFloat("PlayedTime", 0) >= 660 && PlayerPrefs.GetInt("finishedActionPlayedTime5", 0) == 0)
        {
            PlayerPrefs.SetFloat("percentageOfPlayedTime", (PlayerPrefs.GetFloat("PlayedTime", 0) - 660.0f) / (1080.0f - 660.0f));
        }
        if (PlayerPrefs.GetFloat("PlayedTime", 0) == 1080 && PlayerPrefs.GetInt("finishedActionPlayedTime5", 0) == 0) // ako smo kupili prvo auto
        {
            AchievementAnimation.earnedCoins1 = 200;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 200);
            PlayerPrefs.SetInt("finishedActionPlayedTime5", 1);  // onemogucuje ponovo pokretanje ovog if statementa
            PlayerPrefs.SetInt("Achievement16", 1);
            isOkayToStartCount = true;
        }

    }

    private void setAchievementBehaviour(string achievementNumber, string finishedAction, string title, string message)
    {
        PlayerPrefs.SetInt(achievementNumber, 1);
        PlayerPrefs.SetInt("indexOfAchievementPanelPlayedTime", PlayerPrefs.GetInt("indexOfAchievementPanelPlayedTime", 0) + 1);  // povecava se index i sakriva se mastery II a pojavljuje se Mastery III
        PlayerPrefs.SetInt(finishedAction, 1);  // onemogucuje ponovo pokretanje ovog if statementa
    }
}
