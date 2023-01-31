using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace PF.Behaviours
{
    [RequireComponent(typeof(TMP_Text))]
    public class DialogueTextLinks : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text _textMeshPro;

        private void Start()
        {
            _textMeshPro = GetComponent<TMP_Text>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(_textMeshPro,
                new Vector3(eventData.position.x, eventData.position.y, 0), null);
            if (linkIndex == -1) return;
            var linkInfo = _textMeshPro.textInfo.linkInfo[linkIndex];
            Debug.Log(linkInfo);
        }
    }
}