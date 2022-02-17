using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAction : Actions
{
    [SerializeField] string sceneTarget;

    public override void Act()
    {
        DataManager.Instance.SetPrevScene(SceneManager.GetActiveScene().name);

        DataManager.Instance.StoreEntitiesState();

        DataManager.Instance.LevelManager.SceneLoad(sceneTarget);
    }
}
