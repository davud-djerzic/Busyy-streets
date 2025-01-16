using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    #region Singleteon:Game
    public static Game instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }   else
        {
            Destroy(gameObject);
        }
        coins = PlayerPrefs.GetInt("Coins", 0);

    }
    #endregion

    public static int coins;
    



    public void useCoins(int amount)
    {
        coins -= amount;
        PlayerPrefs.SetInt("Coins", coins);
    }

    public bool hasEnough(int amount)
    {
        return (coins >= amount);
    }



}

