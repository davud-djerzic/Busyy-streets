using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementCar : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private GameObject car;

    public static float speed = 10.0f;
    public float rotationSpeed = 25f;
    private int currentIndex = 0;
    public bool isMoving = false;
    private Button button;
    public CheckWin checkWin;
    private static List<Button> usedButtons = new List<Button>();

    public GameObject arrow;
    private bool deleteArrow;
    public GameObject nextLevelPanel;
    public PauseMenu pauseMenu;

    [SerializeField] public AudioSource carAudio;

    public GameObject winLosePanel;
    public PauseMenu winLosePanelPauseMenu;
    public TextMeshProUGUI winLoseText;
    public TextMeshProUGUI coinsText;

    private void Start()
    {
        MovementOfChoosenCar.isFinishMovement = true;
        button = GetComponent<Button>();
        checkWin = FindObjectOfType<CheckWin>();
        List<Button> usedButtons = new List<Button>();
        deleteArrow = true; // potrebno je samo na pocetku da ova varijabla bude true
        PauseMenu.isPause = false;
    }


    public void Update()
    {
        speed = 3.0f;
        if (!deleteArrow)
        {
            if (PauseMenu.isPause)
            {
                carAudio.Pause();
            }
            else
            {
                carAudio.UnPause();
            }
        }


        if (isMoving && MovementOfChoosenCar.isFinishMovement) // mora se koristiti ova bool varijabla jer je bilo neki gresaka sa kretnjom
        {
            if (deleteArrow) // ako se auto krenulo kretati
            {
                carAudio.Play(); // pokrece se sound auta
                arrow.SetActive(false); // brise se arrow auta
                deleteArrow = false; // reseta se bool vrijednost
            }
            if (Vector2.Distance(wayPoints[currentIndex].transform.position, car.transform.position) < 0.1f) // ako je razdaljina izmedju auta i wayPointa 0
            {
                if (currentIndex != wayPoints.Length - 1)
                {
                    currentIndex++;
                }
                else
                {
                    isMoving = false; // auto se zasutavlja
                    carAudio.Stop(); // gasi se sound auta
                    EnableButtons(); // buttoni se mogu koristiti
                }
            }
            car.transform.position = Vector2.MoveTowards(car.transform.position, wayPoints[currentIndex].transform.position, Time.deltaTime * speed);


            Vector2 targetPosition = wayPoints[currentIndex].transform.position;
            Vector2 direction = targetPosition - (Vector2)car.transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            car.transform.rotation = Quaternion.RotateTowards(car.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Vector2.Distance(wayPoints[wayPoints.Length - 1].transform.position, car.transform.position) < 0.1f && checkWin.isWin) // ako je auto doslo do zadnjeg waypointa i presli ste level
            {
                Invoke("loadNextScene", 1.0f); // pokrece se ova funkcija
            }
        }

    }


    public void isMoved()
    {
        button.interactable = false;
        isMoving = true;
        usedButtons.Add(button);
        DisableButtons();
    }

    private void DisableButtons()
    {
        for (int i = 0; i < checkWin.buttons.Length; i++)
        {
            if (checkWin.buttons[i] != button) // Disable other buttons except the one that was pressed
            {
                checkWin.buttons[i].interactable = false;
            }
        }
    }

    private void EnableButtons()
    {
        for (int i = 0; i < checkWin.buttons.Length; i++)
        {
            if (checkWin.buttons[i] != button) // Enable all buttons except the one that was used
            {
                checkWin.buttons[i].interactable = true;
            }
        }
        for (int i = 0; i < usedButtons.Count; i++)
        {
            usedButtons[i].interactable = false;
        }
    }

    private void loadNextScene() // poziva kada se predje level
    {
        pauseMenu.NextLevelIntro();
        nextLevelPanel.SetActive(true);

        winLosePanelPauseMenu.NextLevelIntro();
        winLosePanel.SetActive(true);
        winLoseText.text = "You Win!!!";
        if (CheckWin.streak == 3)
        {
            coinsText.text = "+70";

        } else
        {
            coinsText.text = "+50";
        }

        Timer.elapsedTime = 0;
        Timer.isOkayToCount = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
