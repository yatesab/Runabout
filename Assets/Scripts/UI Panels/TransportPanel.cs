using UnityEngine;

public class TransportPanel : MonoBehaviour
{
    [SerializeField] private SceneGroup newScene;

    public void StartTransport()
    {
        GameSceneManager.instance.StartTransport(newScene);
    }
}
