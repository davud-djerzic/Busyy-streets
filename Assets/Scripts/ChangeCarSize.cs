using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCarSize : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void AdjustSpriteSize()
    {
        // Dobij veli?inu ekrana
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // Dobij veli?inu sprite-a
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

    public GameObject[] cars;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //AdjustSpriteSize();
        foreach (GameObject car in cars)
        {
            GetRelativePosition(car);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject car in cars)
        {
            UpdateCarPosition(car, GetRelativePosition(car));
        }
    }

    Vector2 GetRelativePosition(GameObject car)
    {

        // Dobijanje granica pozadinske slike
        Bounds backgroundBounds = spriteRenderer.bounds;

        // Dobijanje pozicije automobila
        Vector3 carPosition = car.transform.position;

        // Izraèunavanje relativne pozicije automobila
        float relativeX = (carPosition.x - backgroundBounds.min.x) / backgroundBounds.size.x;
        float relativeY = (carPosition.y - backgroundBounds.min.y) / backgroundBounds.size.y;

        // Vraæanje relativne pozicije izmeðu 0 i 1
        return new Vector2(relativeX, relativeY);
    }

    void UpdateCarPosition(GameObject car, Vector2 relativePosition)
    {
        SpriteRenderer background = GetComponent<SpriteRenderer>();
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float backgroundWidth = topRight.x - bottomLeft.x;
        float backgroundHeight = topRight.y - bottomLeft.y;

        car.transform.position = new Vector3(
            bottomLeft.x + relativePosition.x * backgroundWidth,
            bottomLeft.y + relativePosition.y * backgroundHeight,
            car.transform.position.z
        );
    }
}
