using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerConditionManager : MonoBehaviour
{
    public static PlayerConditionManager instance { get; private set; }

    private PlayerCameraManager playerCameraManager;
    private LocomotionSystem locomotionSystem;
    private FadeScreen fadeScreen;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Condition Manager");
        }
        instance = this;

        playerCameraManager = GetComponentInChildren<PlayerCameraManager>();
        locomotionSystem = GetComponentInChildren<LocomotionSystem>();
        fadeScreen = GetComponentInChildren<FadeScreen>();
    }

    public void SetPlayerMovement(bool isActive)
    {
        locomotionSystem.gameObject.SetActive(isActive);
    }

    public void SetNewLocation(Vector3 newLocation)
    {
        playerCameraManager.transform.position = newLocation;
    }
}
