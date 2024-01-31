using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StationSystem : MonoBehaviour
{
    public float PowerLevel { get; set; } = 1f;

    public bool isOverheated { get { return HeatLevel >= maxHeatLevel; } }
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

    public virtual void PatchToSystem(float powerUpgrade)
    {
        PowerLevel += powerUpgrade;
    }

    public virtual void RemovePatchToSystem(float powerUpgrade)
    {
        PowerLevel -= powerUpgrade;
    }

    public virtual void PatchFromSystem(float powerDowngrade)
    {
        PowerLevel -= powerDowngrade;
    }

    public virtual void RemovePatchFromSystem(float powerDowngrade)
    {
        PowerLevel += powerDowngrade;
    }
}
