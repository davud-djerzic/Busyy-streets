using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Shop;


public class Shop : MonoBehaviour
{
    #region Singlton:Shop
    public static Shop Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
    }

    #endregion

    /*[System.Serializable] public  class ShopItem
    {
        public Sprite image;
        public int price;
        public bool isPurchased;
    }*/

    public ShopItemList ShopItemList;
    //public List<ShopItem> shopItemList;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;

    [SerializeField] TMP_Text coinsText;
    [SerializeField] TMP_Text noCoinsText;

    [SerializeField] Animator noCoinsAnim;

    int indexOfSavedCar;
    public static int counterBoughtCar;
    public static int indexOfAchievementPanelShop;

    public AchievementAnimation AchievementAnimation;
    public GameObject achievementObject;
    public GameObject darkPanel;

    public static bool isOkayToStartCount;
    

    Button buyButton;
    void Start()
    {
        PlayerPrefs.SetInt("NeedToIncreaseSavedCar", 0); // seta vrijednost na 0, jer je ovo vrijednost koja treba biti 1 samo u jednom slucaju

        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int len = ShopItemList.shopItemList.Count;
        for (int i = 0; i < len; i++)
        {
            //PlayerPrefs.DeleteKey("Achievement" + i);
            g = Instantiate(ItemTemplate, ShopScrollView); // pravi clone objekta
            if (i == 0)
            {
                g.SetActive(false); // prvo auto se ne prikazuje u shopu jer je ono po defaultu kupljeno
            }
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemList.shopItemList[i].image; // postavlja sliku auta na objekat
            g.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = ShopItemList.shopItemList[i].price.ToString(); // ispisuje cijenu auta
            buyButton = g.transform.GetChild(2).GetComponent<Button>(); // uzima button
            
            if (PlayerPrefs.HasKey("BuyCar" + i)) // provjerava ako je to auto kupljeno 
            {
                setButtonBehaviour(i);

            } else {
                buyButton.interactable = true; // ako auto nije kupljeno button se moze koristiti
                buyButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Buy";

            }
           // buyButton.interactable = !shopItemList[i].isPurchased;
            
            buyButton.AddEventListener(i, onShopItemBtnClicked);

            if (ShopItemList.shopItemList[i].image.name == PlayerPrefs.GetString("CarSpriteName", "4x4_blue")) // trazi index auta koje je trenutno koristeno
            {
                indexOfSavedCar = i;
            }
            PlayerPrefs.DeleteKey("SavedCar");
            PlayerPrefs.DeleteKey("CarSpriteName");
            PlayerPrefs.DeleteKey("BuyCar" + i);
            PlayerPrefs.DeleteKey("numberOfBoughtCar");
            PlayerPrefs.DeleteKey("indexOfAchievementPanelShop");
            PlayerPrefs.DeleteKey("numberOfSolvedLevel");
            PlayerPrefs.DeleteKey("indexOfAchievementPanelCheckWin");
            PlayerPrefs.DeleteKey("finishedActionCheckWin1");
            PlayerPrefs.DeleteKey("finishedActionCheckWin2");
            PlayerPrefs.DeleteKey("finishedActionCheckWin3");
            PlayerPrefs.DeleteKey("finishedActionCheckWin4");
            PlayerPrefs.DeleteKey("finishedActionCheckWin5");
            PlayerPrefs.DeleteKey("finishedActionShop1");
            PlayerPrefs.DeleteKey("finishedActionShop2");
            PlayerPrefs.DeleteKey("finishedActionShop3");
            PlayerPrefs.DeleteKey("finishedActionShop4");
            PlayerPrefs.DeleteKey("finishedActionShop5");
            PlayerPrefs.DeleteKey("percentageOfBoughtCar");
            PlayerPrefs.DeleteKey("percentageOfSolvedLevel");
            PlayerPrefs.DeleteKey("percentageOfPlayedTime");
            PlayerPrefs.DeleteKey("finishedActionPlayedTime1");
            PlayerPrefs.DeleteKey("finishedActionPlayedTime2");
            PlayerPrefs.DeleteKey("finishedActionPlayedTime3");
            PlayerPrefs.DeleteKey("finishedActionPlayedTime4");
            PlayerPrefs.DeleteKey("finishedActionPlayedTime5");
            PlayerPrefs.DeleteKey("indexOfAchievementPanelPlayedTime");
            PlayerPrefs.SetInt("Saved", 2);
            PlayerPrefs.DeleteKey("Achievement" + i);
            PlayerPrefs.SetInt("Coins", 100000);
            PlayerPrefs.DeleteKey("PlayedTime");
            PlayerPrefs.DeleteKey("FinishGuideOfGame");
        }

        Destroy(ItemTemplate);

        UpdateALLCoinsUIText();

    }

    public void onShopItemBtnClicked(int itemIndex)
    {
        if (Game.instance.hasEnough(ShopItemList.shopItemList[itemIndex].price)) // provjerava da li posjedujemo dovoljno coinsa da kupimo auto
        {
            Game.instance.useCoins(ShopItemList.shopItemList[itemIndex].price); // oduzima na ukupnu sumu coinsa

            buyButton = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            setButtonBehaviour(itemIndex);

            PlayerPrefs.SetInt("BuyCar" + itemIndex, 1); // cuvamo kupljeno auto

            //PlayerPrefs.SetString("autoSprite" + itemIndex, shopItemList[itemIndex].image.name);

            PlayerPrefs.Save();

            UpdateALLCoinsUIText();

            if (itemIndex < indexOfSavedCar) // ako je index kupljenog auta manji od indexa trenutno koristenog auta, seta 1 jer mi je to potrebno u profile skirpti
            {
                PlayerPrefs.SetInt("NeedToIncreaseSavedCar", 1);
            }

            PlayerPrefs.SetInt("numberOfBoughtCar", PlayerPrefs.GetInt("numberOfBoughtCar", 0) + 1); // broji koliko smo prosli levela i pamti to
            if (PlayerPrefs.GetInt("numberOfBoughtCar", 0) == 1 && PlayerPrefs.GetInt("finishedActionShop1", 0) == 0) // ako smo kupili prvo auto
            {
                PlayerPrefs.SetInt("finishedActionShop1", 1);  // onemogucuje ponovo pokretanje ovog if statementa

                setAchievementBehaviour("Achievement2", "finishedActionShop1", "SHOP MASTERY I", "You have successfully passed mastery I");

            }
            if (PlayerPrefs.GetInt("numberOfBoughtCar", 0) >= 3 && PlayerPrefs.GetInt("finishedActionShop2", 0) == 0) // ako smo kupili vise od 3 auta
            {
                PlayerPrefs.SetInt("finishedActionShop2", 1);  // onemogucuje ponovo pokretanje ovog if statementa

                setAchievementBehaviour("Achievement3", "finishedActionShop2", "SHOP MASTERY II", "You have successfully passed mastery II");
            }
            else if (PlayerPrefs.GetInt("numberOfBoughtCar", 0) <= 2 && PlayerPrefs.GetInt("numberOfBoughtCar", 0) >= 1) // ako smo kupili manje od 3 auta
            {
                PlayerPrefs.SetFloat("percentageOfBoughtCar", (PlayerPrefs.GetInt("numberOfBoughtCar", 0) - 1) / 2.0f); // broji broj kupljenih auta sa 3 da bi dobili percentage koji se prosledjuju u drugu skriptu
            }

            if (PlayerPrefs.GetInt("numberOfBoughtCar", 0) >= 8 && PlayerPrefs.GetInt("finishedActionShop3", 0) == 0)  // ako smo kupili vise od 5 auta
            {
                PlayerPrefs.SetInt("finishedActionShop3", 1);  // onemogucuje ponovo pokretanje ovog if statementa                
                setAchievementBehaviour("Achievement4", "finishedActionShop3", "SHOP MASTERY III", "You have successfully passed mastery III");
            }
            else if (PlayerPrefs.GetInt("numberOfBoughtCar", 0) >= 3 && PlayerPrefs.GetInt("numberOfBoughtCar", 0) <= 7) // ako smo kupili manje od 5 auta
            {
                PlayerPrefs.SetFloat("percentageOfBoughtCar", (PlayerPrefs.GetInt("numberOfBoughtCar", 0) - 3) / 5.0f); //  broji broj kupljenih auta sa 5 da bi dobili percentage koji se prosledjuju u drugu skriptu
            }
            if (PlayerPrefs.GetInt("numberOfBoughtCar", 0) >= 15 && PlayerPrefs.GetInt("finishedActionShop4", 0) == 0)  // ako smo kupili vise od 5 auta
            {
                PlayerPrefs.SetInt("finishedActionShop4", 1);  // onemogucuje ponovo pokretanje ovog if statementa
                setAchievementBehaviour("Achievement5", "finishedActionShop4", "SHOP MASTERY IV", "You have successfully passed mastery IV");
            }
            else if (PlayerPrefs.GetInt("numberOfBoughtCar", 0) >= 8 && PlayerPrefs.GetInt("numberOfBoughtCar", 0) <= 14) // ako smo kupili manje od 5 auta
            {
                PlayerPrefs.SetFloat("percentageOfBoughtCar", (PlayerPrefs.GetInt("numberOfBoughtCar", 0) - 8) / 7.0f); //  broji broj kupljenih auta sa 5 da bi dobili percentage koji se prosledjuju u drugu skriptu
            }

            if (PlayerPrefs.GetInt("numberOfBoughtCar", 0) >= 15 && PlayerPrefs.GetInt("numberOfBoughtCar", 0) <= 25 && PlayerPrefs.GetInt("finishedActionShop5", 0) == 0) // ako smo kupili manje od 5 auta
            {
                PlayerPrefs.SetFloat("percentageOfBoughtCar", (PlayerPrefs.GetInt("numberOfBoughtCar", 0) - 15) / 10.0f); //  broji broj kupljenih auta sa 5 da bi dobili percentage koji se prosledjuju u drugu skriptu
            }
            if (PlayerPrefs.GetInt("numberOfBoughtCar", 0) >= 25 && PlayerPrefs.GetInt("finishedActionShop5", 0) == 0)
            {
                PlayerPrefs.SetInt("finishedActionShop5", 1);  // onemogucuje ponovo pokretanje ovog if statementa
                PlayerPrefs.SetInt("Achievement6", 1);
                darkPanel.gameObject.SetActive(true); // pogledati kasnije
                achievementObject.SetActive(true);
                AchievementAnimation.PausePanelIntro("SHOP MASTERY V", "You have successfully passed mastery V");
                isOkayToStartCount = true;
            }
            /*if (PlayerPrefs.GetInt("numberOfBoughtCar", 0) >= 9)
            {
                PlayerPrefs.SetInt("Achievement4", 1);

                achievementObject.SetActive(true);
                AchievementAnimation.PausePanelIntro("SHOP MASTERY III", "You have successfully passed mastery III");
                isOkayToStartCount = true;
            }*/
            //PlayerPrefs.SetInt("indexOfAchievementPanelShop", PlayerPrefs.GetInt("indexOfAchievementPanelShop", 0));
            Debug.Log(PlayerPrefs.GetInt("indexOfAchievementPanelShop"));
            Debug.Log(PlayerPrefs.GetFloat("percentageOfBoughtCar", 0));

        } else
        {
            noCoinsText.text = "No enough coins!";
            noCoinsAnim.SetTrigger("NoCoins"); // pokrecemo animaciju
        }
    }

    public void UpdateALLCoinsUIText()  
    {
        coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

    private void setButtonBehaviour(int itemIndex)
    {
        buyButton.interactable = false;
        buyButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Purchased";
        ShopItemList.shopItemList[itemIndex].isPurchased = true;
    }

    private void setAchievementBehaviour(string achievementNumber, string finishedAction, string title, string message)
    {
        PlayerPrefs.SetInt(achievementNumber, 1);
        PlayerPrefs.SetInt("indexOfAchievementPanelShop", PlayerPrefs.GetInt("indexOfAchievementPanelShop", 0) + 1);  // povecava se index i sakriva se mastery II a pojavljuje se Mastery III
        PlayerPrefs.SetInt(finishedAction, 1);  // onemogucuje ponovo pokretanje ovog if statementa

        darkPanel.gameObject.SetActive(true); // pogledati kasnije
        achievementObject.SetActive(true);
        AchievementAnimation.PausePanelIntro(title, message);
        isOkayToStartCount = true;
    }
}
