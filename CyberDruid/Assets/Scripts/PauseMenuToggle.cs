using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PauseMenuToggle : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuCanvas;

    [SerializeField]
    private InputActionReference PauseButton;

    // Start is called before the first frame update
    void Start()
    {
        PauseButton.action.Enable();
        PauseButton.action.performed += TogglePause;
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        pauseMenuCanvas.SetActive(isGamePaused);
        Time.timeScale = !isGamePaused ? 1 : 0;
    }
}