using UnityEngine;
using UnityEngine.UI;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>PlayerPrefsManager</c> contains the methods and properties needed for the management of player preferences.
    /// </summary>
    public class PlayerPrefsManager : MonoBehaviour
    {
        /// <value>Property <c>_instance</c> represents the instance of the PlayerPrefsManager.</value>
        private static PlayerPrefsManager _instance;
        
        /// <value>Property <c>_volume</c> represents the value of the master volume.</value>
        private int _volume;
        
        /// <value>Property <c>_textSpeed</c> represents the value of the text speed.</value>
        private int _textSpeed;
        
        /// <value>Property <c>_movementSpeed</c> represents the value of the movement speed.</value>
        private int _movementSpeed;
        
        /// <value>Property <c>volumeSlider</c> represents the UI element containing the volume slider.</value>
        [SerializeField] private Slider volumeSlider;
        
        /// <value>Property <c>textSpeedSlider</c> represents the UI element containing the text speed slider.</value>
        [SerializeField] private Slider textSpeedSlider;
        
        /// <value>Property <c>movementSpeedSlider</c> represents the UI element containing the movement speed slider.</value>
        [SerializeField] private Slider movementSpeedSlider;

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

            // Load player preferences
            LoadPlayerPrefs();
            
            // Save immediately after in case the player preferences did not exist and default values were used
            SavePlayerPrefs();
        }
        
        /// <summary>
        /// Method <c>ChangeVolume</c> changes the master volume.
        /// </summary>
        public void ChangeVolume()
        {
            _volume = (int) volumeSlider.value;
            AudioListener.volume = _volume / 100f;
            PlayerPrefs.SetInt("Volume", _volume);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Method <c>ChangeTextSpeed</c> changes the text speed.
        /// </summary>
        public void ChangeTextSpeed()
        {
            _textSpeed = (int) textSpeedSlider.value;
            PlayerPrefs.SetInt("TextSpeed", _textSpeed);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Method <c>ChangeMovementSpeed</c> changes the movement speed.
        /// </summary>
        public void ChangeMovementSpeed()
        {
            _movementSpeed = (int) movementSpeedSlider.value;
            PlayerPrefs.SetInt("MovementSpeed", _movementSpeed);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Method <c>SavePlayerPrefs</c> saves the player preferences.
        /// </summary>
        public void SavePlayerPrefs()
        {
            PlayerPrefs.SetInt("Volume", _volume);
            PlayerPrefs.SetInt("TextSpeed", _textSpeed);
            PlayerPrefs.SetInt("MovementSpeed", _movementSpeed);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Method <c>LoadPlayerPrefs</c> loads the player preferences.
        /// </summary>
        public void LoadPlayerPrefs()
        {
            _volume = PlayerPrefs.GetInt("Volume", 100);
            _textSpeed = PlayerPrefs.GetInt("TextSpeed", 2);
            _movementSpeed = PlayerPrefs.GetInt("MovementSpeed", 2);
            volumeSlider.value = _volume;
            textSpeedSlider.value = _textSpeed;
            movementSpeedSlider.value = _movementSpeed;
        }

        /// <summary>
        ///  Method <c>OnApplicationQuit</c> is called when playmode is stopped.
        /// </summary>
        private void OnApplicationQuit()
        {
            SavePlayerPrefs();
        }
        
        /// <summary>
        /// Method <c>SavePlayerProgress</c> saves the player's progress.
        /// </summary>
        public static void SavePlayerProgress(string progress)
        {
            PlayerPrefs.SetString("PlayerProgress", progress);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Method <c>LoadPlayerProgress</c> loads the player's progress.
        /// </summary>
        public static string LoadPlayerProgress()
        {
            var progress = PlayerPrefs.GetString("PlayerProgress", "");
            return progress;
        }
    }
}
