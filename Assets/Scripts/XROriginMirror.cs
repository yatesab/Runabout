using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XROriginMirror : MonoBehaviour
{
    public Transform XROrigin;

    public Transform CameraOffset;
    public Transform CameraOffsetMirror;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = XROrigin.localPosition;
        transform.localRotation = XROrigin.localRotation;

        CameraOffsetMirror.localPosition = CameraOffset.localPosition;
        CameraOffsetMirror.localRotation = CameraOffset.localRotation;
    }
}
