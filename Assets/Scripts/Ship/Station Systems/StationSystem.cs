using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StationSystem : MonoBehaviour
{
    public float PowerLevel { get; set; } = 1f;

    public bool isOverheated {  get; set; }
    public bool isHeating { get; set; }

    public float HeatLevel { get; set; } = 0f;
    public float maxHeatLevel = 60f;
    public float dangerHeatLevel = 40f;

    public void AddHeat(float timePassed)
    {
        if(HeatLevel < maxHeatLevel)
        {
            HeatLevel += timePassed;
        }
    }

    public void RemoveHeat(float timePassed)
    {
        if(HeatLevel > 0f)
        {
            HeatLevel -= timePassed;
        }
    }

    public virtual void AddPower(float powerUpgrade)
    {
        PowerLevel += powerUpgrade;
    }

    public virtual void RemovePower(float powerUpgrade)
    {
        PowerLevel -= powerUpgrade;
    }
}
