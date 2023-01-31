using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

        /// <value>Property <c>title</c> represents TextMeshProUGUI component for the title of the page.</value>
        public TextMeshProUGUI title;
        
        /// <value>Property <c>wordPrefab</c> represents the prefab for the word.</value>
        public GameObject wordPrefab;

        /// <value>Property <c>wordList</c> represents the gameobject for the word list.</value>
        public GameObject wordList;
        
        /// <value>Property <c>maxEquipableWords</c> represents the maximum number of equipable words.</value>
        private int _maxEquipableWords = 4;

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
            DialogueManager.wordsEquipped = Array.Empty<string>();
        }

        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            // Change the title of the page depending on the number of learned words
            var learnedWordsCount = _persistentDataManager.learnedWords.Count;
            if (learnedWordsCount <= 0)
                SceneManager.LoadScene("Game");
            _maxEquipableWords = (learnedWordsCount < 4) ? learnedWordsCount : 4;
            var titleText = "Equip up to " + _maxEquipableWords + " words";
            title.text = titleText;
            
            // Remove all words in the UI and add every possible word
            foreach (Transform child in wordList.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var word in DialogueManager.words)
            {
                var wordObject = Instantiate(wordPrefab, wordList.transform);
                wordObject.name = "Word-" + word;
                var tmpComponent = wordObject.GetComponentInChildren<TextMeshProUGUI>();
                tmpComponent.text = word;
                if (!_persistentDataManager.learnedWords.Contains(word))
                {
                    tmpComponent.color = new Color(0, 0, 0, 0);
                    tmpComponent.gameObject.GetComponent<Button>().interactable = false;
                }
                wordObject.GetComponent<Button>().onClick.AddListener(() => EquipOrUnequipWord(word));
            }
        }
        
        private void EquipOrUnequipWord(string word)
        {
            if (_persistentDataManager.equippedWords.Contains(word))
                RemoveWord(word);
            else
                EquipWord(word);
        }

        private void EquipWord(string word)
        {
            if (!_persistentDataManager.learnedWords.Contains(word)
                || _persistentDataManager.equippedWords.Count >= _maxEquipableWords)
                return;
            _persistentDataManager.equippedWords.Add(word);
            _persistentDataManager.SavePlayerProgress();
            DialogueManager.RefreshWords(_persistentDataManager.learnedWords, _persistentDataManager.equippedWords);
            GameObject.Find("Word-" + word).GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 0, 0, 0.8f);
            if (_persistentDataManager.equippedWords.Count >= _maxEquipableWords)
                SceneManager.LoadScene("Game");
        }
        
        private void RemoveWord(string word)
        {
            if (!_persistentDataManager.equippedWords.Contains(word))
                return;
            _persistentDataManager.equippedWords.Remove(word);
            _persistentDataManager.SavePlayerProgress();
            DialogueManager.RefreshWords(_persistentDataManager.learnedWords, _persistentDataManager.equippedWords);
            GameObject.Find("Word-" + word).GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
        }
    }
}
