using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayedTime : MonoBehaviour
{
    public GameObject darkPanel;
    public GameObject achievementObject;
    public AchievementAnimation AchievementAnimation;
    public static bool isOkayToStartCount;

    public void finishTimeAchievement()
    {
        /*if (PlayerPrefs.GetFloat("PlayedTime", 0) == 60 && PlayerPrefs.GetInt("finishedActionPlayedTime1", 0) == 0) // ako smo kupili prvo auto
        {
            AchievementAnimation.earnedCoins1 = 25;
            PlayerPrefs.SetInt("finishedActionPlayedTime1", 1);  // onemogucuje ponovo pokretanje ovog if statementa
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 25);
            setAchievementBehaviour("Achievement12", "finishedActionPlayedTime1", "TIME MASTERY I", "You have successfully passed mastery I");
        } else if (PlayerPrefs.GetFloat("PlayedTime", 0) < 60)
        {
            PlayerPrefs.SetFloat("percentageOfPlayedTime", PlayerPrefs.GetFloat("PlayedTime", 0) / 60.0f);
        }

        if (PlayerPrefs.GetFloat("PlayedTime", 0) == 90 && PlayerPrefs.GetInt("finishedActionPlayedTime2", 0) == 0) // ako smo kupili prvo auto
        {
            AchievementAnimation.earnedCoins1 = 50;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 50);
            PlayerPrefs.SetInt("finishedActionPlayedTime2", 1);  // onemogucuje ponovo pokretanje ovog if statementa

            setAchievementBehaviour("Achievement13", "finishedActionPlayedTime2", "TIME MASTERY II", "You have successfully passed mastery II");
        }
        else if (PlayerPrefs.GetFloat("PlayedTime", 0) < 90 && PlayerPrefs.GetFloat("PlayedTime", 0) >= 60)
        {
            PlayerPrefs.SetFloat("percentageOfPlayedTime", (PlayerPrefs.GetFloat("PlayedTime", 0) - 60.0f) / (90.0f - 60.0f));
        }

        if (PlayerPrefs.GetFloat("PlayedTime", 0) == 120 && PlayerPrefs.GetInt("finishedActionPlayedTime3", 0) == 0) // ako smo kupili prvo auto
        {
            AchievementAnimation.earnedCoins1 = 100;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 100);
            PlayerPrefs.SetInt("finishedActionPlayedTime3", 1);  // onemogucuje ponovo pokretanje ovog if statementa

            setAchievementBehaviour("Achievement14", "finishedActionPlayedTime3", "TIME MASTERY III", "You have successfully passed mastery III");
        }
        else if (PlayerPrefs.GetFloat("PlayedTime", 0) < 120 && PlayerPrefs.GetFloat("PlayedTime", 0) >= 90)
        {
            PlayerPrefs.SetFloat("percentageOfPlayedTime", (PlayerPrefs.GetFloat("PlayedTime", 0) - 90.0f) / (120.0f - 90.0f));
        }

        if (PlayerPrefs.GetFloat("PlayedTime", 0) == 150 && PlayerPrefs.GetInt("finishedActionPlayedTime4", 0) == 0) // ako smo kupili prvo auto
        {
            AchievementAnimation.earnedCoins1 = 150;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 150);
            PlayerPrefs.SetInt("finishedActionPlayedTime4", 1);  // onemogucuje ponovo pokretanje ovog if statementa

            setAchievementBehaviour("Achievement15", "finishedActionPlayedTime4", "TIME MASTERY IV", "You have successfully passed mastery IV");
        }
        else if (PlayerPrefs.GetFloat("PlayedTime", 0) < 150 && PlayerPrefs.GetFloat("PlayedTime", 0) >= 120)
        {
            PlayerPrefs.SetFloat("percentageOfPlayedTime", (PlayerPrefs.GetFloat("PlayedTime", 0) - 120.0f) / (150.0f - 120.0f));
        }


        if (PlayerPrefs.GetFloat("PlayedTime", 0) <= 180 && PlayerPrefs.GetFloat("PlayedTime", 0) >= 150 && PlayerPrefs.GetInt("finishedActionPlayedTime5", 0) == 0)
        {
            PlayerPrefs.SetFloat("percentageOfPlayedTime", (PlayerPrefs.GetFloat("PlayedTime", 0) - 150.0f) / (180.0f - 150.0f));
        }
        if (PlayerPrefs.GetFloat("PlayedTime", 0) == 180 && PlayerPrefs.GetInt("finishedActionPlayedTime5", 0) == 0) // ako smo kupili prvo auto
        {
            AchievementAnimation.earnedCoins1 = 200;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 200);
            PlayerPrefs.SetInt("finishedActionPlayedTime5", 1);  // onemogucuje ponovo pokretanje ovog if statementa
            PlayerPrefs.SetInt("Achievement16", 1);
            darkPanel.gameObject.SetActive(true); // pogledati kasnije
            achievementObject.SetActive(true);
            AchievementAnimation.PausePanelIntro("TIME MASTERY V", "You have successfully passed mastery V");
            isOkayToStartCount = true;

        }*/

    }

    private void setAchievementBehaviour(string achievementNumber, string finishedAction, string title, string message)
    {
        PlayerPrefs.SetInt(achievementNumber, 1);
        PlayerPrefs.SetInt("indexOfAchievementPanelPlayedTime", PlayerPrefs.GetInt("indexOfAchievementPanelPlayedTime", 0) + 1);  // povecava se index i sakriva se mastery II a pojavljuje se Mastery III
        PlayerPrefs.SetInt(finishedAction, 1);  // onemogucuje ponovo pokretanje ovog if statementa

        darkPanel.gameObject.SetActive(true); // pogledati kasnije
        achievementObject.SetActive(true);
        AchievementAnimation.PausePanelIntro(title, message);
        isOkayToStartCount = true;
       // Debug.Log(PlayerPrefs.GetInt("indexOfAchievementPanelPlayedTime", 0));
    }
}
