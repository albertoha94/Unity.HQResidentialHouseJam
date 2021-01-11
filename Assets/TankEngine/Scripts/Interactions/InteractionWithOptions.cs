using System.Linq;
using System.Collections;
using TankEngine.Scripts.Interactions.Base;
using TankEngine.Scripts.Interactions.DataClasses;
using UnityEngine;

namespace TankEngine.Scripts.Interactions
{

    /// <summary>
    /// Used to display interaction, display a text and its options.
    /// </summary>
    public class InteractionWithOptions : Interactable
    {

        public string interactionMessage;
        public bool destroyItemOnFinished = false;
        public bool AreOptionsSet = false;
        [SerializeField] bool isMessageWritten = false;
        [SerializeField] bool isOptionSelectedDone = false;
        public OptionData[] options;

        public override IEnumerator PerformInteraction()
        {
            AreOptionsSet = false;
            isMessageWritten = false;
            isOptionSelectedDone = false;
            yield return WriteUISingle(true);
        }

        public override InteractionTypes GetInteractionType()
        {
            return InteractionTypes.Options;
        }

        public override bool IsCompleted()
        {
            return isOptionSelectedDone;
        }

        public override IEnumerator ForceComplete()
        {
            if (isMessageWritten)
            {
                if (AreOptionsSet)
                    yield return null;

                AreOptionsSet = true;
                GameManager.uIAssistant.WriteOptions(options);
            }
            else
            {
                GameManager.uIAssistant.PauseWriting();
                isMessageWritten = false;
                yield return WriteUISingle(false);
            }
        }

        public override void OnComplete()
        {
            GameManager.uIAssistant.CleanOptionsCanvas();

            if (destroyItemOnFinished)
            {
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Used to move between the options available.
        /// </summary>
        /// <param name="movementLeft">Is it moving left flag.</param>
        /// <param name="movementRight">Is it moving right flag.</param>
        internal void MoveOption(bool movementLeft, bool movementRight)
        {
            var originalIndex = options.Select((option, index) => new { option, index }).First((item) => item.option.isSelected == true).index;
            var newIndex = originalIndex;
            if (movementLeft) newIndex--;
            if (movementRight) newIndex++;

            if (originalIndex != newIndex && newIndex >= 0)
                GameManager.uIAssistant.SelectOption(newIndex);
        }

        /// <summary>
        /// Perform the events of the selected option.
        /// </summary>
        /// <returns>An IEnumerator.</returns>
        internal IEnumerator PerformSelectedOption()
        {
            var selectedOption = options.Where((item) => item.isSelected == true).First();
            selectedOption.eventToUse?.Invoke();
            isOptionSelectedDone = true;
            yield return null;
        }

        /// <summary>
        /// Calls the assistant to write a single line to the UI.
        /// </summary>
        /// <param name="useWriter">Flag to use the writer.</param>
        /// <returns>An IEnumerator.</returns>
        private IEnumerator WriteUISingle(bool useWriter)
        {
            yield return GameManager.uIAssistant.Write(interactionMessage, useWriter, () =>
            {
                isMessageWritten = true;
                StartCoroutine(ForceComplete());
            });
        }
    }
}
