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

    [SerializeField] private GameObject victoryCanvas;

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

    public void ActivateGameVictory()
    {
        physicsShip.ShutOffPower();
        victoryCanvas.SetActive(true);

        playerRig.ActivateXRInteractors();
        playerRig.transform.SetParent(transform);

        playerRig.transform.localPosition = new Vector3(0, 0, 0);
        playerRig.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    public void ResetGameScene()
    {
        playerRig.DeactivateXRInteractors();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (cargoHold.itemsInCargoHold.Count > 0 && cargoHold.itemsInCargoHold[0].Name == winItem.Name)
        {
            ActivateGameVictory();
        }
    }
}
