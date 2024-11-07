using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FlightStick : MonoBehaviour
{
    public bool StickGrabbed { get; set; }

    [SerializeField] private Engines engines;
    [SerializeField] private float stickDeadZoneUpper = 0.1f;
    [SerializeField] private float stickDeadZoneLower = -0.1f;

    [Header("Stick Action")]
    [SerializeField] private InputActionReference stickAction;

    private XRGrabInteractable interactable;
     
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
            //engines.Pitch = GetStickMovement(interactable.transform.localRotation.x);
            //engines.Yaw = GetStickMovement(interactable.transform.localRotation.y);
            engines.Roll = GetStickMovement(interactable.transform.localRotation.z) * -1;
        }
    }

    private float GetStickMovement(float rotationAmount)
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
        // Rotation Controls for stick
        engines.Yaw = obj.ReadValue<Vector2>().x;
        engines.Pitch = obj.ReadValue<Vector2>().y;

        // Strafe Controls for stick
        //engines.LeftRightStrafe = obj.ReadValue<Vector2>().x;
        //engines.UpDownStrafe = obj.ReadValue<Vector2>().y;
    }

    private void StopStickMovement(InputAction.CallbackContext obj)
    {
        engines.Yaw = 0;
        engines.Pitch = 0;

        //engines.LeftRightStrafe = 0;
        //engines.UpDownStrafe = 0;
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
