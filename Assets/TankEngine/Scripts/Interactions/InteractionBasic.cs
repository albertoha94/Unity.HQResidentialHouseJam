using System.Collections;
using TankEngine.Scripts.Interactions.Base;

namespace TankEngine.Scripts.Interactions
{

    /// <summary>
    /// Used as basic interaction, displays a series of text strings.
    /// </summary>
    public class InteractionBasic : Interactable
    {

        public string[] interactionMessages;
        bool isCurrentMessageWritten;
        int messageIndex;

        public override IEnumerator PerformInteraction()
        {
            messageIndex = 0;
            isCurrentMessageWritten = false;
            yield return WriteUISingle(true);
        }

        public override InteractionTypes GetInteractionType()
        {
            return InteractionTypes.Basic;
        }

        public override bool IsCompleted()
        {
            return isCurrentMessageWritten && messageIndex == interactionMessages.Length - 1;
        }

        public override IEnumerator ForceComplete()
        {
            if (isCurrentMessageWritten)
            {
                messageIndex++;
                isCurrentMessageWritten = false;
                yield return WriteUISingle(true);
            }
            else
            {
                GameManager.uIAssistant.PauseWriting();
                isCurrentMessageWritten = false;
                yield return WriteUISingle(false);
            }
        }

        public override void OnComplete()
        {
            GameManager.uIAssistant.CleanOptionsCanvas();
        }

        /// <summary>
        /// Calls the assistant to write a single line to the UI.
        /// </summary>
        /// <param name="useWriter">Flag to use the writer.</param>
        /// <returns>An IEnumerator.</returns>
        private IEnumerator WriteUISingle(bool useWriter)
        {
            var currentMessage = interactionMessages[messageIndex];
            yield return GameManager.uIAssistant.Write(currentMessage, useWriter, () => isCurrentMessageWritten = true);
        }
    }
}
