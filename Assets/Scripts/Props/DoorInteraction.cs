using TankEngine.Scripts.Interactions;
using UnityEngine.SceneManagement;

public class DoorInteraction : InteractionBasic
{
    public override void OnComplete()
    {
        base.OnComplete();
        SceneManager.LoadScene(2);
    }
}
