using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.XR.LegacyInputHelpers;

public class PlayerRecenter : MonoBehaviour
{
    private CameraOffset cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = GetComponent<CameraOffset>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecenterPlayerRig()
    {
        cameraOffset.requestedTrackingMode = UserRequestedTrackingMode.Device;

        cameraOffset.requestedTrackingMode = UserRequestedTrackingMode.Floor;
    }
}
