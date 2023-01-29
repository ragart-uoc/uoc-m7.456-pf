using UnityEngine;
using UnityEngine.Tilemaps;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>TileManager</c> contains the methods and properties needed for managing tiles.
    /// </summary>
    public class TileManager : MonoBehaviour
    {
        /// <value>Property <c>_instance</c> represents the instance of the TileManager.</value>
        private TileManager _instance;

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            // Singleton pattern
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = this;
        }
        
        /// <summary>
        /// Method <c>DeleteEventFile</c> deletes the event tile at the given position.
        /// </summary>
        public void DeleteEventTile(string position)
        { 
            // Get the tilemap
            var tilemap = GameObject.FindWithTag("Event").GetComponent<Tilemap>();
            // Get the tile position
            var tilePosition = new Vector3Int(
                int.Parse(position.Split(',')[0]),
                int.Parse(position.Split(',')[1]),
                0
            );
            // Set the tile to null
            tilemap.SetTile(tilePosition, null);
        }

        public void ToggleGate(string gateNumber)
        {
            // Get the tilemap collider
            var tilemapCollider = GameObject.Find("Gate" + gateNumber).GetComponent<TilemapCollider2D>();
            // Toggle the tilemap collider
            tilemapCollider.enabled = !tilemapCollider.enabled;
        }
        
        public void ToggleBridge(string bridgeNumber)
        {
            // Get the tilemap renderer
            var tilemapRenderer = GameObject.Find("Bridge" + bridgeNumber).GetComponent<TilemapRenderer>();
            // Toggle the tilemap
            tilemapRenderer.enabled = !tilemapRenderer.enabled;
        }
        
        public void SwitchBackgroundMusic(string musicName)
        {
            // Get the audio source
            var audioSource = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
            // Get the audio clip
            var audioClip = Resources.Load<AudioClip>("Music/" + musicName);
            // Change the audio clip
            audioSource.clip = audioClip;
            // Play the audio clip
            audioSource.Play();
        }
    }
}
