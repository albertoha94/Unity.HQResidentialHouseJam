using System.Collections;
using UnityEngine;

namespace TankEngine.Scripts.Interactions
{

    /// <summary>
    /// Used as basic interaction, displays a simple text string.
    /// </summary>
    public class InteractionBasic : Interactable
    {

        public string interactionMessage;
        bool isMessageWritten = false;

        public override IEnumerator PerformInteraction()
        {
            isMessageWritten = false;
            GameManager.StopTime();
            yield return GameManager.uIAssistant.Write(interactionMessage, true, () => isMessageWritten = true);
        }

        public override InteractionTypes GetInteractionType()
        {
            return InteractionTypes.BasicInteraction;
        }

        public override bool IsCompleted()
        {
            return isMessageWritten;
        }

        public override IEnumerator ForceComplete()
        {
            isMessageWritten = false;
            GameManager.uIAssistant.PauseWriting();
            yield return GameManager.uIAssistant.Write(interactionMessage, false, () => isMessageWritten = true);
        }

        public override void OnComplete()
        {
            GameManager.uIAssistant.CleanText();
            GameManager.ResumeTime();
        }
    }
}
