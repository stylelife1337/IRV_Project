using UnityEngine;

public class EntitiesInitializer : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        DataManager.Instance.InitEntities();
	}
}
