#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoomBoom : MonoBehaviour
{
    private Animator anim;
    public MovementCar movementCar;
    public AudioSource crashSound;
    public PauseMenu pauseScript;
    public GameObject restartLevelPanel;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))  // ako se desi sudar 2 auta
        {
            crashSound.Play(); // pokrene se sound sudara
            anim.SetBool("boom", true); // pokrece se animacija sudara
            movementCar.isMoving = false; // reseta se vrijednost auta da se ne moze nastaviti kretati
            Invoke("resetLevel", 2.0f); // pokrene se funkcija da se level resetuje
        }
    }

    private void setBoolFalse() // ova funkcija se poziva kada se zavrsi animacija da se resetuje bool vrijednost
    {
        anim.SetBool("boom", false); 
    }

    private void resetLevel()
    {
        CheckWin.streak = 0;
        loadNextScene();
        Timer.elapsedTime = 0;
    }

    private void loadNextScene() // ova funkcija se koristi da se pokrene pause panel i njegova animacije
    {
        pauseScript.NextLevelIntro();
        restartLevelPanel.SetActive(true);
        Timer.elapsedTime = 0;
        Timer.isOkayToCount = false;
    }
}
#endif