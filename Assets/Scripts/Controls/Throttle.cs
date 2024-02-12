using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;

public enum ThrottleSide
{
    Left,
    Right
}

public class Throttle : GrabPhysics
{
    public float _throttleDeadZone = 0.02f;
    public enum StickMovementDirection
    {
        PitchYaw,
        Roll
    }
    public StickMovementDirection stickMovementSetting;

    public float ThrottlePercentage { set; get; }
    public bool Grabbed { set; get; }
    public Vector2 PitchYaw { set; get; }
    public float Roll { set; get; }

    [SerializeField] private Transform _throttleHandle;
    [SerializeField] private Transform _throttleCeiling;
    [SerializeField] private Transform _throttleFloor;

    [SerializeField] private InputActionReference _stickAction;

    private float maxDistance;
    private Vector3 handlePosition;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        grabInteractable.selectEntered.AddListener(HandleStartGrab);
        grabInteractable.selectExited.AddListener(HandleStopGrab);

        // Set up Stick Actions
        _stickAction.action.performed += OnStickPressed;
        _stickAction.action.canceled += OnStickStopped;

        maxDistance = Vector3.Distance(_throttleFloor.position, _throttleCeiling.position);
    }

    // Update is called once per frame
    void Update()
    {
        float currentDistance = maxDistance - Vector3.Distance(_throttleHandle.position, _throttleCeiling.position);

        ThrottlePercentage = (currentDistance) / (maxDistance);

        base.Update();
    }

    /**
        Grab Interactable Actions
    */
    private void HandleStartGrab(SelectEnterEventArgs args)
    {
        Grabbed = true;
    }

    private void HandleStopGrab(SelectExitEventArgs args)
    {
        Grabbed = false;
    }

    public void SyncThrottlePosition(float newLocalPositionZ)
    {
        _throttleHandle.localPosition = new Vector3(_throttleHandle.localPosition.x, _throttleHandle.localPosition.y, newLocalPositionZ);
    }

    public void ResetThrottle()
    {
        _throttleHandle.localPosition = _throttleFloor.localPosition;
    }

    private void OnStickPressed(InputAction.CallbackContext obj)
    {
        switch(stickMovementSetting)
        {
            case StickMovementDirection.PitchYaw:
                PitchYaw = obj.ReadValue<Vector2>(); break;
            case StickMovementDirection.Roll:
                Roll = obj.ReadValue<Vector2>().x; break;
        }
       
    }

    private void OnStickStopped(InputAction.CallbackContext obj)
    {
        switch (stickMovementSetting)
        {
            case StickMovementDirection.PitchYaw:
                PitchYaw = Vector2.zero; break;
            case StickMovementDirection.Roll:
                Roll = 0f; break;
        }
    }
}
