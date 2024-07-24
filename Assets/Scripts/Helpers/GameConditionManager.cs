using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameConditionManager : MonoBehaviour
{
    [SerializeField] private SceneGroup currentScene;
    [SerializeField] private FadeScreen fadeScreen;
    [SerializeField] private GameObject Player;

    public void Awake()
    {
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
        LocomotionSystem playerMovement = Player.GetComponentInChildren<LocomotionSystem>();
        playerMovement.gameObject.SetActive(false);

        // Wait for fade out to continue
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // Wait for scenes to unload and load
        currentScene.UnloadScenes();
        newSceneGroup.LoadScenes();
        while (newSceneGroup.ScenesLoading() || currentScene.ScenesLoading())
        {
            yield return null;
        }

        // Set new location and turn back on movement
        Player.transform.position = newSceneGroup.spawnLocation;
        playerMovement.gameObject.SetActive(true);

        // Setup the camera for the new location
        PlayerTracker playerTracker = Player.GetComponent<PlayerTracker>();
        if (newSceneGroup.needsConvergedCamera && playerTracker.isDiverged)
        {
            Player.GetComponent<PlayerTracker>().ConvergeCamera();
        } else if (!newSceneGroup.needsConvergedCamera && !playerTracker.isDiverged)
        {
            Player.GetComponent<PlayerTracker>().DivergeCamera();
        }

        // Clear out the operation lists
        currentScene.ClearSceneOperationsList();
        newSceneGroup.ClearSceneOperationsList();
        currentScene = newSceneGroup;

        fadeScreen.FadeIn();
    }
}
