using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDescriptionUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI descriptionText;

	// Use this for initialization
	void Start ()
    {
        HidePanel();	
	}
	
	void HidePanel()
    {
        gameObject.SetActive(false);
    }

    public void ShowItemDescription(string desc, Vector2 position)
    {
        gameObject.SetActive(true);

        descriptionText.text = desc;

        GetComponent<RectTransform>().anchoredPosition = position;
    }
}
