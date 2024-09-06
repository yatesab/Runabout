using System.Collections;
using UnityEngine;

[System.Serializable]
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance { get; private set; }

    [SerializeField] private SceneGroup startMenuSceneGroup;
    [SerializeField] private SceneGroup currentScene;

    public void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Game Condition Manager");
        }
        instance = this;

        // Set current scene in nothing is selected to start menu
        if(currentScene == null){
            currentScene = startMenuSceneGroup;
        }

        StartCoroutine(LoadCurrentSceneGroup());

        Application.runInBackground = true;
    }

    public void SaveGameState()
    {
        PlayerConditionManager.instance.SavePlayerData();
        ShipConditionManager.instance.SaveShipData();
    }

    public void LoadGameState()
    {
        PlayerConditionManager.instance.LoadPlayerData();
        ShipConditionManager.instance.LoadShipData();
    }

    public void StartTransport(SceneGroup newScene)
    {
        StartCoroutine(TeleportRoutine(newScene));
    }

    private IEnumerator LoadCurrentSceneGroup()
    {
        // First load scenes
        currentScene.LoadScenes();
        while (currentScene.ScenesLoading())
        {
            yield return null;
        }

        // Set player to location
        //PlayerConditionManager.instance.SetNewLocation(currentScene.spawnLocation);

        // Check startDiverged and check if we can diverge the camera
        if (currentScene.startDiverged)
        {
            PlayerConditionManager.instance.SetupSplitCamera();
        }
    }

    private IEnumerator TeleportRoutine(SceneGroup newSceneGroup)
    {
        // Turn off the movement system while we transport
        PlayerConditionManager.instance.PlayerFadeOut();
        yield return new WaitForSeconds(PlayerConditionManager.instance.CameraFadeDuration / 2);

        PlayerConditionManager.instance.AttemptConvergeCamera();

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
        PlayerConditionManager.instance.SetupSplitCamera();

        // Clear out the operation lists
        currentScene.ClearSceneOperationsList();
        newSceneGroup.ClearSceneOperationsList();
        currentScene = newSceneGroup;

        PlayerConditionManager.instance.PlayerFadeIn();
    }
}
