using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public enum STICK_ORIENTATION
{
    LEFT = 0,
    RIGHT = 1
}

public class FlightStick : MonoBehaviour
{
    public bool StickGrabbed { get; set; }

    [Header("Stick Side")]
    public STICK_ORIENTATION stickOrientation;

    [Header("Engines")]
    [SerializeField] private Engines engines;
    [SerializeField] private float stickDeadZoneUpper = 0.1f;
    [SerializeField] private float stickDeadZoneLower = -0.1f;

    [Header("Stick Action")]
    [SerializeField] private InputActionReference leftStickAction;
    [SerializeField] private InputActionReference rightStickAction;

    [Header("Flight Stick Settings")]
    [SerializeField] private SHIP_MOVEMENT_TYPE movementLeftRightSetting;
    [SerializeField] private SHIP_MOVEMENT_TYPE movementUpDownSetting;
    [SerializeField] private SHIP_MOVEMENT_TYPE stickLeftRightSetting;
    [SerializeField] private SHIP_MOVEMENT_TYPE stickUpDownSetting;

    private XRGrabInteractable interactable;

    private void Awake()
    {
        if (stickOrientation == STICK_ORIENTATION.LEFT) {
            PlayerConditionManager.instance.LeftFlightStick = this;
        } else
        {
            PlayerConditionManager.instance.RightFlightStick = this;
        }
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
            case SHIP_MOVEMENT_TYPE.PITCH:
                engines.Pitch = newMovementX;
                break;
            case SHIP_MOVEMENT_TYPE.STRAFE:
                engines.UpDownStrafe = newMovementX;
                break;
        }

        float newMovementZ = GetMovement(interactable.transform.localRotation.z) * -1;
        switch (movementLeftRightSetting)
        {
            case SHIP_MOVEMENT_TYPE.YAW:
                engines.Yaw = GetMovement(interactable.transform.localRotation.y);
                break;
            case SHIP_MOVEMENT_TYPE.ROLL:
                engines.Roll = newMovementZ;
                break;
            case SHIP_MOVEMENT_TYPE.STRAFE:
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

    private void HandleStickMovement(InputAction.CallbackContext obj)
    {
        Vector2 stickNewValue = obj.ReadValue<Vector2>();

        // Rotation Controls for stick
        switch (stickLeftRightSetting)
        {
            case SHIP_MOVEMENT_TYPE.YAW:
                engines.Yaw = stickNewValue.x;
                break;
            case SHIP_MOVEMENT_TYPE.ROLL:
                engines.Roll = stickNewValue.x;
                break;
            case SHIP_MOVEMENT_TYPE.STRAFE:
                engines.LeftRightStrafe = stickNewValue.x;
                break;
        }

        switch(stickUpDownSetting)
        {
            case SHIP_MOVEMENT_TYPE.PITCH:
                engines.Pitch = stickNewValue.y;
                break;
            case SHIP_MOVEMENT_TYPE.STRAFE:
                engines.UpDownStrafe = stickNewValue.y;
                break;
        }
    }

    private void StopStickMovement(InputAction.CallbackContext obj)
    {
        // Rotation Controls for stick
        if (stickLeftRightSetting == SHIP_MOVEMENT_TYPE.YAW)
        {
            engines.Yaw = 0;
        }

        if (stickUpDownSetting == SHIP_MOVEMENT_TYPE.PITCH)
        {
            engines.Pitch = 0;
        }

        if (stickLeftRightSetting == SHIP_MOVEMENT_TYPE.ROLL)
        {
            engines.Roll = 0;
        }

        // Strafe Controls for stick
        if (stickLeftRightSetting == SHIP_MOVEMENT_TYPE.STRAFE)
        {
            engines.LeftRightStrafe = 0;
        }

        if (stickUpDownSetting == SHIP_MOVEMENT_TYPE.STRAFE)
        {
            engines.UpDownStrafe = 0;
        }
    }

    public void HandleGrabStick(SelectEnterEventArgs args)
    {
        StickGrabbed = true;
        PlayerConditionManager.instance.SetPlayerMovement(false);

        PullStickSettings();
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
        if (stickOrientation == STICK_ORIENTATION.LEFT)
        {
            leftStickAction.action.performed -= HandleStickMovement;
            leftStickAction.action.canceled -= StopStickMovement;
        } else
        {
            rightStickAction.action.performed -= HandleStickMovement;
            rightStickAction.action.canceled -= StopStickMovement;
        }
    }

    public void PullStickSettings()
    {
        if (stickOrientation == STICK_ORIENTATION.LEFT)
        {
            movementLeftRightSetting = PlayerConditionManager.instance.leftMovementLeftRightSetting;
            movementUpDownSetting = PlayerConditionManager.instance.leftMovementUpDownSetting;
            stickLeftRightSetting = PlayerConditionManager.instance.leftStickLeftRightSetting;
            stickUpDownSetting = PlayerConditionManager.instance.leftStickUpDownSetting;

            leftStickAction.action.performed += HandleStickMovement;
            leftStickAction.action.canceled += StopStickMovement;
        } else
        {
            movementLeftRightSetting = PlayerConditionManager.instance.rightMovementLeftRightSetting;
            movementUpDownSetting = PlayerConditionManager.instance.rightMovementUpDownSetting;
            stickLeftRightSetting = PlayerConditionManager.instance.rightStickLeftRightSetting;
            stickUpDownSetting = PlayerConditionManager.instance.rightStickUpDownSetting;

            rightStickAction.action.performed += HandleStickMovement;
            rightStickAction.action.canceled += StopStickMovement;
        }
    }
}
