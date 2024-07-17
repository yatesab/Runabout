using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public IEnumerator GoToSceneRoutine(string sceneName)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        //Launce the new scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public IEnumerator TeleportRoutine(SceneGroup newSceneGroup)
    {
        fadeScreen.FadeOut();
        currentScene.UnloadScenes();

        float timer = 0;
        while (timer <= fadeScreen.fadeDuration && currentScene.ScenesLoading())
        {
            yield return null;
        }

        newSceneGroup.LoadScenes();

        while (newSceneGroup.ScenesLoading())
        {
            yield return null;
        }

        Player.transform.position = new Vector3(0, 10, -3);
        Player.GetComponent<PlayerTracker>().ConvergeCamera();

        currentScene = newSceneGroup;
        fadeScreen.FadeIn();
    }
}
