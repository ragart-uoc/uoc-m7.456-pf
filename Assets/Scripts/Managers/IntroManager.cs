using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace PF.Managers
{
    /// <summary>
    /// Class <c>IntroManager</c> contains the methods and properties needed for the intro sequence.
    /// </summary>
    public class IntroManager : MonoBehaviour
    {
        /// <value>Property <c>nextScene</c> represents the scene that will be loaded next.</value>
        public SceneAsset nextScene;
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            SceneManager.LoadScene(nextScene.name);
        }
    }
}
