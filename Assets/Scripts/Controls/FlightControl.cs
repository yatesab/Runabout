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

    [Header("Input Actions")]
    [SerializeField] private InputActionReference pitchYawSource;
    [SerializeField] private InputActionReference strafeInputSource;
    [SerializeField] private InputActionReference upDownInputSource;

    private Transform controlHandle;
    private TMP_Text pitchPercentage;

    private float handlePercentage = 0f;

    private bool isGrabbed;

    // Start is called before the first frame update
    private void Start()
    {
        XRGrabInteractable grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(HandleStartGrab);
        grabInteractable.selectExited.AddListener(HandleStopGrab);

        // Pull Children Components
        controlHandle = GameObject.Find("Control Handle").transform;
        pitchPercentage = GetComponentInChildren<TMP_Text>();

        pitchYawSource.action.performed += OnStickPressed;
        pitchYawSource.action.canceled += OnStickStopped;

        // Set up Strafe Left / Right Actions
        strafeInputSource.action.performed += OnStrafePressed;
        strafeInputSource.action.canceled += OnStrafeStopped;

        // Set up Strage Up / Down Actions
        upDownInputSource.action.performed += OnUpDownPressed;
        upDownInputSource.action.canceled += OnUpDownStopped;
    }

    private void Update()
    {
        handlePercentage = transform.InverseTransformPoint(controlHandle.position).z / 0.2f;
        UpdateScreen(handlePercentage);

        if(handlePercentage > 0.1f || handlePercentage < -0.1f )
        {
            propulsionSystem.SetLeftThrustPercentage(handlePercentage);
        } else 
        {
            propulsionSystem.SetLeftThrustPercentage(0f);
        }
    }

    private void UpdateScreen(float newPercentage)
    {
        if(handlePercentage > 0.2f || handlePercentage < -0.2f )
        {
            pitchPercentage.text = Mathf.Round(newPercentage * 100).ToString() + " %";
        } else 
        {
            pitchPercentage.text = "0 %";
        }
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

    /**
        Input Actions
    */
    private void OnStickPressed(InputAction.CallbackContext obj)
    {
        Vector2 pitchYaw = obj.ReadValue<Vector2>();

        if(isGrabbed)
        {
            propulsionSystem.SetRollRotation(pitchYaw.x);
        }
    }

    private void OnStickStopped(InputAction.CallbackContext obj)
    {
        if(isGrabbed)
        {
            propulsionSystem.SetRollRotation(0f);
        }
    }

    private void OnStrafePressed(InputAction.CallbackContext obj)
    {
        if(isGrabbed)
        {
            propulsionSystem.SetStrafingStarted(true);
        }
    }

    private void OnStrafeStopped(InputAction.CallbackContext obj)
    {
        if(isGrabbed)
        {
            propulsionSystem.SetStrafingStarted(false);
        }
    }

    private void OnUpDownPressed(InputAction.CallbackContext obj)
    {
        if(isGrabbed)
        {
            propulsionSystem.SetUpDownStarted(true);
        }
    }

    private void OnUpDownStopped(InputAction.CallbackContext obj)
    {
        if(isGrabbed)
        {
            propulsionSystem.SetUpDownStarted(false);
        }
    }
}
