using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance { get; private set; }

    [SerializeField] private SceneField startMenuScene;
    [SerializeField] private SceneField shipScene;
    [SerializeField] private SceneField newGameScene;
    [SerializeField] private SceneField currentScene;
    [SerializeField] private SceneField loadedScene;

    public void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Game Condition Manager");
        }
        instance = this;

        AsyncOperation sceneLoading = LoadScene(startMenuScene);
        loadedScene = startMenuScene;

        Application.runInBackground = true;
    }

    public void LoadMainMenu()
    {
        StartCoroutine(SceneChangePlayerFade(startMenuScene, false));
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

    public void StartNewGame()
    {
        GameSaveManager.CreateNewPlayerData(new Vector3(0, 0, 0));
        GameSaveManager.CreateNewShipData();

        PlayerConditionManager.instance.LoadPlayerData();

        StartCoroutine(SceneChangePlayerFade(newGameScene, true));
    }    

    public void InitiateWarp(SceneField newScene)
    {
        StartCoroutine(LoadNewScene(newScene));
    }
    private AsyncOperation LoadScene(SceneField newScene)
    {
        return SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
    }

    private AsyncOperation UnloadScene(SceneField sceneToRemove)
    {
        return SceneManager.UnloadSceneAsync(sceneToRemove);
    }

    private IEnumerator LoadNewScene(SceneField newScene)
    {
        AsyncOperation sceneUnloading = UnloadScene(loadedScene);
        while (sceneUnloading.isDone == false)
        {
            yield return null;
        }

        //Move ship external to correct location for arrival

        AsyncOperation sceneLoading = LoadScene(newScene);
        while (sceneLoading.isDone == false)
        {
            yield return null;
        }
        loadedScene = newScene;
    }

    private IEnumerator SceneChangePlayerFade(SceneField newScene, bool loadShip)
    {
        // Turn off the movement system while we transport
        PlayerConditionManager.instance.PlayerFadeOut();
        yield return new WaitForSeconds(PlayerConditionManager.instance.CameraFadeDuration);

        PlayerConditionManager.instance.AttemptConvergeCamera();

        // Wait for scenes to unload and load
        AsyncOperation sceneUnloading = UnloadScene(loadedScene);
        AsyncOperation sceneLoading = LoadScene(newScene);
        while (sceneLoading.isDone == false && sceneUnloading.isDone == false)
        {
            yield return null;
        }
        Scene sceneRef = SceneManager.GetSceneByName("Ship");
        if (loadShip && sceneRef.isLoaded == false)
        {
            AsyncOperation shipSceneLoading = LoadScene(shipScene);
            while (shipSceneLoading.isDone == false)
            {
                yield return null;
            }

            // Set new location and turn back on movement
            PlayerConditionManager.instance.SetNewLocation(new Vector3(0, 0, 0));
            ShipConditionManager.instance.MoveShip(new Vector3(0,-15,-3000));
        } 
        else if (loadShip == false && sceneRef.isLoaded == true)
        {
            AsyncOperation shipSceneLoading = LoadScene(shipScene);
            while (shipSceneLoading.isDone == false)
            {
                yield return null;
            }

            // Set new location and turn back on movement
            PlayerConditionManager.instance.SetNewLocation(new Vector3(0, 0, 0));
        }

        // Clear out the operation lists
        loadedScene = newScene;

        PlayerConditionManager.instance.PlayerFadeIn();

        // Setup the camera for the new location
        PlayerConditionManager.instance.SetupSplitCamera();
    }
}
