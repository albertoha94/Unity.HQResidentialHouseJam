using TankEngine.Scripts.AbstractClasses;
using TankEngine.Scripts.Interactions;
using UnityEngine;

public class BrokenLightSwitchInteraction : GameManagerBehavior
{

    [SerializeField] InteractionBasic interactionToUse;

    public void AddBrokenLightSwitchInteraction()
    {
        GameManager.AddInteractionToArray(interactionToUse);
    }

}
