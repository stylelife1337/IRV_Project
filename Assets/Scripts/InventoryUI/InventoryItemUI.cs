using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    private Item thisItem;
    private Button button;
    private TMPro.TextMeshProUGUI amountText;
    private InventorySystemUI invSystem;

    [SerializeField] Image itemImage;

    public void Init(Item item, InventorySystemUI invSystem)
    {
        thisItem = item;
        this.invSystem = invSystem;

        button = GetComponent<Button>();
        amountText = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        itemImage.sprite = thisItem.ItemSprite;

        amountText.gameObject.SetActive(thisItem.AllowMultple);
        amountText.text = "x" + thisItem.Amount;

        button.onClick.AddListener(Tapped);
    }

    void Tapped()
    {
        invSystem.InvDesc.ShowItemDescription(thisItem.ItemDesc, GetComponent<RectTransform>().anchoredPosition);
    }
}
