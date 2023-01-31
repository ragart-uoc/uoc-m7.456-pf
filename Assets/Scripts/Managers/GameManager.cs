using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
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

        /// <value>Property <c>_wordUsageErrors</c> represents the number of times that the wrong word was used.</value>
        private int _wordUsageErrors;

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

        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            DialogueManager.RefreshWords(_persistentDataManager.learnedWords, _persistentDataManager.equippedWords);

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
                AddEquippedWordListUI(word);
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
                    ContinueDialogue();
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
            LoadDialogue(dialogueSegmentId, false);
            dialoguePanel.SetActive(true);
        }

        /// <summary>
        /// Method <c>ContinueDialogue</c> continues the dialogue sequence.
        /// </summary>
        private void ContinueDialogue()
        {
            if (_replacingWords) return;

            if (DialogueManager.currentSegment.bridgeOpen > 0)
            {
                ToggleBridge(DialogueManager.currentSegment.bridgeOpen);
            }

            switch (DialogueManager.currentSegment)
            {
                case { ending: > 0 }:
                    _persistentDataManager.endingReached = DialogueManager.currentSegment.ending;
                    SceneManager.LoadScene("Ending");
                    break;
                case { nextSegment: > 0 }:
                    LoadDialogue(DialogueManager.currentSegment.nextSegment, false);
                    break;
                default:
                    dialoguePanel.SetActive(false);
                    ResumeMovement();
                    break;
            }
        }

        /// <summary>
        /// Method <c>LoadDialogue</c> loads the current segment in the dialogue sequence.
        /// </summary>
        private void LoadDialogue(int dialogueSegmentId, bool forceInteraction)
        {
            DialogueManager.SetCurrentSegment(dialogueSegmentId);
            _messageTitleText.text = DialogueManager.currentSegment.speaker;
            _messageText.text = DialogueManager.ProcessText(DialogueManager.currentSegment.nextSegment);
            RefreshEquippedWordListUI(forceInteraction);
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
        
        /// <summary>
        /// Method <c>EquipWord</c> equips a word.
        /// </summary>
        public void EquipWord(string word)
        {
            if (_persistentDataManager.equippedWords.Contains(word))
                return;
            if (_persistentDataManager.equippedWords.Count < 4)
            {
                SaveEquippedWord(word);
                ContinueDialogue();
            }
            else
            {
                ReplaceEquippedWord(word);
            }
        }

        /// <summary>
        /// Method <c>SaveEquippedWord</c> saves an equipped word.
        /// </summary>
        private void SaveEquippedWord(string word)
        {
            _persistentDataManager.equippedWords.Add(word);
            _persistentDataManager.SavePlayerProgress();
            DialogueManager.RefreshWords(_persistentDataManager.learnedWords, _persistentDataManager.equippedWords);
            AddEquippedWordListUI(word);
        }
        
        /// <summary>
        ///  Method <c>ReplaceEquippedWord</c> replaces an equipped word.
        /// </summary>
        private void ReplaceEquippedWord(string word)
        {
            _replacingWords = true;
            _replacementWord = word;
            _messageText.text = "You can't carry any more words, please drop one first.";
            RefreshEquippedWordListUI(true);
        }
        
        /// <summary>
        /// Method <c>RemoveEquippedWord</c> removes an equipped word.
        /// </summary>
        private void RemoveEquippedWord(string word)
        {
            _persistentDataManager.equippedWords.Remove(word);
            _persistentDataManager.SavePlayerProgress();
            DialogueManager.RefreshWords(_persistentDataManager.learnedWords, _persistentDataManager.equippedWords);
            Destroy(GameObject.Find("Word-" + word));
            _messageText.text = DialogueManager.ProcessText(DialogueManager.currentSegment.id);
        }
        
        /// <summary>
        /// Method <c>UseOrReplaceWord</c> uses or replaces an equipped word.
        /// </summary>
        private void UseOrReplaceEquippedWord(string word)
        {
            // Replace equipped word
            if (_replacingWords)
            {
                RemoveEquippedWord(word);
                SaveEquippedWord(_replacementWord);
                _replacingWords = false;
                ContinueDialogue();
            }
            // Use equipped word
            else
            {
                var usableWord = DialogueManager.currentSegment.usableWords.SingleOrDefault(item => item.word == word);
                if (usableWord != null)
                {
                    LoadDialogue(usableWord.nextSegment, false);
                }
                else
                {
                    var currentSegment = DialogueManager.currentSegment;
                    _wordUsageErrors++;
                    if (_wordUsageErrors >= 10)
                    {
                        LoadDialogue(25, false);
                        DialogueManager.currentSegment.ending = 4;
                    }
                    else
                    {
                        LoadDialogue(29, false);
                        DialogueManager.currentSegment.nextSegment = currentSegment.nextSegment;
                        DialogueManager.currentSegment.bridgeOpen = currentSegment.bridgeOpen;
                        DialogueManager.currentSegment.ending = currentSegment.ending;
                    }
                }
            }
        }
        
        /// <summary>
        /// Method <c>AddEquippedWordListUI</c> adds an equipped word to the UI.
        /// </summary>
        private void AddEquippedWordListUI(string word)
        {
            var wordObject = Instantiate(wordPrefab, wordList.transform);
            wordObject.name = "Word-" + word;
            var tmpComponent = wordObject.GetComponentInChildren<TextMeshProUGUI>();
            tmpComponent.text = word;
            tmpComponent.color = DialogueManager.currentSegment is not { interactable: true }
                ? new Color(0, 0, 0, 0.2f)
                : new Color(0, 0, 0, 1f);
            wordObject.GetComponent<Button>().onClick.AddListener(() => UseOrReplaceEquippedWord(word));
        }

        /// <summary>
        /// Method <c>RefreshEquippedWordListUI</c> refreshes the equipped word list.
        /// </summary>
        private void RefreshEquippedWordListUI(bool forceInteraction)
        {
            if (DialogueManager.currentSegment == null) return;
            foreach (var child in wordList.GetComponentsInChildren<TextMeshProUGUI>())
            {
                child.color = (DialogueManager.currentSegment.interactable | forceInteraction)
                    ? new Color(0, 0, 0, 1f)
                    : new Color(0, 0, 0, 0.2f);
            }
        }
    }
}
