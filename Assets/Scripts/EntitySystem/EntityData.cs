using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityData
{
    public float x, y, z;
    public float qw, qx, qy, qz;
    public Dictionary<int, bool> childActiveStatus = new Dictionary<int, bool>();

	public void SetPosition(Vector3 position)
    {
        x = position.x;
        y = position.y;
        z = position.z;
    }

    public void SetRotation(Quaternion rotation)
    {
        qw = rotation.w;
        qx = rotation.x;
        qy = rotation.y;
        qz = rotation.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }

    public Quaternion GetRotation()
    {
        return new Quaternion(qx, qy, qz, qw);
    }
}
