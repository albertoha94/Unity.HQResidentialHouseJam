using TankEngine.Scripts;
using TankEngine.Scripts.Interactions;
using TankEngine.Scripts.Interactions.Base;
using UnityEngine;

public class InteractionChanger : MonoBehaviour
{

    public InteractableObject target;
    public Interactable[] sequenceToUse;
    [SerializeField] bool areItemsChanged = false;
    Interactable[] originalSequence;

    public void Toggle()
    {
        if (target == null || sequenceToUse == null)
        {
            ConsoleLogger.Error("Missing items to perform the toggle.", this);
            return;
        }

        if (areItemsChanged)
            Revert();
        else
            ChangeSequence();
    }

    internal void ChangeSequence()
    {
        originalSequence = target.InteractionSequence;
        target.InteractionSequence = sequenceToUse;
        areItemsChanged = true;
    }

    internal void Revert()
    {
        target.InteractionSequence = originalSequence;
        originalSequence = null;
        areItemsChanged = false;
    }
}
