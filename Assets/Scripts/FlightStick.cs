using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightStick : MonoBehaviour
{
    private Transform interactable;
    public Engines engines;

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
            engines.Pitch = interactable.localRotation.x * -1;
            engines.Yaw = interactable.localRotation.y;
            engines.Roll = interactable.localRotation.z * -1;
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
