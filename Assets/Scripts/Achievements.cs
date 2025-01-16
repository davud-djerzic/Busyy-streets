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
            fillImageMastery[0].fillAmount = 1; // make instagram slider full
            percentageMastery[0].text = "100%"; // make instagram text 100%
        }
        if (PlayerPrefs.GetInt("Achievement1", 0) == 1)
        {
            fillImageMastery[1].fillAmount = 1; // make instagram slider full
            percentageMastery[1].text = "100%"; // make instagram text 100%
        }
        if (PlayerPrefs.GetInt("Achievement17", 0) == 1)
        {
            fillImageMastery[2].fillAmount = 1; // make instagram slider full
            percentageMastery[2].text = "100%"; // make instagram text 100%
        }

        float solvedAchievements = numberOfAchievements / achievementTickObject.Length;
        float percentageOfSolvedAchievements = solvedAchievements * 100;
        /*Debug.Log(solvedAchievements);
        Debug.Log(percentageOfSolvedAchievements);*/
        fillImage.fillAmount = solvedAchievements;
        percentage.text = Mathf.FloorToInt(percentageOfSolvedAchievements).ToString() + "%";
    }
}
