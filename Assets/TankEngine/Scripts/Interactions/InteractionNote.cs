using System;
using System.Collections;
using TankEngine.Scripts.Interactions.Base;
using UnityEngine;

public class InteractionNote : Interactable
{

    public Texture backgroundTexture;
    [TextArea(10, 15)]
    public string noteMessage;
    bool isMessageWritten = false;

    public override IEnumerator PerformInteraction()
    {
        isMessageWritten = false;
        yield return GameManager.uIAssistant.SetNote(noteMessage, backgroundTexture, () => isMessageWritten = true);
    }

    public override InteractionTypes GetInteractionType()
    {
        return InteractionTypes.Note;
    }

    public override bool IsCompleted()
    {
        return isMessageWritten;
    }

    public override IEnumerator ForceComplete()
    {
        throw new NotImplementedException();
    }

    public override void OnComplete()
    {
        GameManager.uIAssistant.RemoveNote();
        Destroy(gameObject);
    }
}
