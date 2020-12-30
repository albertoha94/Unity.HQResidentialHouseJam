using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera InCamera;
    [SerializeField] CinemachineVirtualCamera OutCamera;

    internal void TriggerCamerasIn()
    {
        InCamera.gameObject.SetActive(true);
        OutCamera.gameObject.SetActive(false);
    }

    internal void TriggerCamerasOut()
    {
        OutCamera.gameObject.SetActive(true);
        InCamera.gameObject.SetActive(false);
    }
}
