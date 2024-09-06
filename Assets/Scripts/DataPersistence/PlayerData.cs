using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] playerLocation;
    public float[] playerRotation;

    public PlayerData(Transform player)
    {
        playerLocation = new float[3];
        playerLocation[0] = player.position.x;
        playerLocation[1] = player.position.y;
        playerLocation[2] = player.position.z;

        playerRotation = new float[4];
        playerRotation[0] = player.rotation.x;
        playerRotation[1] = player.rotation.y;
        playerRotation[2] = player.rotation.z;
        playerRotation[3] = player.rotation.w;
    }

    public Vector3 GetLocationData()
    {
        return new Vector3(playerLocation[0], playerLocation[1], playerLocation[2]);
    }

    public Quaternion GetRotationData()
    {
        return new Quaternion(playerRotation[0], playerRotation[1], playerRotation[2], playerRotation[3]);
    }
}
