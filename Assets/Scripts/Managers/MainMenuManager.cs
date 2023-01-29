using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using PF.Entities;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>MainMenuManager</c> contains the methods and properties needed for the main menu screen.
    /// </summary>
    public class MainMenuManager : MonoBehaviour
    {
        /// <value>Property <c>companyLogo</c> represents the UI element containing the game logo text.</value>
        public TextMeshProUGUI logoText;
        
        /// <value>Property <c>pressAnyKeyText</c> represents the UI element containing the "press any button" text.</value>
        public TextMeshProUGUI pressAnyKeyText;
        
        /// <value>Property <c>mainMenu</c> represents the UI element containing the main menu.</value>
        public GameObject mainMenu;
        
        /// <value>Property <c>_mainMenuTexts</c> represents the UI elements containing the main menu texts.</value>
        private TextMeshProUGUI[] _mainMenuTexts;
        
        /// <value>Property <c>selectedButton</c> represents the button that is currently selected.</value>
        public Button selectedButton;
        
        /// <value>Property <c>optionsPanel</c> represents the UI element containing the options panel.</value>
        public GameObject optionsPanel;
        
        /// <value>Property <c>creditsPanel</c> represents the UI element containing the credits panel.</value>
        public GameObject creditsPanel;

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            _mainMenuTexts = mainMenu.GetComponentsInChildren<TextMeshProUGUI>();
        }
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartCoroutine(ShowLogo());
        }
        
        /// <summary>
        /// Method <c>Start</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (!mainMenu.activeSelf && logoText.canvasRenderer.GetAlpha() >= 1.0f)
            {
                if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0)
                        || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
                {
                    StopCoroutine(BlinkText(pressAnyKeyText));
                    pressAnyKeyText.gameObject.SetActive(false);
                    StartCoroutine(ShowMainMenu());
                }
            }
            
            if (optionsPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ToggleOptions();
                }
            }
            
            if (creditsPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ToggleCredits();
                }
            }
        }
        
        /// <summary>
        /// Method <c>ShowLogo</c> shows the logo and the "press any button" message.
        /// </summary>
        private IEnumerator ShowLogo()
        {
            logoText.canvasRenderer.SetAlpha(0.0f);
            pressAnyKeyText.canvasRenderer.SetAlpha(0.0f);
            logoText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);
            pressAnyKeyText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(BlinkText(pressAnyKeyText));
        }

        /// <summary>
        /// Method <c>BlinkText</c> makes a text blink.
        /// </summary>
        private static IEnumerator BlinkText(Graphic text)
        {
            while (text.gameObject.activeSelf)
            {
                text.CrossFadeAlpha(0.0f, 0.5f, false);
                yield return new WaitForSeconds(0.5f);
                text.CrossFadeAlpha(1.0f, 0.5f, false);
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        /// <summary>
        /// Method <c>ShowMainMeny</c> shows the main menu.
        /// </summary>
        private IEnumerator ShowMainMenu()
        {
            var playerProgress = PlayerPrefsManager.LoadPlayerProgress();
            foreach (var mainMenuText in _mainMenuTexts)
            {
                // If element name is "ContinueText", check if there is a saved game
                if (mainMenuText.name == "ContinueText"
                    && playerProgress == "")
                {
                    mainMenuText.gameObject.SetActive(false);
                    continue;
                }
                mainMenuText.canvasRenderer.SetAlpha(0.0f);
            }
            mainMenu.SetActive(true);
            foreach (var mainMenuText in _mainMenuTexts)
            {
                mainMenuText.CrossFadeAlpha(1.0f, 1.5f, false);
                yield return new WaitForSeconds(0.1f);
            }
            selectedButton.Select();
        }

        /// <summary>
        /// Method <c>ContinueGame</c> loads the game scene.
        /// </summary>
        public void ContinueGame()
        {
            var playerProgressJson = PlayerPrefsManager.LoadPlayerProgress();
            if (playerProgressJson == "")
            {
                SceneManager.LoadScene("Game");
            }
            var playerProgress = new PlayerProgress().FromJson(playerProgressJson);
            SceneManager.LoadScene(playerProgress.wordsLearned.Count == 0 ? "Game" : "WordSelection");
        }
        
        /// <summary>
        /// Method <c>NewGame</c> loads the game scene removing any player progress.
        /// </summary>
        public void NewGame()
        {
            PlayerPrefsManager.SavePlayerProgress("");
            SceneManager.LoadScene("Game");
        }

        /// <summary>
        /// Method <c>ToggleOptions</c> toggles the visibility of the options screen.
        /// </summary>
        public void ToggleOptions()
        {
            optionsPanel.SetActive(!optionsPanel.activeSelf);
            // If option panel is active, prevent the main menu buttons from being clicked
            foreach (var mainMenuText in _mainMenuTexts)
            {
                var mainMenuElementButton = mainMenuText.gameObject.GetComponent<Button>();
                mainMenuElementButton.interactable = !optionsPanel.activeSelf;
            }
            // If option panel is active, select the first slider
            if (optionsPanel.activeSelf)
            {
                optionsPanel.GetComponentInChildren<Slider>().Select();
            }
            else
            {
                selectedButton.Select();
            }
        }
        
        /// <summary>
        /// Method <c>ToggleCredits</c> toggles the visibility of the credits screen.
        /// </summary>
        public void ToggleCredits()
        {
            creditsPanel.SetActive(!creditsPanel.activeSelf);
            // If credits panel is active, prevent the main menu buttons from being clicked
            foreach (var mainMenuText in _mainMenuTexts)
            {
                var mainMenuElementButton = mainMenuText.gameObject.GetComponent<Button>();
                mainMenuElementButton.interactable = !creditsPanel.activeSelf;
            }
            if (creditsPanel.activeSelf)
            {
                creditsPanel.GetComponentInChildren<Button>().Select();
            }
            else
            {
                selectedButton.Select();
            }
        }

        /// <summary>
        /// Method <c>QuitGame</c> quits the game.
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

    }
}
