using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Singleton used to enable and disable action maps
/// </summary>
public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    private GlobalInputActions inputActions;
    public GlobalInputActions.PlayerActions playerActionMap;
    public GlobalInputActions.UIActions uiActionMap;

    /// <summary>
    /// Validate there is only one instance & delete any clones.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Setup();
        }
    }

    /// <summary>
    /// Instantiate all action maps
    /// </summary>
    void Setup()
    {
        Instance = this;

        inputActions = new GlobalInputActions();

        playerActionMap = inputActions.Player;
        uiActionMap = inputActions.UI;

        EnableDefaultActionMap();
    }

    public void EnableDefaultActionMap()
    {
        playerActionMap.Enable();
    }

    public void DisableAllActionMaps()
    {
        playerActionMap.Disable();
        uiActionMap.Disable();
    }

    public void EnablePlayerActionMap()
    {
        playerActionMap.Enable();
    }

    public void DisablePlayerActionMap()
    {
        playerActionMap.Enable();
    }

    public void EnableUIActionMap()
    {
        uiActionMap.Enable();
    }

    public void DisableUIActionMap()
    {
        uiActionMap.Disable();
    }
}
