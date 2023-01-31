using System.Collections.Generic;
using UnityEngine;
using PF.Entities;

namespace PF.Managers
{
    public class PersistentDataManager : MonoBehaviour
    {
        /// <value>Property <c>_instance</c> represents the instance of the PersistentDataManager.</value>
        private static PersistentDataManager _instance;
        
        /// <value>Property <c>playerProgress</c> represents the player's progress.</value>
        public PlayerProgress playerProgress;
        
        /// <value>Property <c>learnedWords</c> represents the words that are learned.</value>
        public List<string> learnedWords;
        
        /// <value>Property <c>equippedWords</c> represents the words that are equipped.</value>
        public List<string> equippedWords;

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
            DontDestroyOnLoad(this.gameObject);
            
            // Load game data or create new game data
            var playerProgressJson = PlayerPrefs.GetString("PlayerProgress");
            playerProgress = playerProgressJson == ""
                ? new PlayerProgress()
                : playerProgress.FromJson(playerProgressJson);
            learnedWords = playerProgress.wordsLearned;
            equippedWords = playerProgress.wordsEquipped;
            SavePlayerProgress();
            
            // Create empty equipped words list
            equippedWords = new List<string>();
        }
        
        public void SavePlayerProgress()
        {
            playerProgress.wordsLearned = learnedWords;
            playerProgress.wordsEquipped = equippedWords;
            PlayerPrefsManager.SavePlayerProgress(playerProgress.ToJson());
        }
        
        public void ResetEquippedWords()
        {
            equippedWords.Clear();
            SavePlayerProgress();
        }

        public bool AllTutorialsCompleted()
        {
            return playerProgress.tutorial1Completed
                   && playerProgress.tutorial2Completed
                   && playerProgress.tutorial3Completed;
        }

        public bool AllDeathsSeen()
        {
            return playerProgress.death1Seen
                   && playerProgress.death2Seen
                   && playerProgress.death3Seen
                   && playerProgress.death4Seen
                   && playerProgress.death5Seen
                   && playerProgress.death6Seen
                   && playerProgress.death7Seen
                   && playerProgress.death8Seen;
        }
    }
}
