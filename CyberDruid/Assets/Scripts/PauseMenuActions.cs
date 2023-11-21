using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuActions : MonoBehaviour
{
    [SerializeField] private UIDocument pauseMenuDocument;

    private void OnEnable()
    {
        VisualElement root = pauseMenuDocument.GetComponent<UIDocument>().rootVisualElement;
        Button buttonResume = root.Q<Button>("Resume_Button");
        Button buttonLoad = root.Q<Button>("Load_Button");
        Button buttonOptions = root.Q<Button>("Options_Button");
        Button buttonExit = root.Q<Button>("Exit_Button");

        buttonResume.clicked += () => PauseMenuToggle.TogglePause();
    }
}
