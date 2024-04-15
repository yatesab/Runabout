using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia.Interactions.Controllables.LinearDriver;

public class ThrottleGroup : MonoBehaviour
{
    [SerializeField] private Engines engines;
    [SerializeField] private LinearDriveFacade leftThrottle;
    [SerializeField] private LinearDriveFacade rightThrottle;

    public bool canControlEngines = true;

    public float LeftPercentage { get; set; }
    public float RightPercentage { get; set; }
    public bool LeftThrottleGrabbed { get; set; } = false;
    public bool RightThrottleGrabbed { get; set; } = false;

    // Update is called once per frame
    void Update()
    {
        SyncThrottles();
        SetEnginePercentage();
    }

    private void SyncThrottles()
    {
        if(LeftThrottleGrabbed && !RightThrottleGrabbed)
        {
            rightThrottle.TargetValue = LeftPercentage;
        }
        
        if(RightThrottleGrabbed && !LeftThrottleGrabbed)
        {
            leftThrottle.TargetValue = RightPercentage;
        }
    }

    public void SetEnginePercentage()
    {
        if(canControlEngines)
        {
            engines.PortThrottle = leftThrottle.TargetValue;
            engines.StarboardThrottle = rightThrottle.TargetValue;

            if(LeftThrottleGrabbed && RightThrottleGrabbed)
            {
                float _throttleDiff = LeftPercentage - RightPercentage;
                engines.YawThrottle = (_throttleDiff > 0.15f || _throttleDiff < 0.15f ? _throttleDiff : 0f) / 2;
            } else
            {
                engines.YawThrottle = 0f;
            }
        }
    }

    public void GrabLeftThrottle()
    {
        LeftThrottleGrabbed = true;
        leftThrottle.MoveToTargetValue = false;
    }

    public void GrabRightThrottle()
    {
        RightThrottleGrabbed = true;
        rightThrottle.MoveToTargetValue = false;
    }

    public void ReleaseLeftThrottle()
    {
        LeftThrottleGrabbed = false;

        leftThrottle.MoveToTargetValue = true;

        //Find a value to use for this
        leftThrottle.TargetValue = LeftPercentage;

        if (!RightThrottleGrabbed )
        {
            // Set It to the same as this one
            rightThrottle.TargetValue = LeftPercentage;
            RightPercentage = LeftPercentage;
        }
    }

    public void ReleaseRightThrottle()
    {
        RightThrottleGrabbed = false;

        rightThrottle.MoveToTargetValue = true;
        
        //Find a value to use for this
        rightThrottle.TargetValue = RightPercentage;
        
        if (!LeftThrottleGrabbed)
        {
            // Set It to the same as this one
            leftThrottle.TargetValue = RightPercentage;
            LeftPercentage = RightPercentage;
        }
    }
}
