using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Threading.Tasks;
using System.Linq;

public class guideOfGame : MonoBehaviour
{
    public GameObject[] guidePanels;
    public static int currentSlide;

    public GameObject mainPanel;
    [SerializeField] RectTransform mainGuidePanelRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup;



    private void Start()
    {
        activateFirstPanel();
    }

    public void activateFirstPanel()
    {
        guidePanels[0].SetActive(true);
        for (int i = 1; i < guidePanels.Length; i++)
        {
            guidePanels[i].SetActive(false);
        }
        currentSlide = 0;
    }

    public void changeSlide(int number) // ova funkcija se poziva kada se klikne button nextCar u Garage scene
    {
        currentSlide = currentSlide + number; // mijenja se trenutno aktivno auto prema gore
        if (currentSlide <= -1)
        {
            currentSlide = 0;
        } 
        if (currentSlide >= guidePanels.Length)
        {
            currentSlide = guidePanels.Length - 1;
        }
        
        /*if (PlayerPrefs.GetInt("FinishGuideOfGame", 0) == 0)
        {
            if (currentSlide >= guidePanels.Length - 1)
            {
                PlayerPrefs.SetInt("FinishGuideOfGame", 1);
                PlayerPrefs.SetInt("Achievement17", 1);
                //PlayerPrefs.SetInt("")
            }
        }*/
    }

    public void activeSlide()
    {
        for (int i = 0; i < guidePanels.Length; i++)
        {
            if (i == currentSlide)
            {
                guidePanels[i].SetActive(true);
            }
            else
            {
                guidePanels[i].SetActive(false);
            }
        }
    }

    public void PausePanelIntro()
    {
        mainPanel.SetActive(true);
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true); // desava se fade radi animacije kada se pause panel pojavljuje
        mainGuidePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true); // animacija za silazak pause panela
    }

    public async Task PausePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true); // desava se fade radi animacije kada se pause panel nestaje
        await mainGuidePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion(); // animacija za nestajanja pause panel
    }

    public async void PausePanelOutroVoid()
    {
        await PausePanelOutro();
        mainPanel.SetActive(false);
    }
}
