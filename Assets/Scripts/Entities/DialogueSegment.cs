using System.Collections.Generic;

namespace PF.Entities
{
    [System.Serializable]
    public class DialogueSegment
    {
        public int id;
        public string speaker;
        public string content;
        public int nextSegment;
        public int bridgeOpen;
        public int ending;
        public bool interactable;
        public List<DialogueAffectingWord> affectingWords;
    }
}
