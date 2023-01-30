using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using TMPro;
using PF.Controllers;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>TileManager</c> contains the methods and properties needed for managing tiles.
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        /// <value>Property <c>dialoguePanel</c> represents the UI element containing the dialogue panel.</value>
        public GameObject dialoguePanel;

        /// <value>Property <c>dialogueTitleText</c> represents the UI element containing the dialogue title text.</value>
        private TextMeshProUGUI _dialogueTitleText;
        
        /// <value>Property <c>dialogueText</c> represents the UI element containing the dialogue text.</value>
        private TextMeshProUGUI _dialogueText;

        /// <value>Property <c>player</c> represents the gameobject for the player.</value>
        public GameObject player;
        
        /// <value>Property <c>_playerController</c> represents the player's controller.</value>
        private PlayerController _playerController;
        
        /// <value>Property <c>_playerInput</c> represents the player's input.</value>
        private PlayerInput _playerInput;

        private void Awake(){
            _dialogueTitleText = dialoguePanel.transform.Find("DialogueTitleText").GetComponent<TextMeshProUGUI>();
            _dialogueText = dialoguePanel.transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
            _playerController = player.GetComponent<PlayerController>();
            _playerInput = player.GetComponent<PlayerInput>();
        }

        public void ToggleGate(string gateNumber)
        {
            Debug.Log("PUERTA");
            // Get the tilemap collider
            var tilemapCollider = GameObject.Find("Gate" + gateNumber).GetComponent<TilemapCollider2D>();
            // Toggle the tilemap collider
            tilemapCollider.enabled = !tilemapCollider.enabled;
        }
        
        public void ToggleBridge(string bridgeNumber)
        {
            // Get the tilemap renderer
            var tilemapRenderer = GameObject.Find("Bridge" + bridgeNumber).GetComponent<TilemapRenderer>();
            // Toggle the tilemap
            tilemapRenderer.enabled = !tilemapRenderer.enabled;
        }
        
        public void SwitchBackgroundMusic(string musicName)
        {
            // Get the audio source
            var audioSource = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
            // Get the audio clip
            var audioClip = Resources.Load<AudioClip>("Music/" + musicName);
            // Change the audio clip
            audioSource.clip = audioClip;
            // Play the audio clip
            audioSource.Play();
        }
        
        /// <summary>
        /// Method <c>StartDialogue</c> starts a dialogue sequence.
        /// </summary>
        public void StartDialogue(int dialogueSegmentId)
        {
            StopMovement();
            DialogueManager.CurrentSegment = DialogueManager.Segments[dialogueSegmentId];
            _dialogueTitleText.text = DialogueManager.CurrentSegment.speaker;
            _dialogueText.text = DialogueManager.CurrentSegment.content;
            dialoguePanel.SetActive(true);
        }
        /// <summary>
        /// Method <c>StopMovement</c> prevents the player from moving.
        /// </summary>
        private void StopMovement()
        {
            _playerController.enabled = false;
            _playerInput.enabled = false;
        }
        
        /// <summary>
        /// Method <c>ResumeMovement</c> resumes the player's movement.
        /// </summary>
        private void ResumeMovement()
        {
            _playerController.enabled = true;
            _playerInput.enabled = true;
        }
    }
}
