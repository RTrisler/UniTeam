using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEventHandler : MonoBehaviour
{
    [SerializeField] private UIDocument MainDocument;
    [SerializeField] private UIDocument OptionsDocument;

    /// <summary>
    /// A simple enum to represent the direction of a change.
    /// </summary>
    public enum DeltaDirection { Decrease = -1, Increase = 1 };

    /// <summary>
    /// Focus on the first element with the "first-focused" class.
    /// </summary>
    /// <param name="document"> The document to search for the element in</param>
    public void FocusOnDefaultElement(UIDocument document)
    {
        // Focus on the first element with the "first-focused" class
        var firstFocused = document.rootVisualElement.
            Q<VisualElement>(className: "first-focused");

        if (firstFocused != null)
        {
            firstFocused.Focus();
        }
        else
        {
            Debug.LogError("No element with the \"first-focused\" class found");
        }
    }

    /// <summary>
    /// Show the Main Menu's main screen when the game starts.
    /// </summary>
    private void OnEnable()
    {
        SwitchDocuments(nameof(MainDocument));
    }

    /// <summary>
    /// Disable any shown menus and enable the menu with the given name.
    /// </summary>
    /// <param name="documentName">The variable name of the document being switched to.</param>
    private void SwitchDocuments(string documentName)
    {
        DisableAllMenus();

        switch (documentName)
        {
            case nameof(MainDocument):
                EnableMainMenu();
                FocusOnDefaultElement(MainDocument);
                break;
            case nameof(OptionsDocument):
                EnableOptionsMenu();
                FocusOnDefaultElement(OptionsDocument);
                break;
            default:
                Debug.LogError("Invalid document name");
                break;
        }
    }

    private void DisableAllMenus()
    {
        MainDocument.gameObject.SetActive(false);
        OptionsDocument.gameObject.SetActive(false);
    }

    private void EnableMainMenu()
    {
        // Enable the menu
        MainDocument.gameObject.SetActive(true);

        // Gather the elements
        VisualElement root = MainDocument.GetComponent<UIDocument>().rootVisualElement;
        Button buttonNewGame = root.Q<Button>("NewGame_Button");
        Button buttonLoadGame = root.Q<Button>("LoadGame_Button");
        Button buttonOptions = root.Q<Button>("Options_Button");
        Button buttonQuit = root.Q<Button>("Quit_Button");

        // Register Clicked events
        buttonNewGame.clicked += () => Debug.Log("New Game button clicked");
        buttonLoadGame.clicked += () => Debug.Log("Load Game button clicked");
        buttonOptions.clicked += () => SwitchDocuments(nameof(OptionsDocument));
        buttonQuit.clicked += () => Debug.Log("Quit Game button clicked");
    }

    #region Options Menu

    private void EnableOptionsMenu()
    {
        // Enable the menu
        OptionsDocument.gameObject.SetActive(true);

        // Gather the elements
        VisualElement root = OptionsDocument.GetComponent<UIDocument>().rootVisualElement;
        Button buttonBack = root.Q<Button>("Back_Button");
        Button buttonDecreaseResolution = root.Q<Button>("Resolution_Decrease_Button");
        Button buttonIncreaseResolution = root.Q<Button>("Resolution_Increase_Button");
        Slider sliderAudio = root.Q<Slider>("Audio_Slider");
        Button labelResolutionValue = root.Q<Button>("Resolution_Value");

        // Register Clicked events
        buttonBack.clicked += () => SwitchDocuments(nameof(MainDocument));
        buttonDecreaseResolution.RegisterCallback<ClickEvent>((clickEvent) => 
            ChangeResolution(labelResolutionValue, DeltaDirection.Decrease));
        buttonIncreaseResolution.RegisterCallback<ClickEvent>((clickEvent) => 
            ChangeResolution(labelResolutionValue, DeltaDirection.Increase));
        sliderAudio.RegisterCallback<ChangeEvent<float>>(ChangeAudio);
    }

    public void ChangeResolution(Button resolutionLabel, DeltaDirection deltaDirection)
    {
        IList<string> resolutions = new List<string> { "800x600", "1280x720", "1920x1080"};

        string currentResolution = resolutionLabel.text;
        int currentIndex = resolutions.IndexOf(currentResolution);
        int newIndex = (currentIndex + (int) deltaDirection) % resolutions.Count;
        if (newIndex < 0)
        {
            newIndex = resolutions.Count - 1;
        }
        resolutionLabel.text = resolutions[newIndex];

        Debug.Log($"Resolution changed to {resolutionLabel.text}");
    }

    public void ChangeAudio(ChangeEvent<float> sliderChanged)
    {
        Debug.Log($"Audio changed to {sliderChanged.newValue}");
    }

    #endregion
}