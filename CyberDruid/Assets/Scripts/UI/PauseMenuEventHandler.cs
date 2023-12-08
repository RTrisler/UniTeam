using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;

public class PauseMenuEventHandler : MonoBehaviour
{
    [SerializeField] private UIDocument pauseMenuDocument;

    public void FocusOnDefaultElement()
    {
        GetComponent<UIDocument>().rootVisualElement.
            Q<VisualElement>(className: "first-focused").Focus();
    }

    /// <summary>
    /// Focus on the default element and register the button events.
    /// </summary>
    private void OnEnable()
    {
        // For some reason it doesn't work the first time the pause menu is opened, but it does every time after.
        FocusOnDefaultElement();

        // Gather the elements
        VisualElement root = pauseMenuDocument.GetComponent<UIDocument>().rootVisualElement;
        Button buttonResume = root.Q<Button>("Resume_Button");
        Button buttonLoad = root.Q<Button>("Load_Button");
        Button buttonOptions = root.Q<Button>("Options_Button");
        Button buttonExit = root.Q<Button>("Exit_Button");

        // Register Clicked events
        buttonResume.clicked += () => PauseMenuToggle.TogglePause();
        buttonLoad.clicked += () => Debug.Log("Load");
        buttonOptions.clicked += () => Debug.Log("Options");
        buttonExit.clicked += () => Debug.Log("Exit to Main Menu");
    }
}
