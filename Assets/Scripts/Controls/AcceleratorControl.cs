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
    public FlipSwitch _syncSwitch;

    [Header("Left Throttle Settings")]
    [SerializeField] private InputActionReference _leftStickAction;
    [SerializeField] private InputActionReference _leftThrottleReset;
    [SerializeField] protected InputActionReference _leftFireAction;
    public Throttle leftThrottlePhysics;
    public Transform leftThrottleBody;

    [Header("Right Throttle Settings")]
    [SerializeField] private InputActionReference _rightStickAction;
    [SerializeField] private InputActionReference _rightThrottleReset;
    [SerializeField] protected InputActionReference _rightFireAction;
    public Throttle rightThrottlePhysics;
    public Transform rightThrottleBody;

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
        SyncThrottles();

        // Set Thrust and Yaw on propulsion system based on throttle percentage
        SetThrustPercentage();

        // Set Rotation for sticks
        SetShipRotation();
    }

    private void SyncThrottles()
    {
        if(_syncSwitch.switchOn && leftThrottlePhysics.Grabbed && !rightThrottlePhysics.Grabbed)
        {
            rightThrottlePhysics.SyncThrottlePosition(leftThrottlePhysics.MirrorLocalPosition.z);
        }else if (_syncSwitch.switchOn && rightThrottlePhysics.Grabbed && !leftThrottlePhysics.Grabbed)
        {
            leftThrottlePhysics.SyncThrottlePosition(rightThrottlePhysics.MirrorLocalPosition.z);
        }

        // Sync Mesh to the physics location
        rightThrottleBody.localPosition = rightThrottlePhysics.MirrorLocalPosition;
        leftThrottleBody.localPosition = leftThrottlePhysics.MirrorLocalPosition;
    }

    private void SetThrustPercentage()
    {
        float newThrust = Mathf.Max(leftThrottlePhysics.ThrottlePercentage, rightThrottlePhysics.ThrottlePercentage);

        propulsionSystem.SetThrustPercentage(newThrust);
    }

    private void SetShipRotation()
    {
        float _throttleDiff = leftThrottlePhysics.ThrottlePercentage - rightThrottlePhysics.ThrottlePercentage;
        float newYaw = _throttleDiff > 0.15f || _throttleDiff < 0.15f ? _throttleDiff : 0f;

        propulsionSystem.SetYawRotation(_pitchYaw.x + newYaw);
        propulsionSystem.SetPitchRotation(_pitchYaw.y);
        propulsionSystem.SetRollRotation(_roll);
    }

    /**
        Input Actions
    */
    private void OnLeftStickPressed(InputAction.CallbackContext obj)
    {
        _pitchYaw = obj.ReadValue<Vector2>();
    }

    private void OnLeftStickStopped(InputAction.CallbackContext obj)
    {
        _pitchYaw = Vector2.zero;
    }

    private void OnRightStickPressed(InputAction.CallbackContext obj)
    {
        _roll = obj.ReadValue<Vector2>().x;
    }

    private void OnRightStickStopped(InputAction.CallbackContext obj)
    {
        _roll = 0f;
    }

    private void OnThrottleReset(InputAction.CallbackContext obj)
    {
        leftThrottlePhysics.ResetThrottle();
        rightThrottlePhysics.ResetThrottle();
    }
    
    private void OnLeftWeaponPressed(InputAction.CallbackContext obj)
    {
        weaponSystem.FireLeftWeapon();
    }

    private void OnLeftWeaponStopped(InputAction.CallbackContext obj)
    {
        weaponSystem.StopLeftWeapon();
    }

    private void OnRightWeaponPressed(InputAction.CallbackContext obj)
    {
        weaponSystem.FireRightWeapon();
    }

    private void OnRightWeaponStopped(InputAction.CallbackContext obj)
    {
        weaponSystem.StopRightWeapon();
    }
}
