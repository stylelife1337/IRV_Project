using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    Inventory source;
    SerializedProperty s_inventory, s_itemDatabase;
    int itemId;

    private void OnEnable()
    {
        source = (Inventory)target;

        s_inventory = serializedObject.FindProperty("inventory");
        s_itemDatabase = serializedObject.FindProperty("itemDatabase");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(s_itemDatabase);

        if (source.ItemDatabase != null)
        {
            itemId = EditorGUILayout.Popup(itemId, source.ItemDatabase.ItemsNames.ToArray());

            if (GUILayout.Button("Add Item"))
            {
                Item newItem = Extensions.CopyItem(source.ItemDatabase.GetItem(itemId));
                source.AddItem(newItem);
            }

            for (int i = 0; i < s_inventory.arraySize; i++)
            {
                //draw every item entry
                DrawItemEntry(s_inventory.GetArrayElementAtIndex(i), i);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    void DrawItemEntry(SerializedProperty item, int id)
    {
        GUILayout.BeginVertical("box");

        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Item Id:" + item.FindPropertyRelative("itemId").intValue, GUILayout.Width(75f));
        EditorGUILayout.LabelField("Item Name: " + item.FindPropertyRelative("itemName").stringValue);

        if (GUILayout.Button("X", GUILayout.Width(20f)))
        {
            //delete the item
            s_inventory.DeleteArrayElementAtIndex(id);

            return;
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Item Description" + item.FindPropertyRelative("itemDescription").stringValue, GUILayout.Height(70f));

        GUILayout.BeginHorizontal();

        var spriteViewer = AssetPreview.GetAssetPreview(item.FindPropertyRelative("itemSprite").objectReferenceValue);
        GUILayout.Label(spriteViewer);

        if (item.FindPropertyRelative("allowMultiple").boolValue)
            EditorGUILayout.PropertyField(item.FindPropertyRelative("amount"));

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }
}
