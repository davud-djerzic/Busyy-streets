using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopGarage : MonoBehaviour
{
    [SerializeField] public GameObject panel1;
    [SerializeField] public GameObject panel2;

    public void open()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
    }

    public void close()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
}
