using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Allows the pause menu to be toggled on and off 
/// & holds the current pause state of the game
/// </summary>
public class TogglePause : MonoBehaviour
{
    public static bool isGamePaused = false;

    public static System.Action TogglePauseEvent;

    [SerializeField] private GameObject PauseMenuCanvas;

    /// <summary>
    /// Add input events for toggling the pause menu on and off
    /// </summary>
    private void Start()
    {
        InputController.Instance.playerActionMap.Pause.performed += OnTogglePause;
        InputController.Instance.uiActionMap.UnPause.performed += OnTogglePause;
        TogglePauseEvent += OnTogglePause;
    }

    /// <summary>
    /// Overload for assigning toggling pause to input action events
    /// </summary>
    /// <param name="context"></param>
    private void OnTogglePause(InputAction.CallbackContext context)
    {
        OnTogglePause();
    }

    public void OnTogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;

        if (isGamePaused)
        {
            InputController.Instance.DisableAllActionMaps();
            InputController.Instance.EnableUIActionMap();
        }
        else
        {
            InputController.Instance.DisableAllActionMaps();
            InputController.Instance.EnablePlayerActionMap();
        }

        // Enables the pause menu canvas
        PauseMenuCanvas.gameObject.SetActive(isGamePaused);
    }
}