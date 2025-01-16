using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TimerChangeUsername : MonoBehaviour 
{ 
    public static TimerChangeUsername Instance { get; private set; }
    private float timeElapsed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
       
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        if (seconds > 9)
        {
            ChangeUsername.isAllowToChangeUsername = true;
            timeElapsed = 0;
            PlayerPrefs.SetFloat("PlayedTime", PlayerPrefs.GetFloat("PlayedTime", 0) + seconds);
        }
        Debug.Log(seconds);
        Debug.Log(PlayerPrefs.GetFloat("PlayedTime", 0));
    }

    public float GetTimeElapsed()
    {
        return Mathf.FloorToInt(timeElapsed);
    }
}
