using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipData
{
    public int oreCollected;

    public float[] shipLocation;
    public float[] shipRotation;

    public ShipData(Transform ship)
    {
        oreCollected = 0;

        shipLocation = new float[3];
        shipLocation[0] = ship.position.x;
        shipLocation[1] = ship.position.y;
        shipLocation[2] = ship.position.z;

        shipRotation = new float[4];
        shipRotation[0] = ship.rotation.x;
        shipRotation[1] = ship.rotation.y;
        shipRotation[2] = ship.rotation.z;
        shipRotation[3] = ship.rotation.w;
    }

    public Vector3 GetLocationData()
    {
        return new Vector3(shipLocation[0], shipLocation[1], shipLocation[2]);
    }

    public Quaternion GetRotationData()
    {
        return new Quaternion(shipRotation[0], shipRotation[1], shipRotation[2], shipRotation[3]);
    }
}
