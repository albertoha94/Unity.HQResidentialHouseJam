using TankEngine.Scripts;
using TankEngine.Scripts.AbstractClasses;
using UnityEngine;

public class SafeLightSwitch : GameManagerBehavior
{

    [SerializeField] GameObject lightGO;
    [SerializeField] AudioClip switchClip;

    public void ToggleLight()
    {
        var toggleState = !lightGO.activeSelf;
        lightGO.SetActive(toggleState);

        GameManager.PlaySound(switchClip);
    }
}
