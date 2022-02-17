using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] LayerMask interactLayer;

    private NavMeshAgent agent;
    private Camera mainCamera;

    private bool turning;
    private Quaternion targetRot;

    private PlayerAnimation playerAnim = new PlayerAnimation();
    private Interactable prevInteractable;

	// Use this for initialization
	void Start ()
    {
        mainCamera = Camera.main;

        agent = GetComponent<NavMeshAgent>();

        playerAnim.Init(GetComponentInChildren<Animator>());

        CameraManager.Instance.OnCameraSwitch += CameraSwitched;
	}

    private void OnDestroy()
    {
        CameraManager.Instance.OnCameraSwitch -= CameraSwitched;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0) && !Extensions.IsMouseOverUI())
            OnClick();

        if (turning && transform.rotation == targetRot)
        {
            turning = false;
        }

        if (turning && transform.rotation != targetRot)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 15f * Time.deltaTime);
        }

        if (!agent.isStopped && CheckIfArrived())
            agent.isStopped = true;

        playerAnim.UpdateAnimation(agent.velocity.sqrMagnitude);
	}

    void OnClick()
    {
        Debug.Log("Left Clicked!");

        RaycastHit hit;
        Ray camToScreen = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camToScreen, out hit, Mathf.Infinity, interactLayer))
        {
            if (hit.collider != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    if (!interactable.LookOnly)
                        MovePlayer(interactable.InteractPosition());

                    interactable.Interact(this);

                    if (prevInteractable != null)
                        prevInteractable.StopAllCoroutines();

                    prevInteractable = interactable;
                }
                else
                {
                    if (prevInteractable != null)
                    {
                        prevInteractable.StopAllCoroutines();
                        prevInteractable = null;
                    }

                    MovePlayer(hit.point);
                }
            }
        }
    }

    public bool CheckIfArrived()
    {
        return (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }

    void MovePlayer(Vector3 targetPosition)
    {
        turning = false;

        agent.isStopped = false;

        agent.SetDestination(targetPosition);

        DialogSystem.Instance.HideDialog();
    }

    public void SetDirection(Vector3 targetDirection)
    {
        turning = true;
        Vector3 vectorDirection = targetDirection - transform.position;
        vectorDirection.y = 0f;
        targetRot = Quaternion.LookRotation(vectorDirection);
    }

    void CameraSwitched(Camera cam)
    {
        mainCamera = cam;
    }
}
