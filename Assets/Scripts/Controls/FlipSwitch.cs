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
    [SerializeField] private float rotateAmount = 60f;

    public void HandleFlipSwitch()
    {
        // Check current state and then change based on that.
        if (switchOn)
        {
            transform.Rotate(rotateAmount * -1, 0f, 0f);
            switchOn = false;

            if(FlipOffAction != null)
            {
                FlipOffAction.Invoke();
            }
        }
        else
        {
            transform.Rotate(rotateAmount, 0f, 0f);
            switchOn = true;

            if (FlipOnAction != null)
            {
                FlipOnAction.Invoke();
            }
        }
    }
}
