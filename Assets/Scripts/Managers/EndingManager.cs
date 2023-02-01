using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using TMPro;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>EndingManager</c> contains the methods and properties needed for the ending.
    /// </summary>
    public class EndingManager : MonoBehaviour
    {
        /// <value>Property <c>_persistentDataManager</c> represents the instance of the PersistentDataManager.</value>
        private PersistentDataManager _persistentDataManager;
        
        /// <value>Property <c>room</c> represents the tilemap for the room.</value>
        public Tilemap room;
        
        /// <value>Property <c>player</c> represents the sprite renderer for the player.</value>
        public SpriteRenderer player;
        
        /// <value>Property <c>dialoguePanel</c> represents the transform for the dialogue panel.</value>
        public Transform dialoguePanel;
        
        /// <value>Property <c>dialogueTitleText</c> represents the TextMeshProUGUI component for the dialogue title.</value>
        private TextMeshProUGUI _dialogueTitleText;
        
        /// <value>Property <c>dialogueText</c> represents the TextMeshProUGUI component for the dialogue text.</value>
        private TextMeshProUGUI _dialogueText;

        /// <value>Property <c>endingName</c> represents the TextMeshProUGUI component for the ending name.</value>
        public TextMeshProUGUI endingNameText;

        /// <value>Property <c>credits</c> represents the TextMeshProUGUI component for the credits.</value>
        public TextMeshProUGUI creditsText;

        /// <value>Property <c>_audioSource</c> represents the AudioSource component.</value>
        private AudioSource _audioSource;
        
        /// <value>Property <c>_phoneRing</c> represents the phone ring audio clip.</value>
        private AudioClip _phoneRing;
        
        /// <value>Property <c>_sadMusic</c> represents the sad music audio clip.</value>
        private AudioClip _sadMusic;
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private IEnumerator Start()
        {
            _persistentDataManager = GameObject.Find("PersistentDataManager").GetComponent<PersistentDataManager>();
            
            _dialogueTitleText = dialoguePanel.Find("DialogueTitleText").GetComponent<TextMeshProUGUI>();
            _dialogueText = dialoguePanel.Find("DialogueText").GetComponent<TextMeshProUGUI>();

            _audioSource = GetComponent<AudioSource>();
            _phoneRing = Resources.Load<AudioClip>("Sounds/phone");
            _sadMusic = Resources.Load<AudioClip>("Music/sadness");
            _audioSource.clip = _phoneRing;
            _audioSource.Play();

            room.color = new Color(1, 1, 1, 0);
            player.color = new Color(1, 1, 1, 0);

            StartCoroutine(Fade.FadeTilemapColorAlpha(room, 1, 5));
            if (_persistentDataManager.endingReached is > 0 and < 9)
            {
                StartCoroutine(Fade.FadeSpriteRendererColorAlpha(player, 1, 5));    
            }
            
            yield return new WaitForSeconds(8);
            
            _audioSource.Stop();
            _audioSource.clip = _sadMusic;
            _audioSource.Play();

            switch (_persistentDataManager.endingReached)
            {
                case 1:
                    StartCoroutine(PlayDialogue(30, "What if...?"));
                    break;
                case 2:
                    StartCoroutine(PlayDialogue(33, "Carpe Diem"));
                    break;
                case 3:
                    StartCoroutine(PlayDialogue(36, "My only friend, the end"));
                    break;
                case 4:
                    StartCoroutine(PlayDialogue(26, "Perfect strangers"));
                    break;
                case 5:
                    StartCoroutine(PlayDialogue(39, "Til death do us part"));
                    break;
                case 6:
                    StartCoroutine(PlayDialogue(42, "Avoid like the plague"));
                    break;
                case 7:
                    StartCoroutine(PlayDialogue(45, "This needs to stop happening"));
                    break;
                case 8:
                    StartCoroutine(PlayDialogue(29, "Drowning in tears"));
                    break;
                case 9:
                    yield return new WaitForSeconds(10);
                    StartCoroutine(Fade.FadeTilemapColorAlpha(room, 0, 5));
                    yield return new WaitForSeconds(5);
                    endingNameText.text = "Logos";
                    endingNameText.gameObject.SetActive(true);
                    yield return new WaitForSeconds(5);
                    endingNameText.gameObject.SetActive(false);
                    creditsText.gameObject.SetActive(true);
                    yield return new WaitForSeconds(5);
                    SceneManager.LoadScene("MainMenu");
                    yield break;
                default:
                    StartCoroutine(Fade.FadeTilemapColorAlpha(room, 0, 5));
                    yield return new WaitForSeconds(5);
                    endingNameText.text = "System Error";
                    endingNameText.gameObject.SetActive(true);
                    yield return new WaitForSeconds(5);
                    LoopBack();
                    break;
            }
        }
        
        private IEnumerator PlayDialogue(int dialogueSegmentId, string endingName)
        {
            DialogueManager.SetCurrentSegment(dialogueSegmentId);
            var moreSegments = true;
            do
            {
                _dialogueTitleText.text = DialogueManager.currentSegment.speaker;
                _dialogueText.text = DialogueManager.currentSegment.content;
                dialoguePanel.gameObject.SetActive(true);
                yield return new WaitForSeconds(5);
                dialoguePanel.gameObject.SetActive(false);
                if (DialogueManager.currentSegment.nextSegment > 0)
                {
                    DialogueManager.SetCurrentSegment(DialogueManager.currentSegment.nextSegment);
                }
                else
                {
                    moreSegments = false;
                }
            } while (moreSegments);
            
            StartCoroutine(Fade.FadeTilemapColorAlpha(room, 0, 5));
            StartCoroutine(Fade.FadeSpriteRendererColorAlpha(player, 0, 5));
            
            yield return new WaitForSeconds(5);
            
            endingNameText.text = endingName;
            endingNameText.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(5);
            
            endingNameText.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(2);
            
            LoopBack();
        }
        
        private void LoopBack()
        {
            // Learn all equipped words
            if (_persistentDataManager.equippedWords.Count > 0)
            {
                foreach (var equippedWord in _persistentDataManager.equippedWords
                             .Where(equippedWord => _persistentDataManager.learnedWords.Contains(equippedWord)))
                {
                    _persistentDataManager.learnedWords.Add(equippedWord);
                }
                _persistentDataManager.SavePlayerProgress();
                DialogueManager.RefreshWords(_persistentDataManager.learnedWords, _persistentDataManager.equippedWords);
            }

            // Load scene
            SceneManager.LoadScene(_persistentDataManager.learnedWords.Count > 0 ? "Game" : "WordSelection");
        }
    }
}
