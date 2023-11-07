using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PauseMenuToggle : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        pauseMenuCanvas.SetActive(isGamePaused);
        Time.timeScale = !isGamePaused ? 1 : 0;
    }
}