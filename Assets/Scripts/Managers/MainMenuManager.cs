using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PF.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        public TextMeshProUGUI logoText;
        public TextMeshProUGUI pressButtonText;
        public GameObject mainMenu;
        private TextMeshProUGUI[] _mainMenuTexts;
        public Button selectedButton;

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
            StartCoroutine(ShowLogo());
        }
        
        /// <summary>
        /// Method <c>Start</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (Input.anyKeyDown && !mainMenu.activeSelf)
            {
                StopCoroutine(BlinkPressButtonText());
                pressButtonText.gameObject.SetActive(false);
                StartCoroutine(ShowMainMenu());
            }
        }
        
        /// <summary>
        /// Method <c>ShowLogo</c> shows the logo and the "press any button" message.
        /// </summary>
        private IEnumerator ShowLogo()
        {
            logoText.canvasRenderer.SetAlpha(0.0f);
            pressButtonText.canvasRenderer.SetAlpha(0.0f);
            logoText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);
            pressButtonText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(BlinkPressButtonText());
        }

        /// <summary>
        /// Method <c>BlinkPressButtonText</c> makes the "press any button" text blink.
        /// </summary>
        private IEnumerator BlinkPressButtonText()
        {
            while (true)
            {
                pressButtonText.CrossFadeAlpha(0.0f, 0.5f, false);
                yield return new WaitForSeconds(0.5f);
                pressButtonText.CrossFadeAlpha(1.0f, 0.5f, false);
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        /// <summary>
        /// Method <c>ShowMainMeny</c> shows the main menu.
        /// </summary>
        private IEnumerator ShowMainMenu()
        {
            foreach (var mainMenuText in _mainMenuTexts)
            {
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
        /// Method <c>StartGame</c> loads the new game scene.
        /// </summary>
        public void StartGame()
        {
        }

        /// <summary>
        /// Method <c>ContinueGame</c> loads the new game scene with the last saved state.
        /// </summary>
        public void ContinueGame()
        {
        }

        /// <summary>
        /// Method <c>Options</c> toggles the visibility of the options screen.
        /// </summary>
        public void ToggleOptions()
        {
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
