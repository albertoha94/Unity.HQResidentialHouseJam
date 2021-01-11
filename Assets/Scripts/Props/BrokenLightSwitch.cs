using TankEngine.Scripts.AbstractClasses;
using TankEngine.Scripts.Interactions;
using UnityEngine;

public class BrokenLightSwitch : GameManagerBehavior
{

    [SerializeField] InteractionBasic interactionToUse;

    public void AddBrokenLightSwitchInteraction()
    {
        GameManager.AddInteractionToArray(interactionToUse);
    }

}
