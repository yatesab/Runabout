using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipData
{
    public Vector3 Position
    {
        get
        {
            return new Vector3(shipLocation[0], shipLocation[1], shipLocation[2]);
        }
        set
        {
            shipLocation = new float[3];
            shipLocation[0] = value.x;
            shipLocation[1] = value.y;
            shipLocation[2] = value.z;
        }
    }
    public float[] shipLocation;

    public Quaternion Rotation
    {
        get
        {
            return new Quaternion(shipRotation[0], shipRotation[1], shipRotation[2], shipRotation[3]);
        }
        set
        {
            shipRotation = new float[4];
            shipRotation[0] = value.x;
            shipRotation[1] = value.y;
            shipRotation[2] = value.z;
            shipRotation[3] = value.w;
        }
    }
    public float[] shipRotation;

    public ShipData(Vector3 position, Quaternion rotation)
    {
        shipLocation = new float[3];
        shipLocation[0] = position.x;
        shipLocation[1] = position.y;
        shipLocation[2] = position.z;

        shipRotation = new float[4];
        shipRotation[0] = rotation.x;
        shipRotation[1] = rotation.y;
        shipRotation[2] = rotation.z;
        shipRotation[3] = rotation.w;
    }
}
