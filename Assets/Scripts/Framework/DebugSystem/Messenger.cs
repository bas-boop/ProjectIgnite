using UnityEngine;

namespace Framework.DebugSystem
{
    public class Messenger : MonoBehaviour
    {
        /// <summary>
        /// Logs the given message in the console.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void DebugLog(string message) => Debug.Log(message);
        
        /// <summary>
        /// Logs the given message in the console as a warning.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void DebugLogWarning(string message) => Debug.LogWarning(message);
        
        /// <summary>
        /// Logs the given message in the console as an error.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void DebugLogError(string message) => Debug.LogError(message);
    }
}