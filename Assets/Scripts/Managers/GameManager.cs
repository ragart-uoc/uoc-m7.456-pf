using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using TMPro;
using PF.Controllers;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>GameManager</c> contains the methods and properties needed for the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <value>Property <c>_instance</c> represents the instance of the GameManager.</value>
        private static GameManager _instance;
        
        /// <value>Property <c>Instance</c> represents the instance of the GameManager.</value>
        private string _currentScene;

        /// <value>Property <c>_persistentDataManager</c> represents the instance of the PersistentDataManager.</value>
        private PersistentDataManager _persistentDataManager;
        
        /// <value>Property <c>_wordPrefab</c> represents the prefab for the word.</value>
        public GameObject wordPrefab;

        /// <value>Property <c>wordList</c> represents the gameobject for the word list.</value>
        public GameObject wordList;

        /// <value>Property <c>_replacingWords</c> represents whether the player is replacing words.</value>
        private bool _replacingWords;
        
        /// <value>Property <c>_replacementWord</c> represents the word that will be added instead of a equipped one.</value>
        private string _replacementWord;
        
        /// <value>Property <c>dialoguePanel</c> represents the UI element containing the dialogue panel.</value>
        public GameObject dialoguePanel;
        
        /// <value>Property <c>messagePanel</c> represents the UI element containing the message panel.</value>
        public GameObject messagePanel;

        /// <value>Property <c>_messageTitleText</c> represents the UI element containing the message title text.</value>
        private TextMeshProUGUI _messageTitleText;
        
        /// <value>Property <c>_messageText</c> represents the UI element containing the message text.</value>
        private TextMeshProUGUI _messageText;

        /// <value>Property <c>player</c> represents the gameobject for the player.</value>
        public GameObject player;
        
        /// <value>Property <c>_playerController</c> represents the player's controller.</value>
        private PlayerController _playerController;
        
        /// <value>Property <c>_playerInput</c> represents the player's input.</value>
        private PlayerInput _playerInput;
        
        /// <value>Property <c>_audioSource</c> represents the audio source.</value>
        private AudioSource _audioSource;
        
        /// <value>Property <c>_doorOpen</c> represents the door open audio clip.</value>
        private AudioClip _doorOpen;

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
            
            // Get the PersistentDataManager
            _persistentDataManager = GameObject.Find("PersistentDataManager").GetComponent<PersistentDataManager>();

            // Initialize variables
            _messageTitleText = messagePanel.transform.Find("MessageTitleText").GetComponent<TextMeshProUGUI>();
            _messageText = messagePanel.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();
            _playerController = player.GetComponent<PlayerController>();
            _playerInput = player.GetComponent<PlayerInput>();
            _audioSource = GetComponent<AudioSource>();
            _doorOpen = Resources.Load<AudioClip>("Sounds/door");
        }
        
        private void Start()
        {
            // Disable the renderer of all GameObjects with tag "Collisions"
            var collisionObjects = GameObject.FindGameObjectsWithTag("Collisions");
            foreach (var collisionObject in collisionObjects)
            {
                collisionObject.GetComponent<Renderer>().enabled = false;
            }
            
            // Remove all equipped words in the UI and add the currently equipped
            foreach (Transform child in wordList.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var word in _persistentDataManager.equippedWords)
            {
                var wordObject = Instantiate(wordPrefab, wordList.transform);
                var tmpComponent = wordObject.GetComponentInChildren<TextMeshProUGUI>();
                tmpComponent.text = word;
                tmpComponent.color = new Color(0, 0, 0, 0.7f);
            }

            // If all deaths are seen, load final dialogue
            if (_persistentDataManager.AllDeathsSeen())
            {
                ToggleBridge(9);
                StartDialogue(14);
                ToggleGate("9A");
            }
            // If tutorials are completed, load the loop initial dialogue
            else if (_persistentDataManager.AllTutorialsCompleted())
            {
                StartDialogue(13);
                ToggleGate("1A");
            }
            // In any other case, load the initial dialogue
            else
            {
                StartDialogue(8);
                ToggleGate("1A");
            }
        }
        
        /// <summary>
        /// Method <c>Update</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (dialoguePanel.activeSelf)
            {
                if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0)
                                          || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
                {
                    if (DialogueManager.currentSegment.bridgeOpen > 0)
                    {
                        ToggleBridge(DialogueManager.currentSegment.bridgeOpen);
                    }

                    switch (DialogueManager.currentSegment)
                    {
                        case { ending: > 0 }:
                            break;
                        case { nextSegment: > 0 }:
                            DialogueManager.SetCurrentSegment(DialogueManager.currentSegment.nextSegment);
                            _messageTitleText.text = DialogueManager.currentSegment.speaker;
                            _messageText.text =
                                DialogueManager.ProcessText(DialogueManager.currentSegment.nextSegment);
                            break;
                        default:
                            dialoguePanel.SetActive(false);
                            ResumeMovement();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Method <c>ToggleGate</c> toggles the tilemap collider for a gate.
        /// </summary>
        public void ToggleGate(string gateNumber)
        {
            // Get the tilemap collider
            var tilemapCollider = GameObject.Find("Gate" + gateNumber).GetComponent<TilemapCollider2D>();
            // Toggle the tilemap collider
            tilemapCollider.enabled = !tilemapCollider.enabled;
        }
        
        /// <summary>
        /// Method <c>ToggleBridge</c> toggles the tilemap for a bridge.
        /// </summary>
        public void ToggleBridge(int bridgeNumber)
        {
            // Get the tilemap renderer
            var tilemapRenderer = GameObject.Find("Bridge" + bridgeNumber).GetComponent<TilemapRenderer>();
            // Toggle the tilemap
            tilemapRenderer.enabled = !tilemapRenderer.enabled;
            // Play the door open sound
            _audioSource.PlayOneShot(_doorOpen);
        }
        
        /// <summary>
        /// Method <c>SwitchBackgroundMusic</c> switches the background music.
        /// </summary>
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
            DialogueManager.SetCurrentSegment(dialogueSegmentId);
            _messageTitleText.text = DialogueManager.currentSegment.speaker;
            _messageText.text = DialogueManager.ProcessText(dialogueSegmentId);
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
        
        private void LearnWord(string word)
        {
            /*
            if (_playerProgress.wordsLearned.Contains(word))
                return;
            if (_playerProgress.wordsLearned.Count < 4)
            {
                _playerProgress.wordsLearned.Add(word);
            }
            else
            {
                ReplaceEquippedWord(word);
            }
            */
        }
        
        private void ReplaceEquippedWord(string word)
        {
            _replacingWords = true;
            //_messageText.text = "You can't carry any more words, please drop one first.";
        }
    }
}
