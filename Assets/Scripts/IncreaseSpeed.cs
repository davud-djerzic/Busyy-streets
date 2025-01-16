using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseSpeed : MonoBehaviour
{
    public void clickButton()
    {
        if (MovementCar.speed < 4.0f)
            MovementCar.speed += 2.0f;
    }
}
