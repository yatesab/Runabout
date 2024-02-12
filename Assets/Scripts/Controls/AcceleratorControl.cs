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
    public FlipSwitch _syncOffSwitch;

    [Header("Left Throttle Settings")]
    [SerializeField] private InputActionReference _leftStickAction;
    public Throttle leftThrottlePhysics;
    public Transform leftThrottleBody;

    [Header("Right Throttle Settings")]
    [SerializeField] private InputActionReference _rightStickAction;
    public Throttle rightThrottlePhysics;
    public Transform rightThrottleBody;

    private Vector2 _pitchYaw;
    private float _roll;

    // Start is called before the first frame update
    private void Start()
    {

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
        if(!_syncOffSwitch.switchOn && leftThrottlePhysics.Grabbed && !rightThrottlePhysics.Grabbed)
        {
            rightThrottlePhysics.SyncThrottlePosition(leftThrottlePhysics.MirrorLocalPosition.z);
        }else if (!_syncOffSwitch.switchOn && rightThrottlePhysics.Grabbed && !leftThrottlePhysics.Grabbed)
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

        propulsionSystem.SetThrustPercentage();
    }

    private void SetShipRotation()
    {
        float _throttleDiff = leftThrottlePhysics.ThrottlePercentage - rightThrottlePhysics.ThrottlePercentage;
        float newYaw = _throttleDiff > 0.15f || _throttleDiff < 0.15f ? _throttleDiff : 0f;
    }
}
