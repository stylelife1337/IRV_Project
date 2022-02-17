using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public Inventory Inventory { get { return inventory; } }

    [SerializeField] Inventory inventory;

    public event System.Action OnSave = delegate { };
    public event System.Action OnLoad = delegate { };

    public string PrevSceneName { get; private set; }
    public LevelManager LevelManager { get; private set; }

    private int saveDataId = 0;
    private List<SaveData> saveDatas = new List<SaveData>();

    public List<SaveData> SaveDatas { get { return saveDatas; } }

    private ScreenshotSaver ssSaver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LevelManager = GetComponentInChildren<LevelManager>();
        ssSaver = GetComponent<ScreenshotSaver>();
    }

    public bool HasSaveData()
    {
        if (saveDatas != null)
        {
            return (saveDatas.Count > 0);
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        if (!HasSaveData())
        {
            saveDatas.Add(new SaveData());
            saveDataId = saveDatas.Count - 1;
        }

        Load();
    }

    public void Save()
    {
        List<string> saveMessage = new List<string>();
        saveMessage.Add("Saved Successfully");

        DialogSystem.Instance.ShowMessages(saveMessage, false);
        SaveSystem.Save(saveDatas);
    }

    public void Load()
    {
        if (SaveSystem.CheckForSave())
            saveDatas = SaveSystem.Load<List<SaveData>>();
    }

    public void SaveDataEntry()
    {
        //if (saveDatas.Count == 0)
        //    saveDatas.Add(new SaveData());

        OnSave();
        saveDatas[saveDataId].currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        saveDatas[saveDataId].saveDate = System.DateTime.Now;
        //Save Inventory
        SaveInventory();
        StartCoroutine(SaveScreenshot());
    }

    IEnumerator SaveScreenshot()
    {
        ssSaver.GetSnapShot();

        yield return ssSaver.screenCaptureWait;

        saveDatas[saveDataId].thumbnail = ssSaver.GetThumbnail();

        Save();
    }

    public void LoadDataEntry(int id)
    {
        saveDataId = id;
        //load inventory
        LoadInventory();
        OnLoad();
        LevelManager.SceneLoad(saveDatas[saveDataId].currentScene);
    }

    public void LoadThumbnailBasedOnId(int dataId, out Texture2D ss)
    {
        ss = ssSaver.ArrayToTexture(saveDatas[dataId].thumbnail);
    }

    public void SetPrevScene(string name)
    {
        PrevSceneName = name;
    }

    public void NewGame()
    {
        saveDatas.Add(new SaveData());
        saveDataId = saveDatas.Count - 1;
        LevelManager.SceneLoad("Office");
    }

    public void SaveEntities(string id, EntityData data)
    {
        if (saveDatas[saveDataId].entitiesData.ContainsKey(id))
        {
            saveDatas[saveDataId].entitiesData[id] = data;

            return;
        }
        else
        {
            saveDatas[saveDataId].entitiesData.Add(id, data);
        }
    }

    public EntityData LoadEntities(string id)
    {
        if (saveDatas[saveDataId].entitiesData.ContainsKey(id))
            return saveDatas[saveDataId].entitiesData[id];
        else
            return null;
    }

    public void InitEntities()
    {
        OnLoad();
    }

    public void StoreEntitiesState()
    {
        OnSave();
    }

    public void SaveInventory()
    {
        saveDatas[saveDataId].inventoryItemsId.SaveItemsToId(inventory.GetInventory);
    }

    public void LoadInventory()
    {
        inventory.UpdateInventory(saveDatas[saveDataId].inventoryItemsId);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnLoad();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            OnSave();
        }
    }
}

[System.Serializable]
public class SaveData
{
    public CustomColor[] thumbnail;
    public string currentScene;
    public System.DateTime saveDate;
    public Dictionary<string, EntityData> entitiesData = new Dictionary<string, EntityData>();
    public List<int> inventoryItemsId = new List<int>();
}

[System.Serializable]
public struct CustomColor
{
    public float r, g, b;

    public Color GetColor()
    {
        return new Color(r, g, b, 1f);
    }
}
