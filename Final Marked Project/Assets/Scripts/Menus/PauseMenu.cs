using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    //initialising Variables
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject ControlsMenuUI;
    public Image health;

    // new control scheme
    private InputAction Pa; // pause
    PlayerActions controls;
    private float tapReset = 0;

    //if dead
    public TextMeshProUGUI ContRetry;
    public TextMeshProUGUI Title;

    private void Awake()
    {
        // setting up control scheme
        controls = new PlayerActions();
        Pa = controls.PlayerCon.Pause;
        Pa.Enable();

        // continue the game!
        Resume();
    }

    void Update()
    {
        // checking for pause button pressed and setting to be paused
        Pa.started += ctx =>
        {
            if (ctx.interaction is TapInteraction && tapReset <= 0 && health.fillAmount >= 0.0001)
            {
                if (GameIsPaused == true)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
                tapReset = 0.2f;
            }
        };

        // if dead pause, (since the death screen is just the pause screen modified)
        if (health.fillAmount <= 0.0001)
        {
            if (OptionsMenuUI.activeSelf == false && ControlsMenuUI.activeSelf == false)
            {
                Pause();
            }

            // change text for a death screen
            ContRetry.text = "RETRY?";
            Title.text = "YOU DIED AT: WAVE " + GameObject.Find("GameManager").GetComponent<WaveManager>().WaveNum; 
        }
        else
        {
            // the usual text for the pause screen
            ContRetry.text = "CONTINUE";
            Title.text = "PAUSE MENU";
        }

        // timer for tapping
        tapReset -= Time.fixedUnscaledDeltaTime;

        // taking away and giving cursor control
        if (GameIsPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }



    public void Resume()
    {
        if (health.fillAmount >= 0.0001)
        {
            // setting everything to normal for resuming game
            PauseMenuUI.SetActive(false);
            OptionsMenuUI.SetActive(false);
            ControlsMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        else
        {
            // reset game
            SceneManager.LoadScene("City_Centre");
        }
    }

    void Pause()
    {
        // slowdown time when you're dead instead of freezing
        if (health.fillAmount >= 0.0001)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 0.15f;
        }

        // make ui appear
        PauseMenuUI.SetActive(true);

        // make sure the game knows it is paused
        GameIsPaused = true;
    }
}
