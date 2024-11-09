using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

enum LEFT_RIGHT_MOVEMENT_TYPES
{
    YAW,
    ROLL,
    STRAFE,
    NONE
}

enum UP_DOWN_MOVEMENT_TYPES
{
    PITCH,
    STRAFE,
    NONE
}

public class FlightStick : MonoBehaviour
{
    public bool StickGrabbed { get; set; }

    [SerializeField] private Engines engines;
    [SerializeField] private float stickDeadZoneUpper = 0.1f;
    [SerializeField] private float stickDeadZoneLower = -0.1f;

    [Header("Stick Action")]
    [SerializeField] private InputActionReference stickAction;

    private XRGrabInteractable interactable;
    [SerializeField] private LEFT_RIGHT_MOVEMENT_TYPES movementLeftRightSetting;
    [SerializeField] private UP_DOWN_MOVEMENT_TYPES movementUpDownSetting;
    [SerializeField] private LEFT_RIGHT_MOVEMENT_TYPES stickLeftRightSetting;
    [SerializeField] private UP_DOWN_MOVEMENT_TYPES stickUpDownSetting;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponentInChildren<XRGrabInteractable>();

        interactable.selectEntered.AddListener(HandleGrabStick);
        interactable.selectExited.AddListener(HandleReleaseStick);
    }

    // Update is called once per frame
    void Update()
    {
        if (StickGrabbed)
        {
            UpdateMovement();
        }
    }

    private void UpdateMovement()
    {
        float newMovementX = GetMovement(interactable.transform.localRotation.x);
        switch (movementUpDownSetting)
        {
            case UP_DOWN_MOVEMENT_TYPES.PITCH:
                engines.Pitch = newMovementX;
                break;
            case UP_DOWN_MOVEMENT_TYPES.STRAFE:
                engines.UpDownStrafe = newMovementX;
                break;
        }

        float newMovementZ = GetMovement(interactable.transform.localRotation.z) * -1;
        switch (movementLeftRightSetting)
        {
            case LEFT_RIGHT_MOVEMENT_TYPES.YAW:
                engines.Yaw = GetMovement(interactable.transform.localRotation.y);
                break;
            case LEFT_RIGHT_MOVEMENT_TYPES.ROLL:
                engines.Roll = newMovementZ;
                break;
            case LEFT_RIGHT_MOVEMENT_TYPES.STRAFE:
                engines.LeftRightStrafe = newMovementZ;
                break;
        }
    }

    private float GetMovement(float rotationAmount)
    {
        if (rotationAmount > stickDeadZoneUpper)
        {
            return rotationAmount - stickDeadZoneUpper;
        }
        else if (rotationAmount < stickDeadZoneLower)
        {
            return rotationAmount - stickDeadZoneLower;
        }
        else
        {
            return 0f;
        }
    }

    public void ChangeMovementLeftRightSetting(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 2:
                movementLeftRightSetting = LEFT_RIGHT_MOVEMENT_TYPES.STRAFE;
                break;
            case 1:
                movementLeftRightSetting = LEFT_RIGHT_MOVEMENT_TYPES.ROLL;
                break;
            case 0:
            default:
                movementLeftRightSetting = LEFT_RIGHT_MOVEMENT_TYPES.YAW; 
                break;

        }
    }

    public void ChangeStickLeftRightSetting(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 2:
                stickLeftRightSetting = LEFT_RIGHT_MOVEMENT_TYPES.STRAFE;
                break;
            case 1:
                stickLeftRightSetting = LEFT_RIGHT_MOVEMENT_TYPES.ROLL;
                break;
            case 0:
            default:
                stickLeftRightSetting = LEFT_RIGHT_MOVEMENT_TYPES.YAW;
                break;

        }
    }

    public void ChangeMovementUpDownSetting(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 1:
                movementUpDownSetting = UP_DOWN_MOVEMENT_TYPES.PITCH;
                break;
            case 0:
            default:
                movementUpDownSetting = UP_DOWN_MOVEMENT_TYPES.STRAFE;
                break;

        }
    }

    public void ChangeStickUpDownSetting(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 1:
                stickUpDownSetting = UP_DOWN_MOVEMENT_TYPES.PITCH;
                break;
            case 0:
            default:
                stickUpDownSetting = UP_DOWN_MOVEMENT_TYPES.STRAFE;
                break;

        }
    }

    private void HandleStickMovement(InputAction.CallbackContext obj)
    {
        Vector2 stickNewValue = obj.ReadValue<Vector2>();

        // Rotation Controls for stick
        switch (stickLeftRightSetting)
        {
            case LEFT_RIGHT_MOVEMENT_TYPES.YAW:
                engines.Yaw = stickNewValue.x;
                break;
            case LEFT_RIGHT_MOVEMENT_TYPES.ROLL:
                engines.Roll = stickNewValue.x;
                break;
            case LEFT_RIGHT_MOVEMENT_TYPES.STRAFE:
                engines.LeftRightStrafe = stickNewValue.x;
                break;
        }

        switch(stickUpDownSetting)
        {
            case UP_DOWN_MOVEMENT_TYPES.PITCH:
                engines.Pitch = stickNewValue.y;
                break;
            case UP_DOWN_MOVEMENT_TYPES.STRAFE:
                engines.UpDownStrafe = stickNewValue.y;
                break;
        }
    }

    private void StopStickMovement(InputAction.CallbackContext obj)
    {
        // Rotation Controls for stick
        if (stickLeftRightSetting == LEFT_RIGHT_MOVEMENT_TYPES.YAW)
        {
            engines.Yaw = 0;
        }

        if (stickUpDownSetting == UP_DOWN_MOVEMENT_TYPES.PITCH)
        {
            engines.Pitch = 0;
        }

        if (stickLeftRightSetting == LEFT_RIGHT_MOVEMENT_TYPES.ROLL)
        {
            engines.Roll = 0;
        }

        // Strafe Controls for stick
        if (stickLeftRightSetting == LEFT_RIGHT_MOVEMENT_TYPES.STRAFE)
        {
            engines.LeftRightStrafe = 0;
        }

        if (stickUpDownSetting == UP_DOWN_MOVEMENT_TYPES.STRAFE)
        {
            engines.UpDownStrafe = 0;
        }
    }

    public void HandleGrabStick(SelectEnterEventArgs args)
    {
        StickGrabbed = true;
        PlayerConditionManager.instance.SetPlayerMovement(false);

        // Add stick listeners when flight stick is grabbed
        stickAction.action.performed += HandleStickMovement;
        stickAction.action.canceled += StopStickMovement;
    }

    public void HandleReleaseStick(SelectExitEventArgs args)
    {
        StickGrabbed = false;
        PlayerConditionManager.instance.SetPlayerMovement(true);

        interactable.transform.localPosition = new Vector3(0, 0, 0);
        interactable.transform.localRotation = new Quaternion(0, 0, 0, 0);

        engines.Pitch = 0;
        engines.Yaw = 0;
        engines.Roll = 0;
        engines.LeftRightStrafe = 0;
        engines.UpDownStrafe = 0;

        // Remove stick listeners when flight stick is let go
        stickAction.action.performed -= HandleStickMovement;
        stickAction.action.canceled -= StopStickMovement;
    }
}
