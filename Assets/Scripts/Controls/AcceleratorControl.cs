using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;

public class AcceleratorControl : MonoBehaviour
{
    public PropulsionSystem propulsionSystem;
    public WeaponSystem weaponSystem;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference _leftStickAction;
    [SerializeField] private InputActionReference _rightStickAction;
    [SerializeField] private InputActionReference _leftThrottleReset;
    [SerializeField] private InputActionReference upDownInputSource;
    [SerializeField] protected InputActionReference leftFireAction;
    [SerializeField] protected InputActionReference rightFireAction;

    [Header("Left Throttle Settings")]
    [SerializeField] private Throttle leftThrottle;

    [Header("Right Throttle Settings")]
    [SerializeField] private Throttle rightThrottle;

    // Start is called before the first frame update
    private void Start()
    {
        // Set up Stick Actions
        _leftStickAction.action.performed += OnLeftStickPressed;
        _leftStickAction.action.canceled += OnLeftStickStopped;

        _rightStickAction.action.performed += OnRightStickPressed;
        _rightStickAction.action.canceled += OnRightStickStopped;

        // Set up Strafe Left / Right Actions
        _leftThrottleReset.action.performed += OnThrottleReset;

        // Set up Strage Up / Down Actions
        upDownInputSource.action.performed += OnUpDownPressed;
        upDownInputSource.action.canceled += OnUpDownStopped;

        leftFireAction.action.performed += OnLeftWeaponPressed;
        leftFireAction.action.canceled += OnLeftWeaponStopped;

        rightFireAction.action.performed += OnRightWeaponPressed;
        rightFireAction.action.canceled += OnRightWeaponStopped;
    }

    private void Update()
    {
        if(leftThrottle.throttleReset || rightThrottle.throttleReset)
        {
            UpdateThrottlePositions();
        }

        CheckThrustPercentage(leftThrottle);
        CheckYawPercentage(rightThrottle);
    }

    private void CheckThrustPercentage(Throttle checkThrottle)
    {
        propulsionSystem.SetThrustPercentage(checkThrottle.ThrottlePercentage);
    }

    private void CheckYawPercentage(Throttle checkThrottle)
    {
        float throttleDiff = leftThrottle.ThrottlePercentage - rightThrottle.ThrottlePercentage;

        if(throttleDiff > 0.15f || throttleDiff < -0.15f)
        {
            propulsionSystem.SetYawRotation(throttleDiff);
        } else 
        {
            propulsionSystem.SetYawRotation(0f);
        }
    }

    private void UpdateThrottlePositions()
    {
    }

    /**
        Input Actions
    */
    private void OnLeftStickPressed(InputAction.CallbackContext obj)
    {
        if(leftThrottle.Grabbed)
        {
            if(leftThrottle.useRoll)
            {
                propulsionSystem.SetRollRotation(obj.ReadValue<Vector2>().x);
            }

            if(leftThrottle.usePitch)
            {
                propulsionSystem.SetPitchRotation(-obj.ReadValue<Vector2>().y);
            }
        }
    }

    private void OnLeftStickStopped(InputAction.CallbackContext obj)
    {
        if(leftThrottle.Grabbed)
        {
            if(leftThrottle.useRoll)
            {
                propulsionSystem.SetRollRotation(0f);
            }

            if(leftThrottle.usePitch)
            {
                propulsionSystem.SetPitchRotation(0f);
            }
        }
    }

    private void OnRightStickPressed(InputAction.CallbackContext obj)
    {
        if(rightThrottle.Grabbed)
        {
            if(rightThrottle.useRoll)
            {
                propulsionSystem.SetRollRotation(obj.ReadValue<Vector2>().x);
            }

            if(rightThrottle.usePitch)
            {
                propulsionSystem.SetPitchRotation(-obj.ReadValue<Vector2>().y);
            }
        }
    }

    private void OnRightStickStopped(InputAction.CallbackContext obj)
    {
        if(rightThrottle.Grabbed)
        {
            if(rightThrottle.useRoll)
            {
                propulsionSystem.SetRollRotation(0f);
            }

            if(rightThrottle.usePitch)
            {
                propulsionSystem.SetPitchRotation(0f);
            }
        }
    }

    private void OnThrottleReset(InputAction.CallbackContext obj)
    {
        if(leftThrottle.Grabbed)
        {
            if(!rightThrottle.Grabbed)
            {
                rightThrottle.throttleReset = true;
            }
        } else if(rightThrottle.Grabbed)
        {
            if(!leftThrottle.Grabbed)
            {
                leftThrottle.throttleReset = true;
            }
        } else 
        {
            leftThrottle.SetThrottlePercentage(0f);
            rightThrottle.SetThrottlePercentage(0f);
        }
    }

    private void OnUpDownPressed(InputAction.CallbackContext obj)
    {
        // if(isGrabbed)
        // {
        //     propulsionSystem.SetUpDownStarted(true);
        // }
    }

    private void OnUpDownStopped(InputAction.CallbackContext obj)
    {
        // if(isGrabbed)
        // {
        //     propulsionSystem.SetUpDownStarted(false);
        // }
    }
    
    private void OnLeftWeaponPressed(InputAction.CallbackContext obj)
    {
        // if(isGrabbed)
        // {
        //     weaponSystem.FireLeftWeapon();
        // }
    }

    private void OnLeftWeaponStopped(InputAction.CallbackContext obj)
    {
        // if(isGrabbed)
        // {
        //     weaponSystem.StopLeftWeapon();
        // }
    }

    private void OnRightWeaponPressed(InputAction.CallbackContext obj)
    {
        // if(isGrabbed)
        // {
        //     weaponSystem.FireRightWeapon();
        // }
    }

    private void OnRightWeaponStopped(InputAction.CallbackContext obj)
    {
        // if(isGrabbed)
        // {
        //     weaponSystem.StopRightWeapon();
        // }
    }
}
