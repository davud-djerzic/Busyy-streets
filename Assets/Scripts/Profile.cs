using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    #region Singlton:Profile
    public static Profile Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        //currentCar = PlayerPrefs.GetInt("SavedCar", 0); pogledati kasnije
        
        // ako je kupljeno auta sa manjim indeksom od trenutno koristenog auta, uveca se index za 1 trenutnog auta 
        // jer je kupljeno auto dodano u listu prije koristenog auta
        if (PlayerPrefs.GetInt("NeedToIncreaseSavedCar") == 1) 
        {
            PlayerPrefs.SetInt("SavedCar", PlayerPrefs.GetInt("SavedCar", 0) + 1);
            PlayerPrefs.SetInt("NeedToIncreaseSavedCar", 0); // vraca se na 0, da se ne izvrsava ne potrebno jer se u shop listi setuje na 1 ako ima potrebe za to
        }
        
        getAvailableAvatars(); 

        selectCar(PlayerPrefs.GetInt("SavedCar", 0)); // aktivira zadnje koristeno auto
        //currentCar = 0;
    }

    #endregion

    public class Garage
    {
        public Sprite image;
    }

    public ShopItemList ShopItemList;

    public List<Garage> listOfCars;
    [SerializeField] GameObject carUITemplate;
    [SerializeField] Transform carScrollView;
    [SerializeField] TMP_Text coinsText;

    GameObject g;
    Sprite car;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    public static int currentCar;

    public List<GameObject> instantiatedCars;

    private void Start()
    {
        if (listOfCars.Count > 1) // ako se vise od 1 auta nalazi u listu, omogucavaju se buttoni
        {
            previousButton.interactable = false;
            nextButton.interactable = true;
            previousButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
        }
        else // ako se samo 1 auto nalazi u listu, omogucavaju se buttoni
        {
            previousButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false); 
        }

        coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString(); 

        
        //selectCar(0);

        selectCar(PlayerPrefs.GetInt("SavedCar", 0)); // pogledati kasnije
    }

    void getAvailableAvatars()
    {
        for (int i = 0; i < ShopItemList.shopItemList.Count; i++) // prolazi kroz sva dostupna auta u shopu
        {
            if (PlayerPrefs.HasKey("BuyCar" + i)) // provjerava da li je odredjeno auto kupljeno
            {
                //string spriteName = PlayerPrefs.GetString("autoSprite" + i);
                //GameObject carPrefab = Resources.Load<GameObject>("Assets/2D_urban_cars/prefabs/" + spriteName);

                addCar(ShopItemList.shopItemList[i].image); // dodaje sliku auta u garage
            }
                
        }
    }

    void addCar(Sprite img)
    {
        if (listOfCars == null)
        {
            listOfCars = new List<Garage>();
        }

        Garage gar = new Garage();
        gar.image = img;

        listOfCars.Add(gar); // dodaje auto u listu za dalju upotrebu u CarSelection1

        g = Instantiate(carUITemplate, carScrollView); // pravi klon auta
        //SpriteRenderer spriteRenderer = carPrefab.GetComponent<SpriteRenderer>();

        g.transform.GetChild(0).GetComponent<Image>().sprite = gar.image; // postavlja sliku kloniranog objekta kao auto

        if (instantiatedCars == null)
            instantiatedCars = new List<GameObject>();

        instantiatedCars.Add(g); // dodaje klonirani objekat u listu za dalju upotrebu
    }

    public void selectCar(int index)
    {
        previousButton.interactable = (index != 0); // ako je prvo auto da ne moze ici lijevo
        nextButton.interactable = (index < listOfCars.Count - 1); // ako je zadnje auto da ne moze ici desno

        for (int i = 0; i < listOfCars.Count; i++) // prolazi kroz listu auta u garagi
        {
            GameObject gameObject = instantiatedCars[i]; // uzima klonirani objekat
            
            gameObject.SetActive(i == index); // postavlja samo aktivan jedno auto u zavisnosti od prosljedjenog indexa


        }
    }


}
