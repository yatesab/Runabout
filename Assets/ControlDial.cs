using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlDial : MonoBehaviour
{
    public MeshMirror controlDialMirror;

    // Start is called before the first frame update
    void Start()
    {
        // Get XRGrabInteractable for isGrabbing check
        XRGrabInteractable _grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        _grabInteractable.hoverEntered.AddListener(HandleHoverEnter);
        _grabInteractable.hoverExited.AddListener(HandleHoverExit);
    }

    // Update is called once per frame
    void Update()
    {
        controlDialMirror.MirrorRotation(transform.localRotation);
    }

    public void HandleHoverEnter(HoverEnterEventArgs args)
    {
        // Do hover things here
        if (controlDialMirror.hasHover)
        {
            controlDialMirror.ActivateHover();
        }
    }

    public void HandleHoverExit(HoverExitEventArgs args)
    {
        // Do hover things here
        if (controlDialMirror.hasHover)
        {
            controlDialMirror.DeactivateHover();
        }
    }
}
