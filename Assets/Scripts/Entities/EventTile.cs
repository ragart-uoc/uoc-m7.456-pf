using UnityEngine;
using PF.Managers;

namespace PF.Entities
{
    /// <summary>
    /// Class <c>EventTile</c> contains the methods and properties needed for the event tiles.
    /// </summary>
    public class EventTile : MonoBehaviour
    {
        /// <value>Property <c>GameManager</c> represents the instance of the GameManager.</value>
        public GameManager gameManager;
        
        /// <summary>
        /// Method <c>OnTriggerEnter2D</c> is sent when another object enters a trigger collider attached to this object.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D col)
        {
            // Switch this object name
            switch (gameObject.name)
            {
                case "B1Start":
                    gameManager.ToggleGate("1A");
                    gameManager.ToggleGate("1B");
                    Destroy(gameObject);
                    break;
                case "B1End":
                    gameManager.ToggleGate("1B");
                    Destroy(gameObject);
                    break;
                case "B1Dialogue":
                    gameManager.StartDialogue(51);
                    Destroy(gameObject);
                    break;
                case "B2Start":
                    gameManager.ToggleGate("2A");
                    gameManager.ToggleGate("2B");
                    Destroy(gameObject);
                    break;
                case "B2End":
                    gameManager.ToggleGate("2B");
                    Destroy(gameObject);
                    break;
                case "B2Dialogue":
                    gameManager.StartDialogue(57);
                    Destroy(gameObject);
                    break;
                case "B3Start":
                    gameManager.ToggleGate("3A");
                    gameManager.ToggleGate("3B");
                    Destroy(gameObject);
                    break;
                case "B3End":
                    gameManager.ToggleGate("3B");
                    Destroy(gameObject);
                    break;
                case "B3Dialogue":
                    gameManager.StartDialogue(63);
                    Destroy(gameObject);
                    break;
                case "B4Start":
                    gameManager.ToggleGate("4A");
                    gameManager.ToggleGate("4B");
                    Destroy(gameObject);
                    break;
                case "B4End":
                    gameManager.ToggleGate("4B");
                    Destroy(gameObject);
                    break;
                case "B4Dialogue":
                    gameManager.StartDialogue(69);
                    Destroy(gameObject);
                    break;
                case "B5Start":
                    gameManager.ToggleGate("5A");
                    gameManager.ToggleGate("5B");
                    Destroy(gameObject);
                    break;
                case "B5End":
                    gameManager.ToggleGate("5B");
                    Destroy(gameObject);
                    break;
                case "B5Dialogue":
                    gameManager.StartDialogue(76);
                    Destroy(gameObject);
                    break;
                case "B6Start":
                    gameManager.ToggleGate("6A");
                    gameManager.ToggleGate("6B");
                    Destroy(gameObject);
                    break;
                case "B6End":
                    gameManager.ToggleGate("6B");
                    Destroy(gameObject);
                    break;
                case "B6Dialogue":
                    gameManager.StartDialogue(82);
                    Destroy(gameObject);
                    break;
                case "B7Start":
                    gameManager.ToggleGate("7A");
                    gameManager.ToggleGate("7B");
                    Destroy(gameObject);
                    break;
                case "B7End":
                    gameManager.ToggleGate("7B");
                    Destroy(gameObject);
                    break;
                case "B7Dialogue":
                    gameManager.StartDialogue(89);
                    Destroy(gameObject);
                    break;
                case "B8Start":
                    gameManager.ToggleGate("8A");
                    gameManager.ToggleGate("8B");
                    Destroy(gameObject);
                    break;
                case "B8End":
                    gameManager.ToggleGate("8B");
                    Destroy(gameObject);
                    break;
                case "B8Dialogue":
                    gameManager.StartDialogue(95);
                    Destroy(gameObject);
                    break;
                case "B9Start":
                    gameManager.ToggleGate("9A");
                    gameManager.ToggleGate("9B");
                    gameManager.SwitchBackgroundMusic("dark_blue");
                    Destroy(gameObject);
                    break;
                case "B9End":
                    gameManager.ToggleGate("9B");
                    Destroy(gameObject);
                    break;
                case "B9Dialogue":
                    gameManager.StartDialogue(15);
                    break;
            }

        }
    }
}
