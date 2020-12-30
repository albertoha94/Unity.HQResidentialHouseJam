using UnityEngine;

namespace TankEngine.Scripts
{

    public static class ConsoleLogger
    {

        /// <summary>
        /// Logs an errorto Unitys console.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="classCallingIt">Origin of the call.</param>
        internal static void Error(string message, MonoBehaviour classCallingIt)
        {
            Debug.LogError($"{classCallingIt.name}:{message}");
        }
    }
}