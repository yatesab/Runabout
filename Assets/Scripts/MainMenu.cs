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
    private List<AsyncOperation> sceneLoadOperations;

    public void Awake()
    {
        currentSceneSelection = scenesToLoad[1];
        sceneLoadOperations = new List<AsyncOperation>();
    }

    public void LoadNewScene(string sceneGroup)
    {

        if(sceneGroup != currentSceneSelection.groupName)
        {
            StartCoroutine(LoadNewSceneGroupAsync(sceneGroup));
        }
    }

    private void ForceOpenDoor()
    {
        _automaticDoor.UpdateForceClose(false);
        _automaticDoor.OpenDoor();
    }

    private void ForceDoorClosed()
    {
        _automaticDoor.UpdateForceClose(true);
        _automaticDoor.CloseDoor();
    }

    private void SetNewSceneGroup(string sceneGroupName)
    {
        for(int sceneGroupIndex = 0;  sceneGroupIndex < scenesToLoad.Length; sceneGroupIndex++)
        {
            if (scenesToLoad[sceneGroupIndex].groupName == sceneGroupName)
            {
                currentSceneSelection = scenesToLoad[sceneGroupIndex];
                sceneLoadOperations = new List<AsyncOperation>();
            }
        }
    }

    private IEnumerator LoadNewSceneGroupAsync(string groupName)
    {
        ForceDoorClosed();

        while (_automaticDoor.Open)
        {
            yield return null;
        }

        currentSceneSelection.UnloadScenes();

        SetNewSceneGroup(groupName);

        currentSceneSelection.LoadScenes();

        while (currentSceneSelection.ScenesLoading())
        {
            yield return null;
        }

        ForceOpenDoor();
    }
}
