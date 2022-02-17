using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;

public static class Extensions
{
	public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public static Item CopyItem(Item item)
    {
        Item newItem = new Item(item.ItemId, item.ItemName, item.ItemDesc, item.ItemSprite, item.AllowMultple);

        return newItem;
    }

    public static void RunActions(Actions[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act();
        }
    }

    public static List<T> FindObjectsOfTypeAll<T>()
    {
        List<T> result = new List<T>();
        SceneManager.GetActiveScene().GetRootGameObjects().ToList().ForEach(g => result.AddRange(g.GetComponentsInChildren<T>()));

        return result;
    }

    public static void SaveItemsToId(this List<int> itemsId, List<Item> inventory)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (itemsId.Contains(inventory[i].ItemId))
                return;

            itemsId.Add(inventory[i].ItemId);
        }
    }

    public static void LoadIdToItems(this List<Item> inventory, ItemDatabase itemDatabase, List<int> itemsId)
    {
        for (int i = 0; i < itemsId.Count; i++)
        {
            Item item = CopyItem(itemDatabase.GetItem(itemsId[i]));
            inventory.Add(item);
        }
    }
}
