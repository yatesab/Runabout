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
    [SerializeField] private InputActionReference _rightThrottleReset;
    [SerializeField] protected InputActionReference _leftFireAction;
    [SerializeField] protected InputActionReference _rightFireAction;

    [Header("Left Throttle Settings")]
    [SerializeField] private Throttle leftThrottle;

    [Header("Right Throttle Settings")]
    [SerializeField] private Throttle rightThrottle;

    private Vector2 _pitchYaw;
    private float _roll;

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
        _rightThrottleReset.action.performed += OnThrottleReset;

        _leftFireAction.action.performed += OnLeftWeaponPressed;
        _leftFireAction.action.canceled += OnLeftWeaponStopped;

        _rightFireAction.action.performed += OnRightWeaponPressed;
        _rightFireAction.action.canceled += OnRightWeaponStopped;
    }

    private void Update()
    { 
        // Sync Throttles if set to sync and oposite throttle is grabbed
        SyncLeftThrottle();
        SyncRightThrottle();

        // Set Thrust and Yaw on propulsion system based on throttle percentage
        SetThrustPercentage();

        // Set Rotation for sticks
        SetShipRotation();
    }

    private void SyncLeftThrottle()
    {
        if(leftThrottle.isSynced && rightThrottle.Grabbed)
        {
            leftThrottle.SetThrottlePercentage(rightThrottle.ThrottlePercentage);
        }
    }

    private void SyncRightThrottle()
    {
        if(rightThrottle.isSynced && leftThrottle.Grabbed)
        {
            rightThrottle.SetThrottlePercentage(leftThrottle.ThrottlePercentage);
        }
    }

    private void SetThrustPercentage()
    {
        float newThrust = Mathf.Max(leftThrottle.ThrottlePercentage, rightThrottle.ThrottlePercentage);

        propulsionSystem.SetThrustPercentage(newThrust);
    }

    private void SetShipRotation()
    {
        float _throttleDiff = leftThrottle.ThrottlePercentage - rightThrottle.ThrottlePercentage;
        float newYaw = _throttleDiff > 0.15f || _throttleDiff < 0.15f ? _throttleDiff / 2 : 0f;

        propulsionSystem.SetYawRotation(_pitchYaw.x + newYaw);
        propulsionSystem.SetPitchRotation(_pitchYaw.y);
        propulsionSystem.SetRollRotation(_roll);
    }

    /**
        Input Actions
    */
    private void OnLeftStickPressed(InputAction.CallbackContext obj)
    {
        if(leftThrottle.Grabbed)
        {
            _pitchYaw = obj.ReadValue<Vector2>();
        }
    }

    private void OnLeftStickStopped(InputAction.CallbackContext obj)
    {
        _pitchYaw = Vector2.zero;
    }

    private void OnRightStickPressed(InputAction.CallbackContext obj)
    {
        if(rightThrottle.Grabbed)
        {
            _roll = obj.ReadValue<Vector2>().x;
        }
    }

    private void OnRightStickStopped(InputAction.CallbackContext obj)
    {
        _roll = 0f;
    }

    private void OnThrottleReset(InputAction.CallbackContext obj)
    {
        if(leftThrottle.Grabbed)
        {
            if(!rightThrottle.Grabbed)
            {
                rightThrottle.isSynced = true;
            }
        } else if(rightThrottle.Grabbed)
        {
            if(!leftThrottle.Grabbed)
            {
                leftThrottle.isSynced = true;
            }
        } else 
        {
            leftThrottle.isSynced = true;
            rightThrottle.isSynced = true;

            leftThrottle.SetThrottlePercentage(0f);
            rightThrottle.SetThrottlePercentage(0f);
        }
    }
    
    private void OnLeftWeaponPressed(InputAction.CallbackContext obj)
    {
        if(leftThrottle.Grabbed)
        {
            weaponSystem.FireLeftWeapon();
        }
    }

    private void OnLeftWeaponStopped(InputAction.CallbackContext obj)
    {
        weaponSystem.StopLeftWeapon();
    }

    private void OnRightWeaponPressed(InputAction.CallbackContext obj)
    {
        if(rightThrottle.Grabbed)
        {
            weaponSystem.FireRightWeapon();
        }
    }

    private void OnRightWeaponStopped(InputAction.CallbackContext obj)
    {
        weaponSystem.StopRightWeapon();
    }
}
