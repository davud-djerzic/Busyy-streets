using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] RectTransform pausePanelRect, pauseButtonRect, timerRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Animator transitionAnim;
    [SerializeField] GameObject blackImage;
    public static bool isPause;
    public static bool isStart;

    public void Pause() // funkcija koja se poziva klikom na pause button
    {
        PausePanelIntro(); 
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPause = true;
    }

    public void Home() // funkcija koja se poziva klikom na home button
    {
        SceneManager.LoadScene(0); // vraca na main menu
        Time.timeScale = 1; // seta vrijednost timeScale na 1
        Timer.elapsedTime = 0; // vraca vrijeme proteklo za igru na 0

    }

    public void Restart() 
    {
        PlayerPrefs.SetInt("Streak", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloada level
        Time.timeScale = 1; // seta vrijednost timeScale na 1
        Timer.elapsedTime = 0; // vraca vrijeme proteklo za igru na 0

    }

    public async void Resume()
    {
        await PausePanelOutro(); // koristi async function
        pauseMenu.SetActive(false); 
        Time.timeScale = 1;
        isPause = false;
    }
    public void NextLevel()
    {
        StartCoroutine(LoadLevel()); 
    }

    IEnumerator LoadLevel()
    {
        blackImage.SetActive(true); 
        transitionAnim.SetTrigger("End"); // pokrece se animacija za kraj levela
        yield return new WaitForSeconds(1); // zaustavlja se protok igrice na sekundu
        if (PlayerPrefs.GetInt("Saved", 1) == 37 && PlayerPrefs.GetInt("CompletedGame", 0) == 0)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); // loada se novi level
        } else if (PlayerPrefs.GetInt("Saved", 1) == 37 && PlayerPrefs.GetInt("CompletedGame", 0) == 1)
        {
            if (PlayerPrefs.GetInt("ChoosenLevel", 1) == 17)
            {
                SceneManager.LoadScene(0);
            } else
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        else if (PlayerPrefs.GetInt("Saved", 1) < 37)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); // loada se novi level
        }

        transitionAnim.SetTrigger("Start"); // pokrece se animacija za pocetak levela
    }

    public void PausePanelIntro()
    {
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true); // desava se fade radi animacije kada se pause panel pojavljuje
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true); // animacija za silazak pause panela
        pauseButtonRect.DOAnchorPosX(253, tweenDuration).SetUpdate(true); // animacija za nestajanje pauseButtona sa screena
        timerRect.DOAnchorPosX(-300, tweenDuration).SetUpdate(true); // animacija za nestajanje timera sa screena
    }

    public async Task PausePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true); // desava se fade radi animacije kada se pause panel nestaje
        await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion(); // animacija za nestajanja pause panela
        pauseButtonRect.DOAnchorPosX(-309, tweenDuration).SetUpdate(true); // animacija za vracanje pauseButtona na screen
        timerRect.DOAnchorPosX(184, tweenDuration).SetUpdate(true); // animacija za vracanje timera na screen
    }


    public void NextLevelIntro() // ovo je funkcija koja se poziva kada se zavrsi level, kada se faila level i kada se desi sudar 2 auta 
    { // jer tada nema potrebe za nestajanje pauseButtona sa screena i timera
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);
    }

    public async Task OutroPanel()
    {
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true); // desava se fade radi animacije kada se pause panel nestaje
        await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion(); // animacija za nestajanja pause panela
    }
}
