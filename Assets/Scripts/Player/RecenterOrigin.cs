using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class RecenterOrigin : MonoBehaviour
{
    public Transform target;
    private XROrigin xrOrigin;

    // Start is called before the first frame update
    void Start()
    {
        xrOrigin = GetComponent<XROrigin>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Recenter()
    {
        xrOrigin.MoveCameraToWorldLocation(target.position);
        xrOrigin.MatchOriginUpCameraForward(target.up, target.forward);
    }
}
