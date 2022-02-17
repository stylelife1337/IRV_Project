using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] int itemId;
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    [SerializeField] bool allowMultiple;
    [SerializeField] int amount;

    public Item(int itemId, string name, string desc, Sprite sprite, bool allowMultiple)
    {
        this.itemId = itemId;
        itemName = name;
        itemDescription = desc;
        itemSprite = sprite;
        this.allowMultiple = allowMultiple;
    }

    public int ItemId { get { return itemId; } }
    public string ItemName { get { return itemName; } }
    public string ItemDesc {  get { return itemDescription; } }
    public Sprite ItemSprite { get { return itemSprite; } }
    public bool AllowMultple { get { return allowMultiple; } }
    public int Amount { get { return amount; } }

    public void ModifyAmount(int value)
    {
        amount += value;
    }
}
