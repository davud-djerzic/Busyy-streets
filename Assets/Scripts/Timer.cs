using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public static float elapsedTime;
    public static bool isOkayToCount = true;

    private void Start()
    {
        isOkayToCount = true;
       // timerText.text = "00:00";
    }

    private void Update()
    {
        if (isOkayToCount)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
}
