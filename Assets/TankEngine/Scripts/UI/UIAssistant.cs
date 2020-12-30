using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace TankEngine.Scripts.UI
{

    public class UIAssistant : MonoBehaviour
    {

        #region Message Variables.

        [Header("Message Variables")]
        [SerializeField] TMP_Text mainMessageText;
        [SerializeField] BasicWriter writer;

        #endregion

        #region Note Variables.

        [Header("Note Variables")]
        [SerializeField] GameObject uINoteGameObject;
        [SerializeField] UINote uINote;

        #endregion

        /// <summary>
        /// Writes a message to the mainMessageText gameObject.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="useWriter">Optionally use writer to make the appearance more stylish.</param>
        /// <param name="OnComplete">Action to perform once writing is completed.</param>
        /// <returns></returns>
        public IEnumerator Write(string message, bool useWriter = true, Action OnComplete = null)
        {
            if (useWriter)
            {
                if (writer == null)
                {
                    ConsoleLogger.Error("Writer not found!", this);
                }
                else
                {
                    writer.Write(mainMessageText, message, OnComplete);
                    yield return null;
                }
            }
            else
            {
                mainMessageText.text = message;
                OnComplete?.Invoke();
            }
        }

        /// <summary>
        /// Removes all the text from the mainMessage text object.
        /// </summary>
        internal void CleanText()
        {
            mainMessageText.text = string.Empty;
        }

        internal IEnumerator SetNote(string noteMessage, Texture backgroundTexture, Action OnComplete = null)
        {
            uINoteGameObject.SetActive(true);
            uINote.SetNote(noteMessage, backgroundTexture, OnComplete);
            yield return null;
        }

        internal void RemoveNote()
        {
            uINote.RemoveNote(() => uINoteGameObject.SetActive(false));
        }

        /// <summary>
        /// Disables the writer if its active.
        /// </summary>
        internal void PauseWriting()
        {
            if (writer == null)
            {
                ConsoleLogger.Error("Writer not found!", this);
            }
            else
            {
                if (writer.IsActive())
                {
                    writer.Disable();
                }
            }
        }
    }
}
