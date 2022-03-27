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
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject ControlsMenuUI;
    public GameObject Player;

    private InputAction Pa; // pause
    PlayerActions controls;
    private float tapReset = 0;

    //if dead
    public TextMeshProUGUI ContRetry;

    private void Awake()
    {
        controls = new PlayerActions();
        Pa = controls.PlayerCon.Pause;
        Pa.Enable();
        Resume();
    }

    void Update()
    {
        Pa.started += ctx =>
        {
            if (ctx.interaction is TapInteraction && tapReset <= 0 && Player.GetComponent<PlayerMovement>().Health >= 0.0001)
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

        if (Player.GetComponent<PlayerMovement>().Health <= 0.0001)
        {
            Pause();
            ContRetry.text = "RETRY?";
        }
        else
        {
            ContRetry.text = "CONTINUE";
        }

        tapReset -= Time.fixedUnscaledDeltaTime;

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
        if (Player.GetComponent<PlayerMovement>().Health >= 0.0001)
        {
            PauseMenuUI.SetActive(false);
            OptionsMenuUI.SetActive(false);
            ControlsMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        else
        {
            SceneManager.LoadScene("City_Centre");
        }
    }

    void Pause()
    {
        if (Player.GetComponent<PlayerMovement>().Health >= 0.0001)
        {
            Time.timeScale = 0f;
        }
            PauseMenuUI.SetActive(true);
            GameIsPaused = true;
    }
}
