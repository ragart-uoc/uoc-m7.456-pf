using UnityEngine;
using UnityEngine.UI;

namespace PF.Managers
{
    public class OptionsManager : MonoBehaviour
    {
        /// <value>Property <c>_instance</c> represents the instance of the OptionsManager.</value>
        private static OptionsManager _instance;
        
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
            
            // Get the values from the PlayerPrefs
            _volume = PlayerPrefsManager.GetVolume();
            _textSpeed = PlayerPrefsManager.GetTextSpeed();
            _movementSpeed = PlayerPrefsManager.GetMovementSpeed();
            
            // Set the values of the sliders
            volumeSlider.value = _volume;
            textSpeedSlider.value = _textSpeed;
            movementSpeedSlider.value = _movementSpeed;
            
            // Set the volume
            AudioListener.volume = _volume / 100f;
        }

        /// <summary>
        /// Method <c>ChangeVolume</c> changes the master volume.
        /// </summary>
        public void ChangeVolume()
        {
            _volume = (int) volumeSlider.value;
            AudioListener.volume = _volume / 100f;
            PlayerPrefsManager.ChangeVolume(_volume);
        }
        
        /// <summary>
        /// Method <c>ChangeTextSpeed</c> changes the text speed.
        /// </summary>
        public void ChangeTextSpeed()
        {
            _textSpeed = (int) textSpeedSlider.value;
            PlayerPrefsManager.ChangeTextSpeed(_textSpeed);
        }

        /// <summary>
        /// Method <c>ChangeMovementSpeed</c> changes the movement speed.
        /// </summary>
        public void ChangeMovementSpeed()
        {
            _movementSpeed = (int) movementSpeedSlider.value;
            PlayerPrefsManager.ChangeMovementSpeed(_movementSpeed);
        }

        public void OnApplicationQuit()
        {
            PlayerPrefsManager.SavePlayerPrefs();
        }
    }
}
