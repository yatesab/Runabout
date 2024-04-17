using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConditionManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateGameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    public void ResetGameScene()
    {
        Debug.Log("Scene Reset");
    }
}
