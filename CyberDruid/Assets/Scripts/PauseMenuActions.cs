using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuActions : MonoBehaviour
{
    public UIDocument pauseMenuDocument;
    public PauseMenuToggle pauseMenuToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {

        VisualElement root = pauseMenuDocument.GetComponent<UIDocument>().rootVisualElement;
        Button buttonResume = root.Q<Button>("Resume_Button");
        Button buttonLoad = root.Q<Button>("Load_Button");
        Button buttonOptions = root.Q<Button>("Options_Button");
        Button buttonExit = root.Q<Button>("Exit_Button");

        buttonResume.clicked += () => pauseMenuToggle.TogglePause();
    }
}
