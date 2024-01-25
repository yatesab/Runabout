using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlipSwitch : MonoBehaviour
{
    public bool switchOn = false;
    public Transform switchMirror;
    public ViewFinder activateObject;

    // Start is called before the first frame update
    void Start()
    {
        // Get XRSimpleInteractable for isGrabbing check
        XRSimpleInteractable switchInteractable = GetComponent<XRSimpleInteractable>();

        switchInteractable.selectEntered.AddListener(HandleFlipSwitch);
    }

    // Update is called once per frame
    void Update()
    {
        switchMirror.localRotation = transform.localRotation;
    }

    public void HandleFlipSwitch(SelectEnterEventArgs args)
    {
        // Check current state and then change based on that.
        if (switchOn)
        {
            transform.Rotate(-90f, 0f, 0f);
            switchOn = false;

            if(activateObject != null)
            {
                activateObject.FlipSwitchOffEvent();
            }
        }
        else
        {
            transform.Rotate(90f, 0f, 0f);
            switchOn = true;

            if (activateObject != null)
            {
                activateObject.FlipSwitchOnEvent();
            }
        }
    }

    public void HandleHoverSwitch(HoverEnterEventArgs args)
    {
        // Do hover things here
    }
}
