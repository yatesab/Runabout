using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightStick : MonoBehaviour
{
    private Transform interactable;
    public Engines engines;

    // Start is called before the first frame update
    void Start()
    {
        interactable = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        engines.Pitch = interactable.localRotation.x * -1;
        engines.Yaw = interactable.localRotation.y;
        engines.Roll = interactable.localRotation.z * -1;
    }

    public void ResetPosition()
    {
        interactable.localPosition = new Vector3(0, 0, 0);
        interactable.localRotation = new Quaternion(0, 0, 0, 0);
    }
}
