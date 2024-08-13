using UnityEngine;

public class TransportPanel : MonoBehaviour
{
    [SerializeField] private SceneGroup newScene;

    public void StartTransport()
    {
        GameConditionManager.instance.StartTransport(newScene);
    }
}
