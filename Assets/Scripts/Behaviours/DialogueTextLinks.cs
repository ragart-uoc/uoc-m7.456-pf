using PF.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace PF.Behaviours
{
    /// <summary>
    /// Class <c>DialogueTextLinks</c> contains the methods and properties needed for the links in the dialogues.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class DialogueTextLinks : MonoBehaviour, IPointerClickHandler
    {
        /// <value>Property <c>gameManager</c> represents the instance of the GameManager.</value>
        public GameManager gameManager;

        /// <value>Property <c>_text</c> represents the instance of the TMP_Text.</value>
        private TMP_Text _text;

        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            _text = GetComponent<TMP_Text>();
        }

        /// <summary>
        /// Method <c>OnPointerClick</c> is called when the pointer is clicked.
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(_text,
                new Vector3(eventData.position.x, eventData.position.y, 0), null);
            if (linkIndex == -1) return;
            var word = _text.textInfo.linkInfo[linkIndex].GetLinkID();
            gameManager.EquipWord(word);
        }
    }
}