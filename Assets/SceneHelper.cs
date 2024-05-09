using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHelper
{
    public static AsyncOperation LoadScene(string sceneToLoad)
    {
        return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }

    public static void QuitApplication()
    {
        Application.Quit();
    }
}
