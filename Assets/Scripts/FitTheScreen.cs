using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FitTheScreen : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //AdjustSpriteSize();
    }

    void AdjustSpriteSize()
    {
        // Dobij velièinu ekrana
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // Dobij velièinu sprite-a
        float spriteHeight = spriteRenderer.sprite.bounds.size.y;
        float spriteWidth = spriteRenderer.sprite.bounds.size.x;

        // Prilagodi scale tako da sprite zauzima cijeli ekran
        Vector3 tempScale = transform.localScale;
        tempScale.x = screenWidth / spriteWidth;
        tempScale.y = screenHeight / spriteHeight;
        transform.localScale = tempScale;
    }

    void Update()
    {
        //AdjustSpriteSize();
    }
}

