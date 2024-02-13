using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XROriginMirror : MonoBehaviour
{
    public Transform _cameraOffset;

    public Transform _originMirror;
    public Transform _cameraOffsetMirror;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _originMirror.localPosition = transform.localPosition;
        _originMirror.localRotation = transform.localRotation;

        _cameraOffsetMirror.localPosition = _cameraOffset.localPosition;
        _cameraOffsetMirror.localRotation = _cameraOffset.localRotation;
    }
}
