using System.Collections;
using UnityEngine;

[System.Serializable]
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance { get; private set; }

    [SerializeField] public SceneGroup currentScene;
    [SerializeField] private PlayerCameraManager playerCameraManager;

    public void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Game Condition Manager");
        }
        instance = this;

        if (currentScene != null)
        {
            StartCoroutine(InitialSceneLoad());
        }

        Application.runInBackground = true;
    }

    public void StartTransport(SceneGroup newScene)
    {
        StartCoroutine(TeleportRoutine(newScene));
    }

    private IEnumerator InitialSceneLoad()
    {
        // First load scenes
        currentScene.LoadScenes();
        while (currentScene.ScenesLoading())
        {
            yield return null;
        }

        PlayerConditionManager.instance.SetNewLocation(currentScene.spawnLocation);

        // Check startDiverged and check if we can diverge the camera
        if (currentScene.startDiverged && playerCameraManager.SetupSplitCamera())
        {
            playerCameraManager.DivergeCamera();
        }
    }

    private IEnumerator TeleportRoutine(SceneGroup newSceneGroup)
    {
        // Turn off the movement system while we transport
        playerCameraManager.PlayerFadeOut();
        yield return new WaitForSeconds(playerCameraManager.fadeDuration / 2);

        playerCameraManager.ConvergeCamera();

        // Wait for scenes to unload and load
        currentScene.UnloadScenes();
        newSceneGroup.LoadScenes();
        while (newSceneGroup.ScenesLoading() || currentScene.ScenesLoading())
        {
            yield return null;
        }

        // Set new location and turn back on movement
        PlayerConditionManager.instance.SetNewLocation(newSceneGroup.spawnLocation);

        // Setup the camera for the new location
        if (playerCameraManager.SetupSplitCamera()) {
            playerCameraManager.DivergeCamera();
        }

        // Clear out the operation lists
        currentScene.ClearSceneOperationsList();
        newSceneGroup.ClearSceneOperationsList();
        currentScene = newSceneGroup;

        playerCameraManager.PlayerFadeIn();
    }
}
