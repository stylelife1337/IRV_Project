using UnityEngine;

public class SwitchCameraAction : Actions
{
    [SerializeField] Camera cameraToSwitch;

    public override void Act()
    {
        CameraManager.Instance.SwitchCamera(cameraToSwitch);
    }
}
