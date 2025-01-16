using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManipulateAchievementsPanel : MonoBehaviour
{
    public GameObject[] achievementPanels;
    public Image[] fillImage;
    public TextMeshProUGUI[] percentage;

    // Start is called before the first frame update
    void Start()
    {
        manipulateAchievements("indexOfAchievementPanelPlayedTime");
    }

    public void manipulateAchievements(string PlayerPrefsString)
    {
        for (int i = 0; i < achievementPanels.Length; i++)
        {
            if (i == PlayerPrefs.GetInt(PlayerPrefsString, 0))
            {
                achievementPanels[i].SetActive(true);
                if (PlayerPrefsString == "indexOfAchievementPanelCheckWin")
                {
                    fillImage[i].fillAmount = PlayerPrefs.GetFloat("percentageOfSolvedLevel", 0);
                    percentage[i].text = Mathf.FloorToInt((PlayerPrefs.GetFloat("percentageOfSolvedLevel", 0) * 100)).ToString() + "%";
                }

                if (PlayerPrefsString == "indexOfAchievementPanelShop")
                {
                    fillImage[i].fillAmount = PlayerPrefs.GetFloat("percentageOfBoughtCar", 0);
                    percentage[i].text = Mathf.FloorToInt((PlayerPrefs.GetFloat("percentageOfBoughtCar", 0) * 100)).ToString() + "%";
                }
                if (PlayerPrefsString == "indexOfAchievementPanelPlayedTime")
                {
                    fillImage[i].fillAmount = PlayerPrefs.GetFloat("percentageOfPlayedTime", 0);
                    percentage[i].text = Mathf.FloorToInt((PlayerPrefs.GetFloat("percentageOfPlayedTime", 0) * 100)).ToString() + "%";
                }
            }
            else
            {
                achievementPanels[i].SetActive(false);
            }
        }
    }
    
}
