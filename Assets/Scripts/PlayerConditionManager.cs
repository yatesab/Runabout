using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerConditionManager : MonoBehaviour
{
    public static PlayerConditionManager instance { get; private set; }
    public float CameraFadeDuration { get { return fadeScreen.fadeDuration; } }

    private PlayerCameraManager playerCameraManager;
    private LocomotionSystem locomotionSystem;
    private FadeScreen fadeScreen;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Player Condition Manager");
        }
        instance = this;

        playerCameraManager = GetComponentInChildren<PlayerCameraManager>();
        locomotionSystem = GetComponentInChildren<LocomotionSystem>();
        fadeScreen = GetComponentInChildren<FadeScreen>();
    }
    public void PlayerFadeOut()
    {
        fadeScreen.FadeOut();
        SetPlayerMovement(false);
    }

    public void PlayerFadeIn()
    {
        fadeScreen.FadeIn();
        SetPlayerMovement(true);
    }

    public void SetPlayerMovement(bool isActive)
    {
        locomotionSystem.gameObject.SetActive(isActive);
    }

    public void SetNewLocation(Vector3 newLocation)
    {
        playerCameraManager.transform.position = newLocation;
    }

    public void SetupSplitCamera()
    {
        if (playerCameraManager.SetupSplitCamera())
        {
            playerCameraManager.DivergeCamera();
        }
    }

    public void AttemptConvergeCamera()
    {
        if(playerCameraManager.isDiverged)
        {
            playerCameraManager.ConvergeCamera();
        }
    }

    public void SavePlayerData()
    {
        GameSaveManager.SavePlayerData(playerCameraManager.transform);
    }

    public void LoadPlayerData()
    {
        PlayerData playerData = GameSaveManager.LoadPlayerData();
        playerCameraManager.transform.position = playerData.GetLocationData();
        playerCameraManager.transform.rotation = playerData.GetRotationData();
    }
}
