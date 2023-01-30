using UnityEngine;
using PF.Managers;

namespace PF.Entities
{
    public class EventTile : MonoBehaviour
    {
        public EventManager eventManager;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            // Switch this object name
            switch (gameObject.name)
            {
                case "B1Start":
                    ToggleGate("1A");
                    Destroy(gameObject);
                    break;
                case "B1End":
                    ToggleGate("1B");
                    Destroy(gameObject);
                    break;
                case "B2Start":
                    ToggleGate("2A");
                    Destroy(gameObject);
                    break;
                case "B2End":
                    ToggleGate("2B");
                    Destroy(gameObject);
                    break;
                case "B3Start":
                    ToggleGate("3A");
                    Destroy(gameObject);
                    break;
                case "B3End":
                    ToggleGate("3B");
                    Destroy(gameObject);
                    break;
                case "B4Start":
                    ToggleGate("4A");
                    Destroy(gameObject);
                    break;
                case "B4End":
                    ToggleGate("4B");
                    Destroy(gameObject);
                    break;
                case "B5Start":
                    ToggleGate("5A");
                    Destroy(gameObject);
                    break;
                case "B5End":
                    ToggleGate("5B");
                    Destroy(gameObject);
                    break;
                case "B6Start":
                    ToggleGate("6A");
                    Destroy(gameObject);
                    break;
                case "B6End":
                    ToggleGate("6B");
                    Destroy(gameObject);
                    break;
                case "B7Start":
                    ToggleGate("7A");
                    Destroy(gameObject);
                    break;
                case "B7End":
                    ToggleGate("7B");
                    Destroy(gameObject);
                    break;
                case "B8Start":
                    ToggleGate("8A");
                    Destroy(gameObject);
                    break;
                case "B8End":
                    ToggleGate("8B");
                    Destroy(gameObject);
                    break;
                case "B9Start":
                    ToggleGate("9A");
                    SwitchBackgroundMusic("dark_blue");
                    Destroy(gameObject);
                    break;
                case "B9End":
                    ToggleGate("9B");
                    Destroy(gameObject);
                    break;
                case "TEST":
                    StartDialogue(1);
                    break;
            }

        }

        private void StartDialogue(int dialogueSegmentId)
        {
            eventManager.StartDialogue(dialogueSegmentId);
        }

        private void SwitchBackgroundMusic(string musicName)
        {
            eventManager.SwitchBackgroundMusic(musicName);
        }

        private void ToggleGate(string gateNumber)
        {
            eventManager.ToggleGate(gateNumber);
        }
    }
}
