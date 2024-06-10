using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConditionManager : MonoBehaviour
{
    [SerializeField] private SceneGroup startingScenes;

    public void Awake()
    {
        foreach(SceneField scene in startingScenes.scenes)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }

        Application.runInBackground = true;
    }

    public void ResetGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
