using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public event System.Action<Camera> OnCameraSwitch = delegate { };

    private List<Camera> allCamerasOnScene = new List<Camera>();

    public Camera currentCamera { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        //need to grab all the camera on scene;
        allCamerasOnScene = Extensions.FindObjectsOfTypeAll<Camera>();   
        currentCamera = Camera.main;
	}
	
	public void SwitchCamera(Camera cam)
    {
        for (int i = 0; i < allCamerasOnScene.Count; i++)
        {
            allCamerasOnScene[i].gameObject.SetActive(false);
        }

        cam.gameObject.SetActive(true);
        currentCamera = cam;

        OnCameraSwitch(cam);
    }
}
