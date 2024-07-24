using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPanel : MonoBehaviour
{
    [SerializeField] private SceneGroup newScene;

    private GameConditionManager conditionManager;

    // Start is called before the first frame update
    void Awake()
    {
        conditionManager = GameObject.Find("Game Condition Manager").GetComponent<GameConditionManager>();
    }

    public void StartTransport()
    {
        conditionManager.StartTransport(newScene);
    }
}
