using System.Collections.Generic;
using UnityEngine;
using PF.Entities;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>DialogueManager</c> contains the methods and properties needed for the dialogues.
    /// </summary>
    public static class DialogueManager
    {
        /// <value>Property <c>words</c> contains the words.</value>
        public static string[] words;
        
        /// <value>Property <c>wordsLearned</c> contains the words learned.</value>
        public static string[] wordsLearned;
        
        /// <value>Property <c>wordsEquipped</c> contains the words equipped.</value>
        public static string[] wordsEquipped;
        
        /// <value>Property <c>wordsToLearn</c> contains the words to learn.</value>
        public static string[] wordsToLearn;
        
        /// <value>Property <c>segments</c> contains the dialogue segments.</value>
        public static Dictionary<int, DialogueSegment> segments;
        
        /// <value>Property <c>currentSegment</c> represents the current dialogue segment.</value>
        public static DialogueSegment currentSegment;

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        static DialogueManager()
        {
            // Load words
            ImportWords();
            
            // Load dialogue segments
            segments = new Dictionary<int, DialogueSegment>();
            ImportDialogueSegments();
        }
        
        /// <summary>
        /// Method <c>ImportWords</c> imports the words from the JSON file.
        /// </summary>
        private static void ImportWords()
        {
            var wordsJson = Resources.Load<TextAsset>("Words");
            words = JsonUtility
                .FromJson<DialogueWordList>("{\"words\":" + wordsJson.text + "}").words;
        }

        /// <summary>
        /// Method <c>ImportDialogueSegments</c> imports the dialogue segments from the JSON file.
        /// </summary>
        private static void ImportDialogueSegments()
        {
            var dialogueSegmentsJson = Resources.Load<TextAsset>("DialogueSegments");
            var dialogueSegments = JsonUtility
                .FromJson<DialogueSegmentDictionary>("{\"dialogueSegments\":" + dialogueSegmentsJson.text + "}")
                .dialogueSegments;
            foreach (var dialogueSegment in dialogueSegments)
            {
                segments.Add(dialogueSegment.id, dialogueSegment);
            }
        }
    }
}
