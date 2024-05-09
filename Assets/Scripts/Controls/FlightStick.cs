using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlightStick : MonoBehaviour
{
    public bool StickGrabbed { get; set; }

    [SerializeField] private Engines engines;
    [SerializeField] private float stickDeadZoneUpper = 0.1f;
    [SerializeField] private float stickDeadZoneLower = -0.1f;

    private XRGrabInteractable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponentInChildren<XRGrabInteractable>();

        interactable.selectExited.AddListener(HandleReleaseStick);
    }

    // Update is called once per frame
    void Update()
    {
        if (StickGrabbed)
        {
            engines.Pitch = GetStickMovement(interactable.transform.localRotation.x);
            engines.Yaw = GetStickMovement(interactable.transform.localRotation.y);
            engines.Roll = GetStickMovement(interactable.transform.localRotation.z) * -1;
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

    public void HandleReleaseStick(SelectExitEventArgs args)
    {
        interactable.transform.localPosition = new Vector3(0, 0, 0);
        interactable.transform.localRotation = new Quaternion(0, 0, 0, 0);

        engines.Pitch = 0;
        engines.Yaw = 0;
        engines.Roll = 0;
    }
}
