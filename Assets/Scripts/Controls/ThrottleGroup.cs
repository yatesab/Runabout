using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleGroup : MonoBehaviour
{
    public Throttle LeftThrottle { get { return leftThrottle; } }
    public Throttle RightThrottle { get { return rightThrottle; } }

    [SerializeField] private Engines engines;
    [SerializeField] private Throttle leftThrottle;
    [SerializeField] private Throttle rightThrottle;

    public bool canControlEngines = true;

    // Update is called once per frame
    void Update()
    {
        SyncThrottles();
        SetEnginePercentage();
    }

    private void SyncThrottles()
    {
        if(leftThrottle.Grabbed && !rightThrottle.Grabbed)
        {
            rightThrottle.SyncThrottlePosition(leftThrottle.ThrottlePosition);
        }
        
        if(rightThrottle.Grabbed && !leftThrottle.Grabbed)
        {
            leftThrottle.SyncThrottlePosition(rightThrottle.ThrottlePosition);
        }
    }

    public void SetEnginePercentage()
    {
        if(canControlEngines)
        {
            engines.PortThrottle = leftThrottle.ThrottlePercentage;
            engines.StarboardThrottle = rightThrottle.ThrottlePercentage;

            if(leftThrottle.Grabbed && rightThrottle.Grabbed)
            {
                float _throttleDiff = leftThrottle.ThrottlePercentage - rightThrottle.ThrottlePercentage;
                engines.YawThrottle = (_throttleDiff > 0.15f || _throttleDiff < 0.15f ? _throttleDiff : 0f) / 2;
            } else
            {
                engines.YawThrottle = 0f;
            }
        }
    }

    public void GrabLeftThrottle()
    {
        leftThrottle.Grabbed = true;
    }

    public void GrabRightThrottle()
    {
        rightThrottle.Grabbed = true;
    }

    public void ReleaseLeftThrottle()
    {
        leftThrottle.Grabbed = false;

        if (!rightThrottle.Grabbed)
        {
            // Set It to the same as this one
            rightThrottle.SyncThrottlePosition(leftThrottle.ThrottlePosition);
        }
    }

    public void ReleaseRightThrottle()
    {
        rightThrottle.Grabbed = false;
        
        if (!leftThrottle.Grabbed)
        {
            // Set It to the same as this one
            leftThrottle.SyncThrottlePosition(rightThrottle.ThrottlePosition);
        }
    }
}
