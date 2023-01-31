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
        public bool tutorial1Completed;
        public bool tutorial2Completed;
        public bool tutorial3Completed;
        public bool death1Seen;
        public bool death2Seen;
        public bool death3Seen;
        public bool death4Seen;
        public bool death5Seen;
        public bool death6Seen;
        public bool death7Seen;
        public bool death8Seen;
        public List<string> wordsLearned;
        public List<string> wordsEquipped;
        
        public PlayerProgress()
        {
            tutorial1Completed = false;
            tutorial2Completed = false;
            tutorial3Completed = false;
            death1Seen = false;
            death2Seen = false;
            death3Seen = false;
            death4Seen = false;
            death5Seen = false;
            death6Seen = false;
            death7Seen = false;
            death8Seen = false;
            wordsLearned = new List<string>();
            wordsEquipped = new List<string>();
        }

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
