using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using PF.Controllers;

namespace PF.Managers
{
        
    /// <summary>
    /// Method <c>PauseManager</c> contains the methods and properties needed for the pause panel.
    /// </summary>
    public class PauseManager : MonoBehaviour
    {
        /// <value>Property <c>pausePanel</c> represents the UI element containing the pause panel.</value>
        public GameObject pausePanel;
        
        /// <value>Property <c>player</c> represents the gameobject for the player.</value>
        public GameObject player;
        
        /// <value>Property <c>_playerController</c> represents the player's controller.</value>
        private PlayerController _playerController;

        /// <value>Property <c>_playerInput</c> represents the player's input.</value>
        private PlayerInput _playerInput;
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            _playerController = player.GetComponent<PlayerController>();
            _playerInput = player.GetComponent<PlayerInput>();
        }

        /// <summary>
        /// Method <c>Start</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        /// <summary>
        /// Method <c>TogglePause</c> toggles the visibility of the pause screen.
        /// </summary>
        private void TogglePause()
        {
            Time.timeScale = pausePanel.activeSelf ? 0f : 1f;
            AudioListener.pause = pausePanel.activeSelf;
            pausePanel.SetActive(!pausePanel.activeSelf);
            _playerController.enabled = !pausePanel.activeSelf;
            _playerInput.enabled = !pausePanel.activeSelf;
            // If option panel is active, select the first slider
            if (pausePanel.activeSelf)
            {
                pausePanel.GetComponentInChildren<Slider>().Select();
            }
        }
    }
}
