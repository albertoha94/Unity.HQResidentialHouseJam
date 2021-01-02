using System;
using System.Collections;
using TankEngine.Scripts.Interactions.DataClasses;
using UnityEngine;

namespace TankEngine.Scripts.UI
{

    public class UIAssistant : MonoBehaviour
    {

        #region Message Variables.

        [Header("Message Variables")]
        [SerializeField] BasicWriter writer;

        #endregion

        #region Option Variables.

        [Header("Options Variables")]
        [SerializeField] GameObject uIOptionsGameObject;

        #endregion

        #region Note Variables.

        [Header("Note Variables")]
        [SerializeField] GameObject uINoteGameObject;

        #endregion

        #region OptionCanvas functions

        /// <summary>
        /// Writes a message to the uiOptions canvas.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="OnComplete">Action to perform once writing is completed.</param>
        /// <param name="useWriter">Optionally use writer to make the appearance more stylish.</param>
        /// <returns>An IEnumerator.</returns>
        public IEnumerator Write(string message, bool useWriter = true, Action OnComplete = null)
        {
            uIOptionsGameObject.SetActive(true);
            GetUIOptions().SetMessage(message, useWriter && IsWriterSet() ? writer : null, OnComplete);
            yield return null;
        }

        /// <summary>
        /// Adds the options to the uiOptions canvas.
        /// </summary>
        /// <param name="options">Array of options available.</param>
        internal void WriteOptions(OptionData[] options) => GetUIOptions().SetOptions(options);

        /// <summary>
        /// Removes all the elements from the uiOptions canvas.
        /// </summary>
        internal void CleanOptionsCanvas()
        {
            GetUIOptions().CleanText();
            GetUIOptions().CleanOptions();
            uIOptionsGameObject.SetActive(false);
        }

        /// <summary>
        /// Selected the option at a given index.
        /// </summary>
        /// <param name="newIndex">Index of the selected option.</param>
        internal void SelectOption(int newIndex) => GetUIOptions().SelectOption(newIndex);

        /// <summary>
        /// Checks if the writer component is set.
        /// </summary>
        /// <returns></returns>
        private bool IsWriterSet()
        {
            if (writer == null)
            {
                ConsoleLogger.Error("Writer not found!", this);
            }

            return writer != null;
        }

        /// <summary>
        /// Getter for the UIOptions.
        /// </summary>
        /// <returns>A UIOptions Component.</returns>
        private UIOptions GetUIOptions() => uIOptionsGameObject.GetComponent<UIOptions>();

        #endregion

        #region NoteCanvas functions

        /// <summary>
        /// Sets a note to the uiNote canvas.
        /// </summary>
        /// <param name="noteMessage">Message to write.</param>
        /// <param name="backgroundTexture">Background to show.</param>
        /// <param name="OnComplete">Action to perform once completed.</param>
        /// <returns>An IEnumerator.</returns>
        internal IEnumerator SetNote(string noteMessage, Texture backgroundTexture, Action OnComplete = null)
        {
            uINoteGameObject.SetActive(true);
            getUINote().SetNote(noteMessage, backgroundTexture, OnComplete);
            yield return null;
        }

        /// <summary>
        /// Cleans the uiNote canvas.
        /// </summary>
        internal void RemoveNote()
        {
            getUINote().RemoveNote(() => uINoteGameObject.SetActive(false));
        }

        /// <summary>
        /// Getter for the UINote.
        /// </summary>
        /// <returns>A UINote Component.</returns>
        private UINote getUINote() => uINoteGameObject.GetComponent<UINote>();

        #endregion

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
