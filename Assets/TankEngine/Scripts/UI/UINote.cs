using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankEngine.Scripts.UI
{

    public class UINote : MonoBehaviour
    {

        [SerializeField] TMP_Text noteText;
        [SerializeField] RawImage noteBackground;

        public void SetNote(string note, Texture background, Action OnComplete = null)
        {
            noteBackground.texture = background;
            noteText.text = note;
            OnComplete?.Invoke();
        }

        /// <summary>
        /// Remove the note and disappear the canvas.
        /// </summary>
        internal void RemoveNote(Action OnComplete = null)
        {
            noteText.text = string.Empty;
            OnComplete?.Invoke();
        }
    }
}
