using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshould;

    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;

    [SerializeField] Button previousBtn, nextBtn;
    [SerializeField] ScrollRect scrollRect;

    float pageWidth;
    float contentStartX;

    [SerializeField] float[] pagePositions; // npr. {130f, 490f, 850f}
    public void Awake()
    {
        /*currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshould = Screen.width / 15;
        UpdateBar();
        UpdateArrowButton();
        pageWidth = ((RectTransform)scrollRect.viewport).rect.width;
        contentStartX = levelPagesRect.anchoredPosition.x;*/
        //dragThreshould = Screen.width / 15;
        currentPage = 1;
        // targetPos = Vector3.zero;
        targetPos = levelPagesRect.localPosition;
        // Ako želiš automatski da raèunaš širinu koraka izmeðu stranica
       // pageStep = new Vector3(-levelPagesRect.rect.width, 0f, 0f); // ide u lijevo

        MovePage();
        UpdateBar();
        UpdateArrowButton();
    }

    void MovePage()
    {
        if (currentPage < 1) currentPage = 1;
        if (currentPage > maxPage) currentPage = maxPage;

        float targetX = pagePositions[currentPage - 1];
        targetPos = new Vector3(-targetX, targetPos.y, targetPos.z);

        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrowButton();
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            MovePage();
        }
    }
    /* IEnumerator AnimateScrollAnchoredPosition(Vector2 start, Vector2 end, float duration)
     {
         float elapsed = 0f;
         while (elapsed < duration)
         {
             elapsed += Time.deltaTime;
             float t = Mathf.Clamp01(elapsed / duration);
             levelPagesRect.anchoredPosition = Vector2.Lerp(start, end, t);
             yield return null;
         }
         levelPagesRect.anchoredPosition = end;
     }

     void MovePage()
     {
         currentPage = Mathf.Clamp(currentPage, 1, maxPage);

         // Ove pozicije ti možeš definisati ili izvuæi iz layouta
         float[] pagePositions = { 130f, 490f, 850f };

         float targetX = pagePositions[currentPage - 1];

         Vector2 start = levelPagesRect.anchoredPosition;
         Vector2 end = new Vector2(-targetX, start.y);

         StopAllCoroutines();
         StartCoroutine(AnimateScrollAnchoredPosition(start, end, tweenTime));

         UpdateBar();
         UpdateArrowButton();
     }



     public void Next()
     {
         if (currentPage < maxPage)
         {
             currentPage++;
             MovePage();
         }
     }

     public void Previous()
     {
         if (currentPage > 1)
         {
             currentPage--;
             MovePage();
         }
     }*/

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) Previous();
            else Next();
        } else
        {
            MovePage();
        }
    }

    void UpdateBar()
    {
        foreach (var item in barImage)
        {
            item.sprite = barClosed;
        }
        barImage[currentPage - 1].sprite = barOpen;
    }

    void UpdateArrowButton()
    {
        previousBtn.interactable = true;
        nextBtn.interactable = true;
        if (currentPage == 1) previousBtn.interactable = false;
        else if (currentPage == maxPage) nextBtn.interactable = false;
    }
    public void ResetBehaviour() // poziva se na klik dugme drive
    {
        currentPage = 1;
        scrollRect.horizontalNormalizedPosition = 0f;
        UpdateBar();
        UpdateArrowButton();
    }
}
