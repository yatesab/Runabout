using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

[System.Serializable]
public class ControlDialEvent : UnityEvent<int>
{
}

public class ControlDial : GrabPhysics
{
    [SerializeField] private int snapRotationAmount = 90;
    [SerializeField] private float angleTolerance;
    [SerializeField] private Transform dial;
    [SerializeField] private int upperRotationLimit = 1;
    [SerializeField] private int lowerRotationLimit = -1;

    public ControlDialEvent DialEvent;

    private int currentRotationAmount = 0;
    private IXRSelectInteractor interactor;
    private float startAngle;
    private bool requiresStartAngle = true;
    private bool shouldGetHandRotation = false;
    
    public void Start()
    {
        base.Start();

        grabInteractable.selectEntered.AddListener(HandleGrab);
        grabInteractable.selectExited.AddListener(HandleLetgo);
    }

    // Update is called once per frame
    public void Update()
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
                    RotateDialClockwise();
                    startAngle = currentAngle;
                }
                else if(startAngle > currentAngle) 
                {
                    RotateDialCounterClockwise();
                    startAngle = currentAngle;
                }
            } else
            {
                if (startAngle < currentAngle)
                {
                    RotateDialCounterClockwise();
                    startAngle = currentAngle;
                } else
                {
                    RotateDialClockwise();
                    startAngle = currentAngle;
                }
            }
        }
    }

    private float CheckAngle(float currentAngle, float startAngle) => (360f - currentAngle) + startAngle;

    private void RotateDialClockwise()
    {
        if(currentRotationAmount < upperRotationLimit)
        {         
            // Rotate Dial Clockwise and the mirror
            dial.Rotate(0f, 0f, -snapRotationAmount);
            meshMirror.MirrorRotation(Quaternion.Inverse(transform.rotation) * dial.rotation);
            currentRotationAmount += 1;

            DialEvent.Invoke(currentRotationAmount);
        }
        else
        {
            Debug.Log("Can't Turn Clockwise Anymore");
        }
    }

    private void RotateDialCounterClockwise()
    {
        if (currentRotationAmount > lowerRotationLimit)
        {
            // Rotate dial counter clockwise and the mirror
            dial.Rotate(0f, 0f, snapRotationAmount);
            meshMirror.MirrorRotation(Quaternion.Inverse(transform.rotation) * dial.rotation);
            currentRotationAmount -= 1;

            DialEvent.Invoke(currentRotationAmount);
        }
        else
        {
            Debug.Log("Can't Turn Counter Clockwise Anymore");
        }
    }

    private void HandleGrab(SelectEnterEventArgs args)
    {
        interactor = grabInteractable.interactorsSelecting[0];

        shouldGetHandRotation = true;
        startAngle = 0f;
    }


    private void HandleLetgo(SelectExitEventArgs args)
    {
        shouldGetHandRotation = false;
        requiresStartAngle = true;
    }
}
