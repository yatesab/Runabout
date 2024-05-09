using System;
using UnityEngine;

public class MainMenu : Menu
{
    
    [SerializeField] private AutomaticDoor _automaticDoor;
    
    private string loadedFloor = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadTurboLiftScene(string sceneToLoad)
    {
        if(loadedFloor != sceneToLoad)
        {
            loadedFloor = sceneToLoad;
            AsyncOperation loadScene = SceneHelper.LoadScene(sceneToLoad);

            loadScene.completed += HandleFloorLoaded;
        }
    }

    private void HandleFloorLoaded(AsyncOperation operation)
    {
        _automaticDoor.UpdateForceClose(false);
        _automaticDoor.OpenDoor();
    }
}
