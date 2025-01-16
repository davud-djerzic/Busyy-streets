using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveYourOrginalPosition : MonoBehaviour
{
    public Vector2 anchor; // Relativna pozicija u postocima (0-1)
    void Start()
    {
        AdjustPositionToScreen();
    }

    void AdjustPositionToScreen()
    {
        // Dobij velièinu ekrana u world space-u
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // Raèunaj novu poziciju
        Vector3 newPosition = new Vector3(
            (anchor.x - 0.5f) * screenWidth,
            (anchor.y - 0.5f) * screenHeight,
            transform.position.z
        );

        // Postavi novu poziciju
        transform.position = newPosition;
    }


}
