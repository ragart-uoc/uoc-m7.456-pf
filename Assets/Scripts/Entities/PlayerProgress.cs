using System.Collections.Generic;
using UnityEngine;

namespace PF.Entities
{
    /// <summary>
    /// Class <c>PlayerProgress</c> contains the properties of the player's progress.
    /// </summary>
    [System.Serializable]
    public class PlayerProgress
    {
        public bool tutorialCompleted;
        public bool death1Seen;
        public bool death2Seen;
        public bool death3Seen;
        public bool death4Seen;
        public bool death5Seen;
        public bool death6Seen;
        public bool death7Seen;
        public bool death8Seen;
        public List<string> wordsLearned;

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
        
        public PlayerProgress FromJson(string json)
        {
            return JsonUtility.FromJson<PlayerProgress>(json);
        }
    }
}
