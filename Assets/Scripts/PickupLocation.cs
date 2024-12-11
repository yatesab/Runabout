using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLocation : MonoBehaviour 
{
    public int seconds = 0;
    public Coroutine RunningTimmer { get; set; }
    public int ID {get; set;}
    public int MaxResources { get; set; }

    public float MaxResetTime { get; set;}

    public bool IsTimerRunning { get; set; }

    public int currentResources;

    public void StartTimer()
    {
        if (currentResources < MaxResources)
        {
            StartCoroutine(Timer());
        }
    }

    public void AddResource()
    {
        currentResources++;
    }

    public IEnumerator Timer()
    {
        IsTimerRunning = true;
        while (seconds < MaxResetTime)
        {
            seconds++;
            yield return new WaitForSeconds(1);
        }
        seconds = 0;

        AddResource();
        if (currentResources < MaxResources)
        {
            StartCoroutine(Timer());
        } else
        {
            IsTimerRunning = false;
        }
    }
}
