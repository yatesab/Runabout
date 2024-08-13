using System.Collections;
using UnityEngine;

[System.Serializable]

public class GameConditionManager : MonoBehaviour
{
    public static GameConditionManager instance { get; private set; }

    [SerializeField] public SceneGroup currentScene;
    [SerializeField] private PlayerCameraManager playerCameraManager;

    public void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Game Condition Manager");
        }
        instance = this;

        currentScene.LoadScenes();

        Application.runInBackground = true;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void StartTransport(SceneGroup newScene)
    {
        StartCoroutine(TeleportRoutine(newScene));
    }

    public IEnumerator TeleportRoutine(SceneGroup newSceneGroup)
    {
        // Turn off the movement system while we transport
        playerCameraManager.PlayerFadeOut();
        yield return new WaitForSeconds(playerCameraManager.fadeDuration);

        // Wait for scenes to unload and load
        currentScene.UnloadScenes();
        newSceneGroup.LoadScenes();
        while (newSceneGroup.ScenesLoading() || currentScene.ScenesLoading())
        {
            yield return null;
        }

        // Set new location and turn back on movement
        playerCameraManager.SetNewLocation(newSceneGroup.spawnLocation);

        // Setup the camera for the new location
        if (newSceneGroup.needsConvergedCamera && playerCameraManager.isDiverged)
        {
            playerCameraManager.ConvergeCamera();
        } else if (!newSceneGroup.needsConvergedCamera && !playerCameraManager.isDiverged)
        {
            playerCameraManager.DivergeCamera();
        }

        // Clear out the operation lists
        currentScene.ClearSceneOperationsList();
        newSceneGroup.ClearSceneOperationsList();
        currentScene = newSceneGroup;

        playerCameraManager.PlayerFadeIn();
    }
}
