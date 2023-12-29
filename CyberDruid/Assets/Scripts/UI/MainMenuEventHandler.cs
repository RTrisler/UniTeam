using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CyberDruid.UI
{
    public class MainMenuEventHandler : BaseMenuEventHandler
    {
        [SerializeField]
        [InitialMenu]
        private UIDocument MainMenu;

        [SerializeField] private UIDocument OptionsMenu;

        /// <summary>
        /// A simple enum to represent the direction of a change.
        /// </summary>
        public enum DeltaDirection { Decrease = -1, Increase = 1 };

        protected override void SwitchDocuments(string documentName)
        {
            DisableAllMenus();

            switch (documentName)
            {
                case nameof(MainMenu):
                    EnableMainMenu();
                    FocusOnDefaultElement(MainMenu);
                    break;
                case nameof(OptionsMenu):
                    EnableOptionsMenu();
                    FocusOnDefaultElement(OptionsMenu);
                    break;
                default:
                    Debug.LogError("Invalid document name");
                    break;
            }
        }

        protected override void DisableAllMenus()
        {
            MainMenu.gameObject.SetActive(false);
            OptionsMenu.gameObject.SetActive(false);
        }

        #region Main Menu

        private void EnableMainMenu()
        {
            // Enable the menu
            MainMenu.gameObject.SetActive(true);

            // Gather the elements
            VisualElement root = MainMenu.GetComponent<UIDocument>().rootVisualElement;
            Button buttonNewGame = root.Q<Button>("NewGame_Button");
            Button buttonLoadGame = root.Q<Button>("LoadGame_Button");
            Button buttonOptions = root.Q<Button>("Options_Button");
            Button buttonQuit = root.Q<Button>("Quit_Button");

            // Register Clicked events
            buttonNewGame.clicked += () => Debug.Log("New Game button clicked");
            buttonLoadGame.clicked += () => Debug.Log("Load Game button clicked");
            buttonOptions.clicked += () => SwitchDocuments(nameof(OptionsMenu));
            buttonQuit.clicked += () => Debug.Log("Quit Game button clicked");
        }

        #endregion

        #region Options Menu

        private void EnableOptionsMenu()
        {
            // Enable the menu
            OptionsMenu.gameObject.SetActive(true);

            // Gather the elements
            VisualElement root = OptionsMenu.GetComponent<UIDocument>().rootVisualElement;
            Button buttonBack = root.Q<Button>("Back_Button");
            Button buttonDecreaseResolution = root.Q<Button>("Resolution_Decrease_Button");
            Button buttonIncreaseResolution = root.Q<Button>("Resolution_Increase_Button");
            Slider sliderAudio = root.Q<Slider>("Audio_Slider");
            Button labelResolutionValue = root.Q<Button>("Resolution_Value");

            // Register Clicked events
            buttonBack.clicked += () => SwitchDocuments(nameof(MainMenu));
            buttonDecreaseResolution.RegisterCallback<ClickEvent>((clickEvent) =>
                ChangeResolution(labelResolutionValue, DeltaDirection.Decrease));
            buttonIncreaseResolution.RegisterCallback<ClickEvent>((clickEvent) =>
                ChangeResolution(labelResolutionValue, DeltaDirection.Increase));
            sliderAudio.RegisterCallback<ChangeEvent<float>>(ChangeAudio);
        }

        public void ChangeResolution(Button resolutionLabel, DeltaDirection deltaDirection)
        {
            IList<string> resolutions = new List<string> { "800x600", "1280x720", "1920x1080" };

            string currentResolution = resolutionLabel.text;
            int currentIndex = resolutions.IndexOf(currentResolution);
            int newIndex = (currentIndex + (int)deltaDirection) % resolutions.Count;
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
}

