using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; private set; }

    [SerializeField] TMPro.TextMeshProUGUI messageText, yesText, noText;
    [SerializeField] GameObject panel;
    [SerializeField] Button yesButton, noButton;

    private List<string> currentMessages = new List<string>();
    private int msgId = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        panel.SetActive(false);	
	}

    public void ShowMessages(List<string> messages, bool dialog, List<Actions> yesActions = null, List<Actions> noActions = null, string yes = "Yes", string no = "No")
    {
        msgId = 0;

        yesButton.transform.parent.gameObject.SetActive(false);

        currentMessages = messages;

        panel.SetActive(true);

        if (dialog)
        {
            yesText.text = yes;
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(delegate
            {
                HideDialog();

                if (yesActions != null)
                    AssignActionstoButtons(yesActions);
            });

            noText.text = no;
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(delegate
            {
                HideDialog();

                if (noActions != null)
                    AssignActionstoButtons(noActions);
            });
        }

        StartCoroutine(ShowMultipleMessages(dialog));
    }

    public void ShowMessages(List<string> messages, List<Actions> chainActions = null)
    {
        Debug.Log("Show messages 2 argument");
        msgId = 0;
        yesButton.transform.parent.gameObject.SetActive(false);
        currentMessages = messages;
        panel.SetActive(true);
        StartCoroutine(ShowMultipleMessages(false, chainActions));
    }

    IEnumerator ShowMultipleMessages(bool useDialog, List<Actions> chainActions = null)
    {
        messageText.text = currentMessages[msgId];

        while(msgId < currentMessages.Count)
        {
            if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0) && Extensions.IsMouseOverUI()))
            {
                msgId++;

                if (msgId < currentMessages.Count)
                    messageText.text = currentMessages[msgId];

                if (!useDialog && msgId == currentMessages.Count)
                {
                    if (chainActions != null)
                        Extensions.RunActions(chainActions.ToArray());
                }
            }

            if (useDialog && msgId == currentMessages.Count - 1)
                yesButton.transform.parent.gameObject.SetActive(true);

            yield return null;
        }

        if (!useDialog)
            HideDialog();
    }

    void AssignActionstoButtons(List<Actions> actions)
    {
        List<Actions> localActions = actions;

        for (int i = 0; i < localActions.Count; i++)
        {
            localActions[i].Act();
        }
    }

    public void HideDialog()
    {
        panel.SetActive(false);
        StopAllCoroutines();
        msgId = 0;
    }
}
