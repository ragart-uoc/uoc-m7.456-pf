using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using PF.Entities;
using PF.Controllers;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>DialogueManager</c> contains the methods and properties needed for the dialogues.
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        /// <value>Property <c>_instance</c> represents the instance of the DialogueManager.</value>
        private DialogueManager _instance;
        
        /// <value>Property <c>dialoguePanel</c> represents the UI element containing the dialogue panel.</value>
        public GameObject dialoguePanel;

        /// <value>Property <c>dialogueTitleText</c> represents the UI element containing the dialogue title text.</value>
        private TextMeshProUGUI _dialogueTitleText;
        
        /// <value>Property <c>dialogueText</c> represents the UI element containing the dialogue text.</value>
        private TextMeshProUGUI _dialogueText;

        /// <value>Property <c>_words</c> contains the words.</value>
        private string[] _words;
        
        /// <value>Property <c>_wordsLearned</c> contains the words learned.</value>
        private string[] _wordsLearned;
        
        /// <value>Property <c>_wordsEquipped</c> contains the words equipped.</value>
        private string[] _wordsEquipped;
        
        /// <value>Property <c>_wordsToLearn</c> contains the words to learn.</value>
        private string[] _wordsToLearn;
        
        /// <value>Property <c>_segments</c> contains the dialogue segments.</value>
        private Dictionary<int, DialogueSegment> _segments;
        
        /// <value>Property <c>_currentSegment</c> represents the current dialogue segment.</value>
        private DialogueSegment _currentSegment;

        /// <value>Property <c>player</c> represents the gameobject for the player.</value>
        public GameObject player;
        
        /// <value>Property <c>_playerController</c> represents the player's controller.</value>
        private PlayerController _playerController;
        
        /// <value>Property <c>_playerInput</c> represents the player's input.</value>
        private PlayerInput _playerInput;

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            // Singleton pattern
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = this;
            
            // Load words
            ImportWords();
            
            // Load dialogue segments
            _segments = new Dictionary<int, DialogueSegment>();
            ImportDialogueSegments();
        }
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            _dialogueTitleText = dialoguePanel.transform.Find("DialogueTitleText").GetComponent<TextMeshProUGUI>();
            _dialogueText = dialoguePanel.transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
            _playerController = player.GetComponent<PlayerController>();
            _playerInput = player.GetComponent<PlayerInput>();
        }
        
        /// <summary>
        /// Method <c>Start</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (dialoguePanel.activeSelf)
            {
                if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0)
                                          || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
                {
                    if (_currentSegment != null)
                    {
                        if (_currentSegment.nextSegment > 0)
                        {
                            _currentSegment = _segments[_currentSegment.nextSegment];
                            _dialogueTitleText.text = _currentSegment.speaker;
                            _dialogueText.text = _currentSegment.content;
                        }
                        else
                        {
                            dialoguePanel.SetActive(false);
                            ResumeMovement();
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Method <c>ImportWords</c> imports the words from the JSON file.
        /// </summary>
        private void ImportWords()
        {
            var wordsJson = Resources.Load<TextAsset>("Words");
            _words = JsonUtility
                .FromJson<DialogueWordList>("{\"words\":" + wordsJson.text + "}").words;
        }

        /// <summary>
        /// Method <c>ImportDialogueSegments</c> imports the dialogue segments from the JSON file.
        /// </summary>
        private void ImportDialogueSegments()
        {
            var dialogueSegmentsJson = Resources.Load<TextAsset>("DialogueSegments");
            var dialogueSegments = JsonUtility
                .FromJson<DialogueSegmentDictionary>("{\"dialogueSegments\":" + dialogueSegmentsJson.text + "}")
                .dialogueSegments;
            foreach (var dialogueSegment in dialogueSegments)
            {
                _segments.Add(dialogueSegment.id, dialogueSegment);
            }
        }
        
        /// <summary>
        /// Method <c>StartDialogue</c> starts a dialogue sequence.
        /// </summary>
        public void StartDialogue(int dialogueSegmentId)
        {
            StopMovement();
            _currentSegment = _segments[dialogueSegmentId];
            _dialogueTitleText.text = _currentSegment.speaker;
            _dialogueText.text = _currentSegment.content;
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
