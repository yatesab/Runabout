using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int oreCollected;
    public float[] playerLocation;
    public float[] shipLocation;

    public GameData(Vector3 player, Vector3 ship)
    {
        oreCollected = 0;

        playerLocation = new float[3];
        playerLocation[0] = player.x;
        playerLocation[1] = player.y;
        playerLocation[2] = player.z;

        shipLocation = new float[3];
        shipLocation[0] = ship.x;
        shipLocation[1] = ship.y;
        shipLocation[2] = ship.z;
    }
}
