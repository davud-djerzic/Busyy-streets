using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarSelection1 : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    public ShopItemList ShopItemList;
    public static int currentCar;

    private void Start()
    {
        GameObject gameObject = Profile.Instance.instantiatedCars[PlayerPrefs.GetInt("SavedCar", 0)]; // uzima auto koji je zadnje koristeno
        Button button = gameObject.GetComponentInChildren<Button>(); // dohvaca button tog auta
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>(); // dohvaca text tog buttona
        button.interactable = false; // button se gasi
        buttonText.text = "Used"; // ispis teksta da je auto koristeno
    }

    public void changeCarPlus(int number) // ova funkcija se poziva kada se klikne button nextCar u Garage scene
    {
        currentCar = currentCar + number; // mijenja se trenutno aktivno auto prema gore
        Profile.Instance.selectCar(currentCar); // poziva se selectCar i selektuje se auto sa indexom koji odgovara currentCar
    }

    public void changeCarMinus(int number) // ova funkcija se poziva kada se klikne button previousCar u Garage scene
    {
        currentCar = currentCar - number;
        Profile.Instance.selectCar(currentCar);
    }

    public void useCar() // ova funkcija se poziva kada se pritisne use button u Garage scene
    {
        //Debug.Log(Profile.Instance.instantiatedCars.Count);

        for (int i = 0; i < Profile.Instance.instantiatedCars.Count; i++) // prolazi kroz listu kloniranih auta
        {
            //Debug.Log(Profile.Instance.instantiatedCars[i].name);
            if (i == currentCar) // ako se index kloniranog auta odgovara indexu trenutnog auta, postavlja to auta kao trenutno auta
            {
                GameObject gameObject = Profile.Instance.instantiatedCars[currentCar];
                Button button = gameObject.GetComponentInChildren<Button>();
                TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
                button.interactable = false;
                buttonText.text = "Used";
                PlayerPrefs.SetInt("SavedCar", currentCar); // save trenutno koristenog auta
                PlayerPrefs.SetString("CarSpriteName", Profile.Instance.listOfCars[PlayerPrefs.GetInt("SavedCar", 0)].image.name);
                //PlayerPrefs.SetString("CarSpriteName", ShopItemList.shopItemList[PlayerPrefs.GetInt("SavedCar", 0)].image.name);

                PlayerPrefs.Save();
                //Debug.Log(PlayerPrefs.GetInt("SavedCar"));
                //Debug.Log(PlayerPrefs.GetString("CarSpriteName"));
            }
            else 
            {
                GameObject gameObject = Profile.Instance.instantiatedCars[i];
                Button button = gameObject.GetComponentInChildren<Button>();
                TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
                button.interactable = true;
                buttonText.text = "Use";
            } 
        }
    }
}
