using UnityEngine;
using PF.Managers;

namespace PF.Entities
{
    public class EventTile : MonoBehaviour
    {
        public GameManager gameManager;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            // Switch this object name
            switch (gameObject.name)
            {
                case "B1Start":
                    ToggleGate("1A");
                    ToggleGate("1B");
                    Destroy(gameObject);
                    break;
                case "B1End":
                    ToggleGate("1B");
                    Destroy(gameObject);
                    break;
                case "B1Dialogue":
                    break;
                case "B2Start":
                    ToggleGate("2A");
                    ToggleGate("2B");
                    Destroy(gameObject);
                    break;
                case "B2End":
                    ToggleGate("2B");
                    Destroy(gameObject);
                    break;
                case "B2Dialogue":
                    break;
                case "B3Start":
                    ToggleGate("3A");
                    ToggleGate("3B");
                    Destroy(gameObject);
                    break;
                case "B3End":
                    ToggleGate("3B");
                    Destroy(gameObject);
                    break;
                case "B3Dialogue":
                    break;
                case "B4Start":
                    ToggleGate("4A");
                    ToggleGate("4B");
                    Destroy(gameObject);
                    break;
                case "B4End":
                    ToggleGate("4B");
                    Destroy(gameObject);
                    break;
                case "B4Dialogue":
                    break;
                case "B5Start":
                    ToggleGate("5A");
                    ToggleGate("5B");
                    Destroy(gameObject);
                    break;
                case "B5End":
                    ToggleGate("5B");
                    Destroy(gameObject);
                    break;
                case "B5Dialogue":
                    break;
                case "B6Start":
                    ToggleGate("6A");
                    ToggleGate("6B");
                    Destroy(gameObject);
                    break;
                case "B6End":
                    ToggleGate("6B");
                    Destroy(gameObject);
                    break;
                case "B6Dialogue":
                    break;
                case "B7Start":
                    ToggleGate("7A");
                    ToggleGate("7B");
                    Destroy(gameObject);
                    break;
                case "B7End":
                    ToggleGate("7B");
                    Destroy(gameObject);
                    break;
                case "B7Dialogue":
                    break;
                case "B8Start":
                    ToggleGate("8A");
                    ToggleGate("8B");
                    Destroy(gameObject);
                    break;
                case "B8End":
                    ToggleGate("8B");
                    Destroy(gameObject);
                    break;
                case "B8Dialogue":
                    break;
                case "B9Start":
                    ToggleGate("9A");
                    ToggleGate("9B");
                    SwitchBackgroundMusic("dark_blue");
                    Destroy(gameObject);
                    break;
                case "B9End":
                    ToggleGate("9B");
                    Destroy(gameObject);
                    break;
                case "B9Dialogue":
                    StartDialogue(15);
                    break;
            }

        }

        private void StartDialogue(int dialogueSegmentId)
        {
            gameManager.StartDialogue(dialogueSegmentId);
        }

        private void SwitchBackgroundMusic(string musicName)
        {
            gameManager.SwitchBackgroundMusic(musicName);
        }

        private void ToggleGate(string gateNumber)
        {
            gameManager.ToggleGate(gateNumber);
        }
    }
}
