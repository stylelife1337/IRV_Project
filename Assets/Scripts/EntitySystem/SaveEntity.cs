using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEntity : MonoBehaviour, ISaveAble
{
    [SerializeField] string instanceID;
    private EntityData entityData = new EntityData();

    void Reset()
    {
        instanceID = gameObject.name + gameObject.GetInstanceID();
    }

    public void LoadState()
    {
        Debug.Log(instanceID + " state loaded");

        entityData = DataManager.Instance.LoadEntities(instanceID);

        if (entityData == null)
            return;

        transform.position = entityData.GetPosition();
        transform.rotation = entityData.GetRotation();

        //CHANGES: Now we loop thru the saved childActiveStatus dictionary count
        for (int i = 0; i < entityData.childActiveStatus.Count; i++)
        {
            //the first index would be this game object active status itself
            if (i == 0)
            {
                transform.gameObject.SetActive(entityData.childActiveStatus[i]);
            }
            else //the rest would be the child, and since GetChild requires value of 0 to retrieve the first child, we are subtracting the index i by 1
            {
                transform.GetChild(i-1).gameObject.SetActive(entityData.childActiveStatus[i]);
            }
        }
    }

    public void SaveState()
    {
        entityData = new EntityData();

        entityData.SetPosition(transform.position);
        entityData.SetRotation(transform.rotation);

        //CHANGES: Save this gameobject active status itself, so we can use this data to enable or disable itself when loading
        entityData.childActiveStatus.Add(0, transform.gameObject.activeInHierarchy);

        //then loop thru the child if there any
        for (int i = 0; i < transform.childCount; i++)
        {
            entityData.childActiveStatus.Add(i+1, transform.GetChild(i).gameObject.activeInHierarchy);
        }

        DataManager.Instance.SaveEntities(instanceID, entityData);
    }

    // Use this for initialization
    void Start ()
    {
        DataManager.Instance.OnSave += SaveState;
        DataManager.Instance.OnLoad += LoadState;
    }

    void OnDestroy()
    {
        DataManager.Instance.OnSave -= SaveState;
        DataManager.Instance.OnLoad -= LoadState;
    }
}
