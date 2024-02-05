using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    public MeshMirror turretMirror;
    public Transform turret;
    public bool Grabbed { set; get; }

    private XRGrabInteractable _grabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        _grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        _grabInteractable.hoverEntered.AddListener(HandleHoverEnter);
        _grabInteractable.hoverExited.AddListener(HandleHoverExit);
        _grabInteractable.selectEntered.AddListener(HandleStartGrab);
        _grabInteractable.selectExited.AddListener(HandleStopGrab);
    }

    // Update is called once per frame
    void Update()
    {
        turretMirror.MirrorRotation(Quaternion.Inverse(transform.rotation) * turret.rotation);
    }

    private void HandleHoverEnter(HoverEnterEventArgs args)
    {
        // Do hover things here
        if (turretMirror.hasHover)
        {
            turretMirror.ActivateHover();
        }
    }

    private void HandleHoverExit(HoverExitEventArgs args)
    {
        // Do hover things here
        if (turretMirror.hasHover)
        {
            turretMirror.DeactivateHover();
        }
    }

    private void HandleStartGrab(SelectEnterEventArgs args)
    {
        Grabbed = true;
    }

    private void HandleStopGrab(SelectExitEventArgs args)
    {
        Grabbed = false;
    }
}
