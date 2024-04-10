using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightStick : MonoBehaviour
{
    public Engines engines;
    public float stickDeadZone = 0.1f;

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
            if(interactable.localRotation.x > stickDeadZone || interactable.localRotation.x < stickDeadZone * -1)
            {
                engines.Pitch = interactable.localRotation.x - stickDeadZone;
            }
            else
            {
                engines.Pitch = 0f;
            }

            if (interactable.localRotation.y > stickDeadZone || interactable.localRotation.y < stickDeadZone * -1)
            {
                engines.Yaw = interactable.localRotation.y - stickDeadZone;
            }
            else
            {
                engines.Yaw = 0f;
            }

            if (interactable.localRotation.z > stickDeadZone || interactable.localRotation.z < stickDeadZone * -1)
            {
                engines.Roll = (interactable.localRotation.z - stickDeadZone) * -1;
            }
            else
            {
                engines.Roll = 0f;
            }
        }
    }

    public void ResetPosition()
    {
        if (canControlEngines)
        {
            Vector3 newPosition = new Vector3(0, 0, 0);
            Quaternion newRotation = new Quaternion(0, 0, 0, 0);

            interactable.localPosition = newPosition;
            interactable.localRotation = newRotation;

            engines.Pitch = 0;
            engines.Yaw = 0;
            engines.Roll = 0;
        }
    }
}
