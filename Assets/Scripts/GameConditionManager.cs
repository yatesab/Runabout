using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConditionManager : MonoBehaviour
{
    [SerializeField] private PhysicsShip physicsShip;
    [SerializeField] private Transform playerRig;
    [SerializeField] private Transform PlayerPositionGameOver;

    [SerializeField] private GameObject PlayerMenuCanvas;

    [Header("Win Condition")]
    [SerializeField] private CargoHold cargoHold;
    [SerializeField] private Item winItem;

    private void ReparentPlayerRig(Transform newParent)
    {
        playerRig.SetParent(newParent);

        playerRig.localPosition = new Vector3(0, 0, 0);
        playerRig.localRotation = new Quaternion(0, 0, 0, 0);
    }

    public void ActivateGameOver()
    {
        physicsShip.ShutOffPower();
        PlayerMenuCanvas.SetActive(true);

        ReparentPlayerRig(PlayerPositionGameOver);
    }

    public void ActivateGameVictory()
    {
        physicsShip.ShutOffPower();
        PlayerMenuCanvas.SetActive(true);
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

    public void OnTriggerEnter(Collider other)
    {
        if (cargoHold.itemsInCargoHold.Count > 0 && cargoHold.itemsInCargoHold[0].Name == winItem.Name)
        {
            ActivateGameVictory();
        }
    }
}
