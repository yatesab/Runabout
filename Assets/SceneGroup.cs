using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGroup : MonoBehaviour
{
    [SerializeField] public SceneField[] scenes;

    public string groupName;

    public int Length {  get { return scenes.Length; } }

    [SerializeField] private List<AsyncOperation> sceneLoadOperations = new List<AsyncOperation>();
    private int loadingSceneIndex = 0;

    public void UnloadScenes()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == scenes[i].SceneName)
                {
                    sceneLoadOperations.Add(SceneManager.UnloadSceneAsync(scenes[i]));
                }
            }
        }
    }

    public void LoadScenes()
    {
        foreach (SceneField sceneField in scenes)
        {
            sceneLoadOperations.Add(SceneManager.LoadSceneAsync(sceneField, LoadSceneMode.Additive));
        }
    }

    public bool ScenesLoading()
    {
        foreach (AsyncOperation operation in sceneLoadOperations)
        {
            if (!operation.isDone)
            {
                return true;
            }
        }

        return false;
    }
}
