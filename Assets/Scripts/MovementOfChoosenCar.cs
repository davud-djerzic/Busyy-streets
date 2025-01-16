using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOfChoosenCar : MonoBehaviour
{
    public static bool isFinishMovement = false;
    [SerializeField] public GameObject[] wayPoints;
    private float speed = 3.0f;
    [SerializeField] public GameObject arrow;
    [SerializeField] public AudioSource carAudio;

    private int currentIndex;

    void Start()
    {
        isFinishMovement = false;
        currentIndex = 0;
        carAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinishMovement)
        {
            if (Vector2.Distance(wayPoints[currentIndex].transform.position, gameObject.transform.position) < 0.1f) // ako je razdaljina izmedju auta i wayPointa 0
            {
                if (currentIndex != wayPoints.Length - 1)
                {
                    currentIndex++;
                }
                else
                {
                    carAudio.Stop();
                    isFinishMovement = true;
                    arrow.gameObject.SetActive(true);
                }
            }
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, wayPoints[currentIndex].transform.position, Time.deltaTime * speed);
        }

    }
}
