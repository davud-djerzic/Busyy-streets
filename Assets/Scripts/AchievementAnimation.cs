using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;

public class AchievementAnimation : MonoBehaviour
{
    [SerializeField] GameObject achievementMenu;
    [SerializeField] RectTransform achievementPanelRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI achievementText;
    [SerializeField] TextMeshProUGUI underAchievementText;
    [SerializeField] float earnedCoins;
    public static float earnedCoins1;
    [SerializeField] TextMeshProUGUI coinsText;
    public static float elapsedTime;

    private void Update()
    {
        if (CheckWin.isOkayToStartCount || Shop.isOkayToStartCount || PlayedTime.isOkayToStartCount) // kada se predje neki achievement ovo postaje true
        {
            elapsedTime += Time.deltaTime; // broji vrijeme
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            if (seconds >= 3) // ako je proslo 5 sekundi od prikazivanja achievementa 
            {
                CheckWin.isOkayToStartCount = false; // stavljamo ovo na false jer kada je true achievement se prikazuje u checkWin skripti
                Shop.isOkayToStartCount = false; // stavljamo ovo na false jer kada je true achievement se prikazuje u Shop skripti
                PlayedTime.isOkayToStartCount = false;
                hideAchievementPanel(); // sakrivamo achievement 
                elapsedTime = 0; // resetamo tajmer
            }
        }
    }

    public void PausePanelIntro(string achievementTextArgument, string underAchievementTextArgument)
    {
        //coinsText.text = $"+{earnedCoins.ToString()}";
        coinsText.text = $"+{earnedCoins1.ToString()}";
        achievementText.text = achievementTextArgument; // postavljamo tekst koji smo mastery presli
        underAchievementText.text = underAchievementTextArgument;
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true); // desava se fade radi animacije kada se pause panel pojavljuje
        achievementPanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true); // animacija za silazak pause panela
    }

    public async Task PausePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true); // desava se fade radi animacije kada se pause panel nestaje
        await achievementPanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion(); // animacija za nestajanja pause panela
    }

    public async void hideAchievementPanel()
    {
        await PausePanelOutro();
        achievementMenu.gameObject.SetActive(false);
    }
}
