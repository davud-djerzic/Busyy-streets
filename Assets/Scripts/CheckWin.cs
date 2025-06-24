using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class CheckWin : MonoBehaviour
{
    public Button[] buttons;

    public GameObject restartLevelPanel;
    public PauseMenu pauseMenu;

    public GameObject winLosePanel;
    public PauseMenu winLosePanelPauseMenu;
    public TextMeshProUGUI winLoseText;
    public TextMeshProUGUI coinsText;

    public AchievementAnimation AchievementAnimation;
    public GameObject achievementObject;
    public GameObject darkPanel;

    public static bool isOkayToStartCount;

    private List<int> correctSequence = new List<int>();
    private List<int> inputSequence = new List<int>();
    public bool isWin = false;

    private int index = 0;
    public static int coins = 0;
    public static int streak;


    private static int counterLevel;
    public static int indexOfAchievementPanel;

    public static bool isNewLevelFinished = false;

    [SerializeField] TextMeshProUGUI bestTimeText;
    void Start()
    {
        isNewLevelFinished = false;
        restartLevelPanel.SetActive(false);

        //winText.text = "";

        for (int i = 0; i < buttons.Length; i++)
        {
            correctSequence.Add(i + 1); // 1, 2, 3
            int index = i + 1;
            buttons[i].onClick.AddListener(() => ButtonClicked(index));
        }

        coins = PlayerPrefs.GetInt("Coins", 0);
        streak = PlayerPrefs.GetInt("Streak", 0);
    }

    void ButtonClicked(int buttonNumber)
    {
        if (index < buttons.Length) // ako je trenutni index manji od broja auta
        {
            index++; // poveca index
            inputSequence.Add(buttonNumber); // dodaje taj broj u listu za kasniju provjeru sekvence

            if (index == buttons.Length)
            {
                if (IsSequenceCorrect()) // ako je uspjesno prijedjen level
                {
                    if (Timer.elapsedTime < PlayerPrefs.GetFloat("BestFinishTime", 1000)) // ako je igrac napravio highscore, taj highscore se save
                    {
                        PlayerPrefs.SetFloat("BestFinishTime", Timer.elapsedTime);
                       //Debug.Log(PlayerPrefs.GetFloat("BestFinishTime", 0));
                    }
                    BestTime();
                    //winText.text = "You Win!";
                    /* isWin = true;
                     coins += 1000;
                     streak++; // povecava se vrijednost streaka
                     if (streak == 4) // ako se 3 levela predju zaredom
                     {
                         coins += 5; // dobija se dodatnih 5 coinsa
                         streak = 1; // reseta se vrijednost streka
                     } 
                     Debug.Log("Streak: " + streak);

                     PlayerPrefs.SetInt("Streak", streak);

                     PlayerPrefs.SetInt("Coins", coins);*/
                    isWin = true;
                    //Debug.Log("Najbolje vrijeme: " + PlayerPrefs.GetFloat("BestFinishTime", 1000));
                    finishLevel();

                }
                else
                {
                    //winText.text = "You Lose!";
                    Invoke("resetLevel", 2.0f);
                    PlayerPrefs.SetInt("Streak", 0);
                }
            }
        }
        MovementCar.speed = 2.0f;
    }

    bool IsSequenceCorrect()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (inputSequence[i] != correctSequence[i])
            {
                return false;
            }
        }
        return true;
    }

    private void finishLevel()
    {

        //PlayerPrefs.SetInt("indexOfAchievementPanelShop", PlayerPrefs.GetInt("indexOfAchievementPanelShop", 0));
       // Debug.Log(PlayerPrefs.GetInt("indexOfAchievementPanelCheckWin"));
        
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex", 1))
        {
            PlayerPrefs.SetInt("numberOfSolvedLevel", PlayerPrefs.GetInt("numberOfSolvedLevel", 0) + 1);
            if (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) == 1 && PlayerPrefs.GetInt("finishedActionCheckWin1", 0) == 0)
            {
                AchievementAnimation.earnedCoins1 = 50;
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 50);
                PlayerPrefs.SetInt("finishedActionCheckWin1", 1);  // onemogucuje ponovo pokretanje ovog if statementa
                setAchievementBehaviour("Achievement7", "finishedActionCheckWin1", "LEVEL MASTERY I", "You have successfully passed mastery I");
            }
            /*else if (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) <= 4 && PlayerPrefs.GetInt("numberOfSolvedLevel", 0) >= 0)
            {
                PlayerPrefs.SetFloat("percentageOfSolvedLevel", (PlayerPrefs.GetInt("numberOfSolvedLevel", 0)) / 5.0f);
            }*/
            if (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) >= 4 && PlayerPrefs.GetInt("finishedActionCheckWin2", 0) == 0)
            {
                AchievementAnimation.earnedCoins1 = 100;
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 100);
                PlayerPrefs.SetInt("finishedActionCheckWin2", 1);  // onemogucuje ponovo pokretanje ovog if statementa
                setAchievementBehaviour("Achievement8", "finishedActionCheckWin2", "LEVEL MASTERY II", "You have successfully passed mastery II");
            }
            else if (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) <= 3 && PlayerPrefs.GetInt("numberOfSolvedLevel", 0) >= 1)
            {
                PlayerPrefs.SetFloat("percentageOfSolvedLevel", (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) - 1) / 3.0f);
            }
            if (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) >= 11 && PlayerPrefs.GetInt("finishedActionCheckWin3", 0) == 0)
            {
                AchievementAnimation.earnedCoins1 = 200;
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 200);
                PlayerPrefs.SetInt("finishedActionCheckWin3", 1);  // onemogucuje ponovo pokretanje ovog if statementa
                setAchievementBehaviour("Achievement9", "finishedActionCheckWin3", "LEVEL MASTERY III", "You have successfully passed mastery III");

            }
            else if (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) <= 10 && PlayerPrefs.GetInt("numberOfSolvedLevel", 0) >= 4)
            {
                PlayerPrefs.SetFloat("percentageOfSolvedLevel", (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) - 4) / 7.0f);
            }

            if (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) >= 21 && PlayerPrefs.GetInt("finishedActionCheckWin4", 0) == 0)
            {
                AchievementAnimation.earnedCoins1 = 500;
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 500);
                PlayerPrefs.SetInt("finishedActionCheckWin4", 1);
                setAchievementBehaviour("Achievement10", "finishedActionCheckWin4", "LEVEL MASTERY IV", "You have successfully passed mastery IV");

            }
            else if (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) <= 20 && PlayerPrefs.GetInt("numberOfSolvedLevel", 0) >= 11)
            {
                PlayerPrefs.SetFloat("percentageOfSolvedLevel", (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) - 11) / 10.0f);
            }

            if (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) >= 21 && PlayerPrefs.GetInt("numberOfSolvedLevel", 0) <= 35 && PlayerPrefs.GetInt("finishedActionCheckWin5", 0) == 0) // ako smo kupili manje od 5 auta
            {
                PlayerPrefs.SetFloat("percentageOfSolvedLevel", (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) - 21) / 15.0f); //  broji broj kupljenih auta sa 5 da bi dobili percentage koji se prosledjuju u drugu skriptu
            }
            if (PlayerPrefs.GetInt("numberOfSolvedLevel", 0) >= 36 && PlayerPrefs.GetInt("finishedActionCheckWin5", 0) == 0)
            {
                AchievementAnimation.earnedCoins1 = 1000;
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 1000);
                PlayerPrefs.SetFloat("percentageOfSolvedLevel", 1f);
                PlayerPrefs.SetInt("finishedActionCheckWin5", 1);  // onemogucuje ponovo pokretanje ovog if statementa
                PlayerPrefs.SetInt("Achievement11", 1);
                darkPanel.gameObject.SetActive(true); // pogledati kasnije
                achievementObject.SetActive(true);
                AchievementAnimation.PausePanelIntro("LEVEL MASTERY V", "You have successfully passed mastery V");
                isOkayToStartCount = true;
            }

            coins += 200;
            streak++; // povecava se vrijednost streaka
            if (streak == 4) // ako se 3 levela predju zaredom
            {
                coins += 100; // dobija se dodatnih 5 coinsa
                streak = 1; // reseta se vrijednost streka
            }
            //Debug.Log("Streak: " + streak);

            PlayerPrefs.SetInt("Streak", streak);

            PlayerPrefs.SetInt("Coins", coins);
            isNewLevelFinished = true;

            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Saved", PlayerPrefs.GetInt("Saved", 1) + 1);
            PlayerPrefs.Save();
        }
        
        //Debug.Log(PlayerPrefs.GetInt("Saved"));
        PlayerPrefs.Save();

    }

    private void resetLevel() // ova funkcija se poziva kada se faila level
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseMenu.NextLevelIntro();
        restartLevelPanel.SetActive(true);

        winLosePanelPauseMenu.NextLevelIntro();
        winLosePanel.SetActive(true);
        winLoseText.text = "You Lose!!!";
        coinsText.text = "+0";


        Timer.elapsedTime = 0;
        Timer.isOkayToCount = false;
    }

    private void setAchievementBehaviour(string achievementNumber, string finishedAction, string title, string message)
    {
        PlayerPrefs.SetInt(achievementNumber, 1);
        PlayerPrefs.SetInt("indexOfAchievementPanelCheckWin", PlayerPrefs.GetInt("indexOfAchievementPanelCheckWin", 0) + 1);  // povecava se index i sakriva se mastery II a pojavljuje se Mastery III
        PlayerPrefs.SetInt(finishedAction, 1);  // onemogucuje ponovo pokretanje ovog if statementa

        darkPanel.gameObject.SetActive(true); // pogledati kasnije
        achievementObject.SetActive(true);
        AchievementAnimation.PausePanelIntro(title, message);
        isOkayToStartCount = true;
    }

    private void BestTime()
    {
        float bestTime = PlayerPrefs.GetFloat("BestFinishTime", 1000f);
        bestTimeText.text = "Best Time: " + bestTime.ToString("F2") + "s";
    }
}
