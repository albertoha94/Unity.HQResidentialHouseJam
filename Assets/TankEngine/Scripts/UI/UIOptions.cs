using System;
using System.Linq;
using TankEngine.Scripts.Interactions.DataClasses;
using TMPro;
using UnityEngine;

namespace TankEngine.Scripts.UI
{

    public class UIOptions : MonoBehaviour
    {

        [SerializeField] TMP_Text optionText;
        [SerializeField] GameObject optionsLocation;
        [SerializeField] GameObject optionPrefab;

        /// <summary>
        /// Sets the message to show in the UI.
        /// </summary>
        /// <param name="message">String to write.</param>
        /// <param name="writer">The writer to use if any.</param>
        /// <param name="onComplete">Action to perform once completed.</param>
        internal void SetMessage(string message, BasicWriter writer, Action onComplete)
        {
            if (writer == null)
            {
                optionText.text = message;
                onComplete?.Invoke();
            }
            else
            {
                writer.Write(optionText, message, onComplete);
            }
        }

        /// <summary>
        /// Removes the text from the UI.
        /// </summary>
        internal void CleanText()
        {
            optionText.text = string.Empty;
        }

        /// <summary>
        /// Removes the children in the options menu UI.
        /// </summary>
        internal void CleanOptions()
        {
            if (optionsLocation.transform.childCount > 0)
            {
                foreach (Transform child in optionsLocation.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        /// <summary>
        /// Sets the optons to show in the UI.
        /// </summary>
        /// <param name="options">Option data objects.</param>
        internal void SetOptions(OptionData[] options)
        {
            foreach (OptionData option in options)
            {
                var instance = Instantiate(optionPrefab, optionsLocation.transform);
                instance.GetComponent<OptionItem>().SetOption(option);
            }

            SelectOption(0);
        }

        /// <summary>
        /// Toggles the required option as selected.
        /// </summary>
        /// <param name="newIndex">The index of the option to select.</param>
        internal void SelectOption(int newIndex)
        {
            var optionsAvailable = optionsLocation.GetComponentsInChildren<OptionItem>();
            if (optionsAvailable == null)
                return;

            var previousItem = optionsAvailable.Where((OptionItem item) => item.IsItemSelected() == true).First();
            var newItem = optionsAvailable.ElementAt(newIndex);

            previousItem.ToggleSelected(false);
            newItem.ToggleSelected(true);
        }
    }
}
