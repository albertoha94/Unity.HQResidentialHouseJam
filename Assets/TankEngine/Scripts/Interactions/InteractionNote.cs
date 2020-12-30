using System.Collections;
using TankEngine.Scripts.Interactions;
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
        GameManager.StopTime();
        GameManager.PlaySound(SoundIn);
        yield return GameManager.uIAssistant.SetNote(noteMessage, backgroundTexture, () => isMessageWritten = true);
    }

    public override InteractionTypes GetInteractionType()
    {
        return InteractionTypes.NoteInteraction;
    }

    public override bool IsCompleted()
    {
        return isMessageWritten;
    }

    public override IEnumerator ForceComplete()
    {
        isMessageWritten = false;
        GameManager.uIAssistant.PauseWriting();
        yield return GameManager.uIAssistant.Write(noteMessage, backgroundTexture, () => isMessageWritten = true);
    }

    public override void OnComplete()
    {
        GameManager.uIAssistant.RemoveNote();
        GameManager.ResumeTime();
        Destroy(gameObject);
    }
}
