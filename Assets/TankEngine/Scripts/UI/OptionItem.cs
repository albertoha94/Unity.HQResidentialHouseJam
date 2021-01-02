using System;
using TankEngine.Scripts.Interactions.DataClasses;
using TMPro;
using UnityEngine;

namespace TankEngine.Scripts.UI
{

    public class OptionItem : MonoBehaviour
    {

        [SerializeField] TMP_Text selected;
        [SerializeField] TMP_Text optionText;
        OptionData data = null;

        /// <summary>
        /// Sets the data to the script.
        /// </summary>
        /// <param name="option">OptionData holder.</param>
        internal void SetOption(OptionData option)
        {
            data = option;
            optionText.text = data.conditionText;
            ToggleSelected(data.isSelected);
        }

        /// <summary>
        /// Toggles the UI component with its selected state.
        /// </summary>
        /// <param name="isSelected">Is this item selected flag.</param>
        internal void ToggleSelected(bool isSelected)
        {
            data.isSelected = isSelected;
            selected.gameObject.SetActive(isSelected);
        }

        /// <summary>
        /// Flag for checking if this element is selected.
        /// </summary>
        /// <returns>A boolean.</returns>
        internal bool IsItemSelected() => data != null && data.isSelected == true;
    }
}
