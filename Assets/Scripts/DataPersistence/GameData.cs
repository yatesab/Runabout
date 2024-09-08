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
    }
}
