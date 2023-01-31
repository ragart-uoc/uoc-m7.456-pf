using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public static readonly Dictionary<int, DialogueSegment> Segments;
        
        /// <value>Property <c>currentSegment</c> represents the current dialogue segment.</value>
        public static DialogueSegment currentSegment;

        /// <summary>
        /// Method <c>DialogueManager</c> is the constructor of the class.
        /// </summary>
        static DialogueManager()
        {
            // Load words
            ImportWords();
            
            // Load dialogue segments
            Segments = new Dictionary<int, DialogueSegment>();
            ImportDialogueSegments();
            
            // Initialize words learned
            wordsLearned = Array.Empty<string>();
            
            // Initialize words equipped
            wordsEquipped = Array.Empty<string>();
            
            // Initialize words to learn
            wordsToLearn = Array.Empty<string>();
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
                Segments.Add(dialogueSegment.id, dialogueSegment);
            }
        }
        
        /// <summary>
        /// Method <c>SetCurrentSegment</c> sets the current dialogue segment.
        /// </summary>
        public static void SetCurrentSegment(int id)
        {
            currentSegment = Segments[id];
        }

        /// <summary>
        /// Method <c>ProcessText</c> processes the text in search of words.
        /// </summary>
        public static string ProcessText(int id)
        {
            var processedText = currentSegment.content;
            var wordsRegex = new Regex(@"\w+");
            foreach (var textWord in wordsRegex.Matches(processedText).Select(m => m.Value))
            {
                var word = textWord.ToLower();
                if (!words.Contains(word) || wordsLearned.Contains(word) || wordsEquipped.Contains(word)) continue;
                var newText = "<color=red><link=\"" + word + "\">" + textWord + "</link></color>";
                processedText = processedText.Replace(textWord, newText);

            }
            return processedText;
        }
        
        /// <summary>
        /// Method <c>RefreshWords</c> refreshes the lists of words.
        /// </summary>
        public static void RefreshWords(List<string> wordsLearnedList, List<string> wordsEquippedList)
        {
            wordsLearned = wordsLearnedList.ToArray();
            wordsEquipped = wordsEquippedList.ToArray();
            wordsToLearn = words.Except(wordsLearned).ToArray();
        }
        
    }
}
