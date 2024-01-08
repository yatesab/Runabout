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

    [Header("Input Actions")]
    [SerializeField] private InputActionReference stickAction;
    [SerializeField] private InputActionReference strafeInputSource;
    [SerializeField] private InputActionReference upDownInputSource;

    [SerializeField] private enum SIDES{
        Left,
        Right
    };
    [SerializeField] private SIDES thrustSide = SIDES.Left;
    [SerializeField] private bool resetWhenLetGo = true;
    [SerializeField] private bool useRoll = false;
    [SerializeField] private bool usePitch = false;

    private Transform thrustHandle;
    private TMP_Text thrustPercentage;
    private bool isGrabbed;

    private float handlePercentage = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        // Get XRGrabInteractable for isGrabbing check
        XRGrabInteractable grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(HandleStartGrab);
        grabInteractable.selectExited.AddListener(HandleStopGrab);

        // Pull Children Components
        thrustHandle = GetComponentInChildren<Rigidbody>().transform;
        thrustPercentage = GetComponentInChildren<TMP_Text>();

        // Set up Roll Actions
        stickAction.action.performed += OnStickPressed;
        stickAction.action.canceled += OnStickStopped;

        // Set up Strafe Left / Right Actions
        strafeInputSource.action.performed += OnStrafePressed;
        strafeInputSource.action.canceled += OnStrafeStopped;

        // Set up Strage Up / Down Actions
        upDownInputSource.action.performed += OnUpDownPressed;
        upDownInputSource.action.canceled += OnUpDownStopped;
    }

    private void Update()
    {
        handlePercentage = transform.InverseTransformPoint(thrustHandle.position).z / 0.2f;

        if(handlePercentage > 0.1f || handlePercentage < -0.1f )
        {
            UpdateScreen(handlePercentage);
            SetThrustPercentage(handlePercentage);
        }else {
            UpdateScreen(0f);
            SetThrustPercentage(0f);
        }
    }

    private void UpdateScreen(float newPercentage)
    {
        thrustPercentage.text = Mathf.Round(newPercentage * 100).ToString() + " %";
    }

    private void SetThrustPercentage(float percentage)
    {
        if(thrustSide == SIDES.Left){
            propulsionSystem.SetLeftThrustPercentage(percentage);
        } else {
            propulsionSystem.SetRightThrustPercentage(percentage);
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

        // Reset handle Position when let go. Default Off
        if(resetWhenLetGo){
            thrustHandle.localPosition = Vector3.zero;
        } else if(thrustHandle.localPosition.z < 0f) {
            thrustHandle.localPosition = new Vector3(thrustHandle.localPosition.x, thrustHandle.localPosition.y, 0f);
        }
    }

    /**
        Input Actions
    */
    private void OnStickPressed(InputAction.CallbackContext obj)
    {
        if(isGrabbed)
        {
            if(useRoll)
            {
                propulsionSystem.SetRollRotation(obj.ReadValue<Vector2>().x);
            }

            if(usePitch)
            {
                propulsionSystem.SetPitchRotation(-obj.ReadValue<Vector2>().y);
            }
        }
    }

    private void OnStickStopped(InputAction.CallbackContext obj)
    {
        if(isGrabbed)
        {
            if(useRoll)
            {
                propulsionSystem.SetRollRotation(0f);
            }

            if(usePitch)
            {
                propulsionSystem.SetPitchRotation(0f);
            }
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
