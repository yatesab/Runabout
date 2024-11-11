using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public enum SHIP_MOVEMENT_TYPE
{
    PITCH = 0,
    YAW = 1,
    ROLL = 2,
    STRAFE = 3,
    NONE = 4
}

public class PlayerConditionManager : MonoBehaviour
{
    public static PlayerConditionManager instance { get; private set; }
    public float CameraFadeDuration { get { return fadeScreen.fadeDuration; } }

    private PlayerCameraManager playerCameraManager;
    private LocomotionSystem locomotionSystem;
    private FadeScreen fadeScreen;
    private DynamicMoveProvider dynamicMoveProvider;
    private BadgeMenu badgeMenu;

    public FlightStick LeftFlightStick { get; set; }
    public FlightStick RightFlightStick { get; set;}

    [Header("Left Flight Stick Settings")]
    public SHIP_MOVEMENT_TYPE leftMovementLeftRightSetting;
    public SHIP_MOVEMENT_TYPE leftMovementUpDownSetting;
    public SHIP_MOVEMENT_TYPE leftStickLeftRightSetting;
    public SHIP_MOVEMENT_TYPE leftStickUpDownSetting;

    [Header("Right Flight Stick Settings")]
    public SHIP_MOVEMENT_TYPE rightMovementLeftRightSetting;
    public SHIP_MOVEMENT_TYPE rightMovementUpDownSetting;
    public SHIP_MOVEMENT_TYPE rightStickLeftRightSetting;
    public SHIP_MOVEMENT_TYPE rightStickUpDownSetting;

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
        dynamicMoveProvider = GetComponentInChildren<DynamicMoveProvider>();
        badgeMenu = GetComponentInChildren<BadgeMenu>();
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
        playerCameraManager.transform.position = playerData.Position;
        playerCameraManager.transform.rotation = playerData.Rotation;
    }
}
