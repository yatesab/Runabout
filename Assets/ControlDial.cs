using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlDial : MonoBehaviour
{
    public MeshMirror controlDialMirror;

    [SerializeField] private int snapRotationAmount = 90;
    [SerializeField] private float angleTolerance;

    private IXRSelectInteractor interactor;
    private float startAngle;
    private bool requiresStartAngle = true;
    private bool shouldGetHandRotation = false;
    private XRGrabInteractable _grabInteractable;
    
    void Start()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();

        _grabInteractable.hoverEntered.AddListener(HandleHoverEnter);
        _grabInteractable.hoverExited.AddListener(HandleHoverExit);
        _grabInteractable.selectEntered.AddListener(HandleGrab);
        _grabInteractable.selectExited.AddListener(HandleLetgo);
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldGetHandRotation)
        {
            var rotationAngle = GetInteractorRotation();
            
            GetRotationDistance(rotationAngle);
        }
    }

    public float GetInteractorRotation() => interactor.transform.eulerAngles.z;

    private void GetRotationDistance(float currentAngle)
    {
        if (requiresStartAngle)
        {
            startAngle = currentAngle;
            requiresStartAngle = false;
        }

        var angleDifference = Mathf.Abs(startAngle - currentAngle);

        if (angleDifference > angleTolerance)
        {
            if(angleDifference > 270f)
            {
                float angleCheck = CheckAngle(currentAngle, startAngle);
                
                if (angleCheck < angleTolerance) return;
                else if (startAngle < currentAngle)
                {
                    RotateDialCounterClockwise();
                }
                else if(startAngle > currentAngle) 
                {
                    RotateDialClockwise();
                }

                startAngle = currentAngle;
            } else
            {
                if (startAngle < currentAngle)
                {
                    RotateDialClockwise();
                } else if (startAngle > currentAngle)
                {
                    RotateDialCounterClockwise();
                }
                    
                startAngle = currentAngle;
            }
        }
    }

    private float CheckAngle(float currentAngle, float startAngle) => (360f - currentAngle) + startAngle;

    private void RotateDialClockwise()
    {
        // Rotate Dial Clockwise and the mirror
        transform.Rotate(0f, 0f, snapRotationAmount);

        controlDialMirror.MirrorRotation(transform.localRotation);
    }

    private void RotateDialCounterClockwise()
    {
        // Rotate dial counter clockwise and the mirror
        transform.Rotate(0f, 0f, -snapRotationAmount);

        controlDialMirror.transform.localEulerAngles = transform.localEulerAngles;
    }

    private void HandleHoverEnter(HoverEnterEventArgs args)
    {
        // Do hover things here
        if (controlDialMirror.hasHover)
        {
            controlDialMirror.ActivateHover();
        }
    }

    private void HandleHoverExit(HoverExitEventArgs args)
    {
        // Do hover things here
        if (controlDialMirror.hasHover)
        {
            controlDialMirror.DeactivateHover();
        }
    }

    private void HandleGrab(SelectEnterEventArgs args)
    {
        interactor = _grabInteractable.interactorsSelecting[0];

        shouldGetHandRotation = true;
        startAngle = 0f;
    }


    private void HandleLetgo(SelectExitEventArgs args)
    {
        shouldGetHandRotation = false;
        requiresStartAngle = true;
    }
}
