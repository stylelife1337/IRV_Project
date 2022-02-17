using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    [SerializeField] Transform cursor;
    [SerializeField] Image cursorImage;
    [SerializeField] LayerMask layerMask;

    private Camera cam;
    private RaycastHit hit;
    private Ray ray;
    private Interactable currentInteract;

	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;

        CameraManager.Instance.OnCameraSwitch += CameraSwitch;
	}

    // Update is called once per frame
    void Update()
    {
        cursor.position = Input.mousePosition;

        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            cursor.gameObject.SetActive(true);

            if (hit.collider != null && currentInteract == null)
            {
                currentInteract = hit.collider.GetComponent<Interactable>();

                //we want to change the cursor sprite
                if (currentInteract.SpriteCursor == null)
                    cursor.gameObject.SetActive(false);

                cursorImage.sprite = currentInteract.SpriteCursor;
            }
        }
        else
        {
            cursor.gameObject.SetActive(false);
            currentInteract = null;
        }
	}

    void CameraSwitch(Camera cam)
    {
        this.cam = cam;
    }
}
