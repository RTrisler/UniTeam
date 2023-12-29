using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuEventHandler : BaseMenuEventHandler
{
    [SerializeField]
    [InitialMenu]
    private UIDocument MainMenu;

    protected override void SwitchDocuments(string documentName)
    {
        DisableAllMenus();

        switch (documentName)
        {
            case nameof(MainMenu):
                EnableMainMenu();
                FocusOnDefaultElement(MainMenu);
                break;
            default:
                Debug.LogError("Invalid document name");
                break;
        }
    }

    protected override void DisableAllMenus()
    {
        MainMenu.gameObject.SetActive(false);
    }

    #region Main Menu

    private void EnableMainMenu()
    {
        // Enable the menu
        MainMenu.gameObject.SetActive(true);

        // Gather the elements
        VisualElement root = MainMenu.GetComponent<UIDocument>().rootVisualElement;
        Button buttonResume = root.Q<Button>("Resume_Button");
        Button buttonLoad = root.Q<Button>("Load_Button");
        Button buttonOptions = root.Q<Button>("Options_Button");
        Button buttonExit = root.Q<Button>("Exit_Button");

        // Register Clicked events
        buttonResume.clicked += () => TogglePause.TogglePauseEvent();
        buttonLoad.clicked += () => Debug.Log("Load");
        buttonOptions.clicked += () => Debug.Log("Options");
        buttonExit.clicked += () => Debug.Log("Exit to Main Menu");
    }

    #endregion
}
