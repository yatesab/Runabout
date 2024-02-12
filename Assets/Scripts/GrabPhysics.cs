using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabPhysics : MonoBehaviour
{
    public Transform _grabbedObject;
    public MeshMirror meshMirror;
    
    public Vector3 MirrorLocalPosition { get { return transform.InverseTransformPoint(_grabbedObject.position); } }
    public Quaternion MirrorLocalRotation { get { return Quaternion.Inverse(transform.rotation) * _grabbedObject.rotation; } }

    protected XRGrabInteractable grabInteractable;

    public void Start()
    {
        // Get XRGrabInteractable for isGrabbing check
        grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        grabInteractable.hoverEntered.AddListener(HandleHoverEnter);
        grabInteractable.hoverExited.AddListener(HandleHoverExit);
    }

    public void Update()
    {
        meshMirror.MirrorPosition(MirrorLocalPosition);
        meshMirror.MirrorRotation(MirrorLocalRotation);
    }

    private void HandleHoverEnter(HoverEnterEventArgs args)
    {
        // Do hover things here
        if (meshMirror.hasHover)
        {
            meshMirror.ActivateHover();
        }
    }

    private void HandleHoverExit(HoverExitEventArgs args)
    {
        // Do hover things here
        if (meshMirror.hasHover)
        {
            meshMirror.DeactivateHover();
        }
    }
}
