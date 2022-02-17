using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadMenu : MonoBehaviour
{
    [SerializeField] SaveEntryUI entryPrefabs;
    [SerializeField] GameObject loadPanel;
    [SerializeField] Transform parent;
    [SerializeField] Button newGame, continueGame;

	// Use this for initialization
	void Start ()
    {
        loadPanel.SetActive(false);
        InitPanel();

        newGame.onClick.AddListener(NewGame);
        continueGame.onClick.AddListener(ShowLoadMenu);
    }
	
	void InitPanel()
    {
        int saveCount = DataManager.Instance.SaveDatas.Count;

        for (int i = 0; i < saveCount; i++)
        {
            SaveEntryUI tempEntry = Instantiate(entryPrefabs, parent);

            string sceneName = DataManager.Instance.SaveDatas[i].currentScene;
            string saveDate = DataManager.Instance.SaveDatas[i].saveDate.ToString("dddd, dd MMMM yyyy HH:mm");

            Texture2D ss;

            DataManager.Instance.LoadThumbnailBasedOnId(i, out ss);

            tempEntry.InitLoadEntry(ss, sceneName, saveDate, i);
        }
    }

    public void NewGame()
    {
        DataManager.Instance.NewGame();
    }

    public void ShowLoadMenu()
    {
        loadPanel.SetActive(true);
    }
}
