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
        /// <value>Property <c>Words</c> contains the words.</value>
        public static string[] Words;
        
        /// <value>Property <c>WordsLearned</c> contains the words learned.</value>
        public static string[] WordsLearned;
        
        /// <value>Property <c>WordsEquipped</c> contains the words equipped.</value>
        public static string[] WordsEquipped;
        
        /// <value>Property <c>WordsToLearn</c> contains the words to learn.</value>
        public static string[] WordsToLearn;
        
        /// <value>Property <c>Segments</c> contains the dialogue segments.</value>
        public static Dictionary<int, DialogueSegment> Segments;
        
        /// <value>Property <c>CurrentSegment</c> represents the current dialogue segment.</value>
        public static DialogueSegment CurrentSegment;

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        static DialogueManager()
        {
            // Load words
            ImportWords();
            
            // Load dialogue segments
            Segments = new Dictionary<int, DialogueSegment>();
            ImportDialogueSegments();
        }
        
        /// <summary>
        /// Method <c>Start</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        /*
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
        */
        
        /// <summary>
        /// Method <c>ImportWords</c> imports the words from the JSON file.
        /// </summary>
        private static void ImportWords()
        {
            var wordsJson = Resources.Load<TextAsset>("Words");
            Words = JsonUtility
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
    }
}
