using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager instance { get; private set; }
    public GameData gameData;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Save Manager");
        }
        instance = this;
    }

    public void StartNewGame()
    {
    }

    public void SaveGameData()
    {
        // Save to a JSON file
    }

    public void LoadGameData()
    {
        // Pull from JSON file
    }
}
