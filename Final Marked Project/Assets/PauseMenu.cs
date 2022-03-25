using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject ControlsMenuUI;

    private InputAction Pa; // pause
    PlayerActions controls;
    private float tapReset = 0;

    private void Awake()
    {
        controls = new PlayerActions();
        Pa = controls.PlayerCon.Pause;
        Pa.Enable();
    }

    void Update()
    {
        Pa.started += ctx =>
        {
            if (ctx.interaction is TapInteraction && tapReset <= 0)
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
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(false);
        ControlsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
