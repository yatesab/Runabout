using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    [SerializeField] private AutomaticDoor _automaticDoor;

    [SerializeField] private SceneGroup[] scenesToLoad;

    private SceneGroup currentSceneSelection;

    public void StartNewGame()
    {
        GameSceneManager.instance.StartNewGame();
    }

    public void LoadNewScene(string sceneGroup)
    {
        GetNewSceneGroup(sceneGroup);

        if (currentSceneSelection != null)
        {
            PlayerConditionManager.instance.LoadPlayerData();
            //GameSceneManager.instance.StartTransport(currentSceneSelection);
        } else
        {
            Debug.LogError("No Scene with that name to load");
        }
    }

    private void GetNewSceneGroup(string sceneGroupName)
    {
        for(int sceneGroupIndex = 0;  sceneGroupIndex < scenesToLoad.Length; sceneGroupIndex++)
        {
            if (scenesToLoad[sceneGroupIndex].groupName == sceneGroupName)
            {
                currentSceneSelection = scenesToLoad[sceneGroupIndex];
            }
        }
    }
}
