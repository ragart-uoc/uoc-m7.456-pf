using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using TMPro;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>IntroManager</c> contains the methods and properties needed for the intro sequence.
    /// </summary>
    public class IntroManager : MonoBehaviour
    {
        /// <value>Property <c>room</c> represents the tilemap for the room.</value>
        public Tilemap room;
        
        /// <value>Property <c>unknown</c> represents the tilemap for the unknown.</value>
        public Tilemap unknown;
        
        /// <value>Property <c>player</c> represents the sprite renderer for the player.</value>
        public SpriteRenderer player;
        
        /// <value>Property <c>dialoguePanel</c> represents the transform for the dialogue panel.</value>
        public Transform dialoguePanel;
        
        /// <value>Property <c>dialogueTitleText</c> represents the TextMeshProUGUI component for the dialogue title.</value>
        private TextMeshProUGUI _dialogueTitleText;
        
        /// <value>Property <c>dialogueText</c> represents the TextMeshProUGUI component for the dialogue text.</value>
        private TextMeshProUGUI _dialogueText;

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
            _dialogueTitleText = dialoguePanel.Find("DialogueTitleText").GetComponent<TextMeshProUGUI>();
            _dialogueText = dialoguePanel.Find("DialogueText").GetComponent<TextMeshProUGUI>();

            _audioSource = GetComponent<AudioSource>();
            _phoneRing = Resources.Load<AudioClip>("Sounds/phone");
            _sadMusic = Resources.Load<AudioClip>("Music/sadness");
            _audioSource.clip = _phoneRing;
            _audioSource.Play();

            room.color = new Color(1, 1, 1, 0);
            unknown.color = new Color(1, 1, 1, 0);
            player.color = new Color(1, 1, 1, 0);

            StartCoroutine(Fade.FadeTilemapColorAlpha(room, 1, 5));
            StartCoroutine(Fade.FadeSpriteRendererColorAlpha(player, 1, 5));

            yield return new WaitForSeconds(10);

            _dialogueTitleText.text = "Character";
            _dialogueText.text = DialogueManager.Segments[1].content;

            dialoguePanel.gameObject.SetActive(true);

            yield return new WaitForSeconds(2);

            dialoguePanel.gameObject.SetActive(false);
            _dialogueText.text = DialogueManager.Segments[2].content;

            yield return new WaitForSeconds(0.5f);

            dialoguePanel.gameObject.SetActive(true);

            yield return new WaitForSeconds(2);

            dialoguePanel.gameObject.SetActive(false);
            _dialogueText.text = DialogueManager.Segments[3].content;

            yield return new WaitForSeconds(0.5f);

            dialoguePanel.gameObject.SetActive(true);

            yield return new WaitForSeconds(5);

            dialoguePanel.gameObject.SetActive(false);
            _dialogueText.text = DialogueManager.Segments[4].content;

            yield return new WaitForSeconds(0.5f);

            dialoguePanel.gameObject.SetActive(true);

            StartCoroutine(Fade.FadeTilemapColorAlpha(room, 0, 5));
            StartCoroutine(Fade.FadeTilemapColorAlpha(unknown, 1, 5));

            _audioSource.clip = _sadMusic;
            _audioSource.Play();

            yield return new WaitForSeconds(5);

            dialoguePanel.gameObject.SetActive(false);
            _dialogueText.text = DialogueManager.Segments[5].content;

            yield return new WaitForSeconds(0.5f);

            dialoguePanel.gameObject.SetActive(true);

            yield return new WaitForSeconds(5);

            dialoguePanel.gameObject.SetActive(false);
            _dialogueText.text = DialogueManager.Segments[6].content;

            yield return new WaitForSeconds(0.5f);

            dialoguePanel.gameObject.SetActive(true);

            yield return new WaitForSeconds(5);

            dialoguePanel.gameObject.SetActive(false);
            _dialogueText.text = DialogueManager.Segments[7].content;

            yield return new WaitForSeconds(0.5f);

            dialoguePanel.gameObject.SetActive(true);

            StartCoroutine(Fade.FadeTilemapColorAlpha(unknown, 0, 5));
            StartCoroutine(Fade.FadeSpriteRendererColorAlpha(player, 0, 5));

            yield return new WaitForSeconds(5);

            SceneManager.LoadScene("MainMenu");
        }

        /// <summary>
        /// Method <c>Update</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0)
                                      || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
