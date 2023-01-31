using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>WordSelectionManager</c> contains the methods and properties needed for the word selection.
    /// </summary>
    public class WordSelectionManager : MonoBehaviour
    {
        /// <value>Property <c>_instance</c> represents the instance of the WordSelectionManager.</value>
        private static WordSelectionManager _instance;
        
        /// <value>Property <c>_persistentDataManager</c> represents the instance of the PersistentDataManager.</value>
        private PersistentDataManager _persistentDataManager;
        
        /// <value>Property <c>wordPrefab</c> represents the prefab for the word.</value>
        public GameObject wordPrefab;

        /// <value>Property <c>wordList</c> represents the gameobject for the word list.</value>
        public GameObject wordList;

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
            
            // Reset equipped words
            _persistentDataManager.ResetEquippedWords();
        }

        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            // DEBUG
            _persistentDataManager.learnedWords.Add("death");
            _persistentDataManager.learnedWords.Add("love");
            _persistentDataManager.learnedWords.Add("serendipity");
            
            // Remove all words in the UI and add every possible word
            foreach (Transform child in wordList.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var word in DialogueManager.words)
            {
                var wordObject = Instantiate(wordPrefab, wordList.transform);
                var tmpComponent = wordObject.GetComponentInChildren<TextMeshProUGUI>();
                tmpComponent.text = word;
                if (!_persistentDataManager.learnedWords.Contains(word))
                {
                    tmpComponent.color = new Color(0, 0, 0, 0);
                    tmpComponent.gameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}
