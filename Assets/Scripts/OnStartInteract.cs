using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartInteract : MonoBehaviour
{
    [SerializeField] Actions[] actions;
    [SerializeField] bool hideMessageOnDisable;

    private void OnEnable()
    {
        Extensions.RunActions(actions);
    }

    private void OnDisable()
    {
        if (hideMessageOnDisable)
            DialogSystem.Instance.HideDialog();
    }
}
