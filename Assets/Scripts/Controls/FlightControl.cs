using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;

public class FlightControl : MonoBehaviour
{
    public PropulsionSystem propulsionSystem;
    public Transform _flightStick;

    private float stickPercentageX = 0f;
    private float stickPercentageY = 0f;
    protected bool isGrabbed;

    // Start is called before the first frame update
    private void Start()
    {
        XRGrabInteractable grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(HandleStartGrab);
        grabInteractable.selectExited.AddListener(HandleStopGrab);
    }

    private void Update()
    {
        stickPercentageX = _flightStick.localRotation.x / 0.7f;
        stickPercentageY = _flightStick.localRotation.y / 0.7f;
    }

    /**
        Grab Interactable Actions
    */
    private void HandleStartGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    private void HandleStopGrab(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }
}
