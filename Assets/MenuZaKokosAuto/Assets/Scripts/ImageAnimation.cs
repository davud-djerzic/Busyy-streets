using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAnimation : MonoBehaviour
{
    // Duration for one complete cycle of the floating animation
    public float floatDuration = 2.0f;

    // Maximum displacement in the Y-axis
    public float floatDistance = 30.0f;

    // Initial position of the image
    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position
        startPosition = transform.localPosition;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave for smooth floating effect
        float newY = startPosition.y + Mathf.Sin(Time.time / floatDuration * Mathf.PI * 2) * floatDistance;

        // Set the new position
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
