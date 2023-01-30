using NTW.TilemapEvents;
using UnityEngine;
using UnityEngine.SceneManagement;
using PF.Entities;
using UnityEngine.Tilemaps;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>GameManager</c> contains the methods and properties needed for the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <value>Property <c>_instance</c> represents the instance of the GameManager.</value>
        private static GameManager _instance;
        
        /// <value>Property <c>_playerProgress</c> represents the player's progress.</value>
        private PlayerProgress _playerProgress;

        /// <value>Property <c>_pausePanel</c> represents the game object for the player.</value>
        private GameObject _player;

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
            DontDestroyOnLoad(this.gameObject);
        }
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            var playerProgressJson = PlayerPrefs.GetString("PlayerProgress");
            _playerProgress = playerProgressJson == ""
                ? new PlayerProgress()
                : _playerProgress.FromJson(playerProgressJson);
        }
        
        /// <summary>
        /// Method <c>OnEnable</c> is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        /// <summary>
        /// Method <c>OnDisable</c> is called when the behaviour becomes disabled.
        /// </summary>
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        /// <summary>
        /// Method <c>OnSceneLoaded</c> is called when the scene is loaded.
        /// </summary>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case "Game":
                    _player = GameObject.FindWithTag("Player");
                    // Get all tilemaps with tag "EditorOnly"
                    var tilemaps = GameObject.FindGameObjectsWithTag("EditorOnly");
                    // Disable all tilemaps with tag "EditorOnly"
                    foreach (var tilemap in tilemaps)
                    {
                        tilemap.GetComponent<TilemapRenderer>().enabled = false;
                    }
                    break;
            }
        }
    }
}
