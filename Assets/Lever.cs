using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public MeshMirror leverMirror;

    [SerializeField] private Transform lever;
    public float leverPercentage = 0f;

    private int startEulerAngle = 270;
    private int endEulerAngle = 180;
    private int maxDistance;
    private XRGrabInteractable _grabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        _grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        _grabInteractable.hoverEntered.AddListener(HandleHoverEnter);
        _grabInteractable.hoverExited.AddListener(HandleHoverExit);

        maxDistance = startEulerAngle - endEulerAngle;
    }

    // Update is called once per frame
    void Update()
    {
        leverMirror.MirrorRotation(Quaternion.Inverse(transform.rotation) * lever.rotation);
        leverPercentage = (lever.localEulerAngles.z - endEulerAngle) / maxDistance;
    }

    private void HandleHoverEnter(HoverEnterEventArgs args)
    {
        // Do hover things here
        if (leverMirror.hasHover)
        {
            leverMirror.ActivateHover();
        }
    }

    private void HandleHoverExit(HoverExitEventArgs args)
    {
        // Do hover things here
        if (leverMirror.hasHover)
        {
            leverMirror.DeactivateHover();
        }
    }
}
