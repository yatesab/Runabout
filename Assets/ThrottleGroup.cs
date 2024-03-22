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
        rightThrottle.TargetValue = LeftThrottleGrabbed && !RightThrottleGrabbed ? LeftPercentage : RightPercentage;
        rightThrottle.MoveToTargetValue = LeftThrottleGrabbed && !RightThrottleGrabbed;

        leftThrottle.TargetValue = RightThrottleGrabbed && !LeftThrottleGrabbed ? RightPercentage : LeftPercentage;
        leftThrottle.MoveToTargetValue = RightThrottleGrabbed &&  !LeftThrottleGrabbed;
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
}
