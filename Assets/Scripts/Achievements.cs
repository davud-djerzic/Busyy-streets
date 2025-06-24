using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    public GameObject[] achievementTickObject;
    private float numberOfAchievements;
    public Image fillImage;
    public TextMeshProUGUI percentage;

    public Image[] fillImageMastery;
    public TextMeshProUGUI[] percentageMastery;

    private void Awake()
    {
        for (int i = 0; i < achievementTickObject.Length; i++)
        {
            if (PlayerPrefs.GetInt("Achievement" + i) == 1) 
            {
                achievementTickObject[i].SetActive(true);
                numberOfAchievements++;
            }
        }
        if (PlayerPrefs.GetInt("Achievement0", 0) == 1)
        {
            if (PlayerPrefs.GetInt("FinishedAchievement0", 0) == 0)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 100);
                fillImageMastery[0].fillAmount = 1; // make instagram slider full
                percentageMastery[0].text = "100%"; // make instagram text 100%
                PlayerPrefs.SetInt("FinishedAchievement0", 1);
            } else
            {
                fillImageMastery[0].fillAmount = 1; // make instagram slider full
                percentageMastery[0].text = "100%"; // make instagram text 100%
            }
        }
        if (PlayerPrefs.GetInt("Achievement1", 0) == 1)
        {
            if (PlayerPrefs.GetInt("FinishedAchievement1", 0) == 0)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 100);
                fillImageMastery[1].fillAmount = 1; // make instagram slider full
                percentageMastery[1].text = "100%"; // make instagram text 100%
                PlayerPrefs.SetInt("FinishedAchievement1", 1);
            } else
            {
                fillImageMastery[1].fillAmount = 1; // make instagram slider full
                percentageMastery[1].text = "100%"; // make instagram text 100%
            }
        }
        if (PlayerPrefs.GetInt("Achievement17", 0) == 1)
        {
            fillImageMastery[2].fillAmount = 1; // make instagram slider full
            percentageMastery[2].text = "100%"; // make instagram text 100%
        }

        float solvedAchievements = numberOfAchievements / 17;
        float percentageOfSolvedAchievements = solvedAchievements * 100;
        fillImage.fillAmount = solvedAchievements;
        percentage.text = Mathf.FloorToInt(percentageOfSolvedAchievements).ToString() + "%";
    }
}
