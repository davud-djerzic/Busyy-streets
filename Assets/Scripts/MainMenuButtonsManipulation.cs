using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class MainMenuButtonsManipulation : MonoBehaviour
{
    public GameObject canvasAchievement;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] RectTransform pausePanelRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup;

    public void openCanvasAchievement()
    {
        //.SetActive(true);
        NextLevelIntro();
    }

    public async void closeCanvasAchievement()
    {
        //canvasAchievement.SetActive(false);
        await OutroPanel();
    }

    public void NextLevelIntro() // ovo je funkcija koja se poziva kada se zavrsi level, kada se faila level i kada se desi sudar 2 auta 
    { // jer tada nema potrebe za nestajanje pauseButtona sa screena i timera
        pauseMenu.SetActive(true);
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);

    }

    public async Task OutroPanel()
    {
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true); // desava se fade radi animacije kada se pause panel nestaje
        await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion(); // animacija za nestajanja pause panela
        canvasGroup.gameObject.SetActive(false);
    }
}
