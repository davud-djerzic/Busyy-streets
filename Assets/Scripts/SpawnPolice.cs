using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPolice : MonoBehaviour
{
    public GameObject policeCar;
    public float spawnDistance = 1.0f;

    private Rigidbody2D rb;
    Vector2 lastVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Proveri da li je Rigidbody2D dodan
    }

    private void FixedUpdate()
    {
        lastVelocity = rb.velocity; // Uvek prati poslednju brzinu
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))  // Proveri tag sudara
        {
            Vector2 collisionPoint = collision.contacts[0].point;

            // Brzina (smijer kretanja) automobila koji je napravio koliziju
            Vector2 carVelocity = lastVelocity;

            // Spawnaj policijski auto
            spawnPolice(collisionPoint, carVelocity);
        }
    }

    private void spawnPolice(Vector2 collisionPoint, Vector2 carVelocity)
    {
        // Koristi poziciju sudara za spawnanje
        Vector3 spawnPosition = new Vector3(collisionPoint.x, collisionPoint.y, 0);

        // Spawnaj policijski auto na toj poziciji
        Instantiate(policeCar, spawnPosition, Quaternion.identity);
    }
}