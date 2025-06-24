using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUsername : MonoBehaviour
{
    public GameObject darkPanel;
    public GameObject usernamePanel;
    [SerializeField] RectTransform usernamePanelRect;
    public TMP_InputField inputField;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI textHolderText;
    public static bool isAllowToChangeUsername = true;

    public void changeUsername()
    {
        if (inputField.text.Length > 0 && inputField.text.Length < 15)
        {
            if (isAllowToChangeUsername)
            {
                string username = inputField.text;
                username = char.ToUpper(username[0]) + username.Substring(1).ToLower();
                usernameText.text = username;
                inputField.text = "";
                PlayerPrefs.SetString("username", username);
                isAllowToChangeUsername = false;
                PlayerPrefs.SetInt("isChangedUsername", 1);
            } else
            {
                inputField.text = "";
                inputField.text = "Wait a " + TimerChangeUsername.Instance.GetTimeElapsed() + " seconds";
            }
        }
        else 
        {
            inputField.text = "";
            inputField.text = "Wrong input";
        }

    }

    public void closeUsernamePanel()
    {
        if (PlayerPrefs.GetInt("isChangedUsername", 0) == 1)
        {
            usernamePanelRect.anchoredPosition = new Vector2(0, 1000);
            usernamePanel.SetActive(false);
            darkPanel.SetActive(false);
            PlayerPrefs.SetInt("isChangedUsername", 0);
            setTextHolderText();
        }
    }

    public void clearTextHolderText()
    {
        textHolderText.text = "";
        inputField.text = "";
    }

    public void setTextHolderText()
    {
        textHolderText.text = "Enter a username";
        inputField.text = "";
    }


}
