using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int oreCollected;
    public Vector3 playerLocation;

    public GameData()
    {
        oreCollected = 0;
        playerLocation = new Vector3(0, 0, 0);
    }
}
