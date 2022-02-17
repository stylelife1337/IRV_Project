using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageAction : Actions
{
    [Multiline(5)]
    [SerializeField] List<string> message;
    [SerializeField] bool enableDialog;
    [SerializeField] string yesText, noText;
    [SerializeField] List<Actions> chainActions, yesActions, noActions;

    public override void Act()
    {
        //Debug.Log(message);
        if (enableDialog)
            DialogSystem.Instance.ShowMessages(message, enableDialog, yesActions, noActions, yesText, noText);
        else
            DialogSystem.Instance.ShowMessages(message, chainActions);
    }
}
