using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPosition : MonoBehaviour
{
    [SerializeField] List<Spawner> spawnEntries = new List<Spawner>();

    private Transform player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Reposition();
	}
	
	void Reposition()
    {
        for (int i = 0; i < spawnEntries.Count; i++)
        {
            if (DataManager.Instance.PrevSceneName == spawnEntries[i].PrevSceneName)
            {
                player.position = spawnEntries[i].SpawnPos;
                player.rotation = Quaternion.LookRotation(spawnEntries[i].SpawnDir);
            }
        }
    }
}

[System.Serializable]
public class Spawner
{
    [SerializeField] string prevSceneName;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] Vector3 spawnDir;

    public string PrevSceneName { get { return prevSceneName; } }
    public Vector3 SpawnPos { get { return spawnPos; } }
    public Vector3 SpawnDir { get { return spawnDir; } }
}
