using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightStick : MonoBehaviour
{
    public Engines engines;
    public float stickDeadZoneUpper = 0.1f;
    public float stickDeadZoneLower = -0.1f;

    private Transform interactable;

    public bool StickGrabbed { get; set; }
    public bool canControlEngines = true;

    // Start is called before the first frame update
    void Start()
    {
        interactable = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (StickGrabbed && canControlEngines)
        {
            engines.Pitch = GetStickMovement(interactable.localRotation.x);
            engines.Yaw = GetStickMovement(interactable.localRotation.y);
            engines.Roll = GetStickMovement(interactable.localRotation.z) * -1;
        }
    }

    private float GetStickMovement(float rotationAmount)
    {
        if (rotationAmount > stickDeadZoneUpper)
        {
            return rotationAmount - stickDeadZoneUpper;
        }
        else if (rotationAmount < stickDeadZoneLower)
        {
            return rotationAmount - stickDeadZoneLower;
        }
        else
        {
            return 0f;
        }
    }

    public void HandleGrabStick()
    {
        StickGrabbed = true;
    }

    public void HandleReleaseStick()
    {
        StickGrabbed = false;

        if (canControlEngines)
        {
            interactable.localPosition = new Vector3(0, 0, 0);
            interactable.localRotation = new Quaternion(0, 0, 0, 0);

            engines.Pitch = 0;
            engines.Yaw = 0;
            engines.Roll = 0;
        }
    }
}
