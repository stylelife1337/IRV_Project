using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Custom Data/Inventory Data")]
public class Inventory : ScriptableObject
{
    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] List<Item> inventory = new List<Item>();

    public event System.Action<List<Item>> OnItemChange = delegate { };

    public List<Item> GetInventory { get { return inventory; } }

    public void AddItem(Item item)
    {
        inventory.Add(item);
        OnItemChange(inventory);
    }

    public ItemDatabase ItemDatabase { get { return itemDatabase; } }

    public int CheckAmount(Item item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemId == item.ItemId)
            {
                if (inventory[i].AllowMultple)
                {
                    return inventory[i].Amount;
                }
                else
                {
                    return 1;
                }
            }
        }
        return 0;
    }

    public void ModifyItemAmount(Item item, int amount)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemId == item.ItemId)
            {
                if (inventory[i].AllowMultple)
                {
                    inventory[i].ModifyAmount(-amount);

                    if (inventory[i].Amount <= 0)
                        inventory.RemoveAt(i);
                }
                else
                {
                    inventory.RemoveAt(i);
                }

                OnItemChange(inventory);
                return;
            }
        }

        Item newItem = Extensions.CopyItem(item);
        newItem.ModifyAmount(amount);

        AddItem(newItem);
        OnItemChange(inventory);
    }

    public void UpdateInventory(List<int> itemsId)
    {
        inventory.Clear();
        inventory.LoadIdToItems(itemDatabase, itemsId);
        OnItemChange(inventory);
    }
}
