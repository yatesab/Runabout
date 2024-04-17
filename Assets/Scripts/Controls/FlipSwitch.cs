using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class FlipSwitch : MonoBehaviour
{
    public bool switchOn = false;

    [Header("Flip Actions")]
    public UnityEvent FlipOnAction;
    public UnityEvent FlipOffAction;

    public void HandleFlipSwitch()
    {
        // Check current state and then change based on that.
        if (switchOn)
        {
            transform.Rotate(-60f, 0f, 0f);
            switchOn = false;

            if(FlipOffAction != null)
            {
                FlipOffAction.Invoke();
            }
        }
        else
        {
            transform.Rotate(60f, 0f, 0f);
            switchOn = true;

            if (FlipOnAction != null)
            {
                FlipOnAction.Invoke();
            }
        }
    }
}
