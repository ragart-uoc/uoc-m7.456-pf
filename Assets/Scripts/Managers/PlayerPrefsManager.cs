using UnityEngine;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>PlayerPrefsManager</c> contains the methods and properties needed for the management of player preferences.
    /// </summary>
    public static class PlayerPrefsManager
    {
        
        /// <summary>
        /// Method <c>ChangeVolume</c> changes the master volume.
        /// </summary>
        public static void ChangeVolume(int volume)
        {
            PlayerPrefs.SetInt("Volume", volume);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Method <c>GetVolume</c> gets the master volume.
        /// </summary>
        public static int GetVolume()
        {
            return PlayerPrefs.GetInt("Volume", 100);
        }
        
        /// <summary>
        /// Method <c>ChangeTextSpeed</c> changes the text speed.
        /// </summary>
        public static void ChangeTextSpeed(int textSpeed)
        {
            PlayerPrefs.SetInt("TextSpeed", textSpeed);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Method <c>GetTextSpeed</c> gets the text speed.
        /// </summary>
        public static int GetTextSpeed()
        {
            return PlayerPrefs.GetInt("TextSpeed", 1);
        }

        /// <summary>
        /// Method <c>ChangeMovementSpeed</c> changes the movement speed.
        /// </summary>
        public static void ChangeMovementSpeed(int movementSpeed)
        {
            PlayerPrefs.SetInt("MovementSpeed", movementSpeed);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Method <c>GetMovementSpeed</c> gets the movement speed.
        /// </summary>
        public static int GetMovementSpeed()
        {
            return PlayerPrefs.GetInt("MovementSpeed", 1);
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
        
        /// <summary>
        /// Method <c>SavePlayerPrefs</c> saves the player preferences.
        /// </summary>
        public static void SavePlayerPrefs()
        {
            PlayerPrefs.Save();
        }
    }
}
