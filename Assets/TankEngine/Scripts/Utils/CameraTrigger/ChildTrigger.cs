using TankEngine.Scripts;
using UnityEngine;

public class ChildTrigger : MonoBehaviour
{

    [SerializeField] bool isInTrigger;
    CameraHandler cameraHandler;

    // Start is called before the first frame update
    void Start()
    {
        if (cameraHandler == null)
        {
            cameraHandler = transform.parent.GetComponent<CameraHandler>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != Tags.PLAYER)
            return;

        if (isInTrigger)
            cameraHandler.TriggerCamerasIn();
        else
            cameraHandler.TriggerCamerasOut();
    }
}
