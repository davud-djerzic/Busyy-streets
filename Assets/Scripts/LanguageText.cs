using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] bosnianText;
    public GameObject[] englishText;

    public void writeBosnianText()
    {
        for (int i = 0; i < bosnianText.Length; i++)
        {
            bosnianText[i].SetActive(true);
            englishText[i].SetActive(false);
        }


    }

    public void writeEnglishText()
    {
        for (int i = 0; i < englishText.Length; i++)
        {
            englishText[i].SetActive(true);
            bosnianText[i].SetActive(false);
        }

    }
}
