using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class FlipSwitch : MonoBehaviour
{
    public bool switchOn = false;
    public FlipSwitchMirror flipSwitchMirror;

    [Header("Flip Actions")]
    public UnityEvent FlipOnAction;
    public UnityEvent FlipOffAction;

    private Material originalMaterial;

    // Start is called before the first frame update
    void Start()
    {
        // Get XRSimpleInteractable for isGrabbing check
        XRSimpleInteractable switchInteractable = GetComponent<XRSimpleInteractable>();

        switchInteractable.selectEntered.AddListener(HandleFlipSwitch);
        switchInteractable.hoverEntered.AddListener(HandleHoverEnter);
        switchInteractable.hoverExited.AddListener(HandleHoverExit);
    }

    // Update is called once per frame
    void Update()
    {
        flipSwitchMirror.MirrorObject(transform);
    }

    public void HandleFlipSwitch(SelectEnterEventArgs args)
    {
        // Check current state and then change based on that.
        if (switchOn)
        {
            transform.Rotate(-90f, 0f, 0f);
            switchOn = false;

            if(FlipOffAction != null)
            {
                FlipOffAction.Invoke();
            }
        }
        else
        {
            transform.Rotate(90f, 0f, 0f);
            switchOn = true;

            if (FlipOnAction != null)
            {
                FlipOnAction.Invoke();
            }
        }
    }

    public void HandleHoverEnter(HoverEnterEventArgs args)
    {
        // Do hover things here
        if(flipSwitchMirror.hasHover)
        {
            flipSwitchMirror.ActivateHover();
        }
    }

    public void HandleHoverExit(HoverExitEventArgs args)
    {
        // Do hover things here
        if (flipSwitchMirror.hasHover)
        {
            flipSwitchMirror.DeactivateHover();
        }
    }
}
