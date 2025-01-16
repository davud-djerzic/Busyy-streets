using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SocialMedia : MonoBehaviour
{
    public void instagramProfile()
    {
        Application.OpenURL("https://www.instagram.com/_djerzaa9_/");
        PlayerPrefs.SetInt("Achievement0", 1);

    }

    public void discordProfile()
    {
        Application.OpenURL("https://discord.com/invite/TXf7WuaFpr");
        PlayerPrefs.SetInt("Achievement1", 1);
    }
}
