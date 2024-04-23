using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConditionManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private PhysicsShip physicsShip;
    [SerializeField] private PlayerRig playerRig;
    [SerializeField] private Transform PlayerPositionGameOver;
    [SerializeField] private CargoHold cargoHold;

    [SerializeField] private Item winItem;

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
        physicsShip.ShutOffPower();
        gameOverCanvas.SetActive(true);

        playerRig.ActivateXRInteractors();

        playerRig.transform.SetParent(PlayerPositionGameOver);

        playerRig.transform.localPosition = new Vector3(0,0,0);
        playerRig.transform.localRotation = new Quaternion(0,0,0,0);
    }

    public void ResetGameScene()
    {
        playerRig.DeactivateXRInteractors();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (cargoHold.itemsInCargoHold[0].Name == winItem.Name)
        {
            ActivateGameOver();
        }
    }
}
