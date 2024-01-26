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

public class Throttle : MonoBehaviour
{
    public ThrottleSide throttleLocation;
    public float _throttleDeadZone = 0.02f;
    public bool useRoll = false;
    public bool usePitch = false;
    public bool useYaw = false;

    public Vector3 MirrorLocalPosition { get { return transform.InverseTransformPoint(_throttleHandle.position); } }
    public float ThrottlePercentage {set;get;}
    public bool Grabbed {set;get;}

    [SerializeField] private Transform _throttleHandle;
    [SerializeField] private Transform _throttleCeiling;
    [SerializeField] private Transform _throttleFloor;

    private float maxDistance;
    private Vector3 handlePosition;

    // Start is called before the first frame update
    void Start()
    {
        // Get XRGrabInteractable for isGrabbing check
        XRGrabInteractable _grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        _grabInteractable.selectEntered.AddListener(HandleStartGrab);
        _grabInteractable.selectExited.AddListener(HandleStopGrab);

        maxDistance = Vector3.Distance(_throttleFloor.position, _throttleCeiling.position);
    }

    // Update is called once per frame
    void Update()
    {
        float currentDistance = maxDistance - Vector3.Distance(_throttleHandle.position, _throttleCeiling.position);

        ThrottlePercentage = (currentDistance) / (maxDistance);        
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
}
