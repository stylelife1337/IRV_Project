using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActions : Actions
{
    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] bool giveItem; //this will decide wether we are giving or receiving the item
    [SerializeField] int amount;
    [SerializeField] Actions[] yesActions, noActions;

    public int itemId;

    [SerializeField] Item currentItem;

    public Item CurrentItem { get { return currentItem; } }

    public ItemDatabase ItemDatabase { get { return itemDatabase; } }

    public void ChangeItem(Item item)
    {
        if (CurrentItem.ItemId == item.ItemId)
            return;

        if (itemDatabase != null)
            currentItem = Extensions.CopyItem(item);
    }

    public override void Act()
    {
        //check if giveItem is true, then give the item
        if (giveItem)
        {
            int itemOwned = DataManager.Instance.Inventory.CheckAmount(CurrentItem);

            //check if we own the item
            if (itemOwned > 0)
            {
                if (CurrentItem.AllowMultple && amount <= itemOwned)
                {
                    //pass the item, invoke yesActions
                    DataManager.Instance.Inventory.ModifyItemAmount(CurrentItem, amount);

                    Extensions.RunActions(yesActions);
                }
                else if (!CurrentItem.AllowMultple && itemOwned == 1)
                {
                    //remove the item from inventory, and then invoke yesActions
                    DataManager.Instance.Inventory.ModifyItemAmount(CurrentItem, -itemOwned);

                    Extensions.RunActions(yesActions);
                }
                else
                {
                    //don't have the item
                    Extensions.RunActions(noActions);
                }
            }
            else
            {
                //don't have the item
                Extensions.RunActions(noActions);
            }
        }
        else
        {
            //else receive the item
            if (CurrentItem.AllowMultple)
            {
                DataManager.Instance.Inventory.ModifyItemAmount(CurrentItem, amount);
                Extensions.RunActions(yesActions);
            }
            else if (!CurrentItem.AllowMultple)
            {
                if (DataManager.Instance.Inventory.CheckAmount(CurrentItem) == 1)
                {
                    //already have
                    Extensions.RunActions(noActions);
                }
                else
                {
                    DataManager.Instance.Inventory.ModifyItemAmount(CurrentItem, 1);
                    Extensions.RunActions(yesActions);
                }
            }
        }
    }   
}
