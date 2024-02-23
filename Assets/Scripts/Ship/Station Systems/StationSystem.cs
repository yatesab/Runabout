using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StationSystem : MonoBehaviour
{
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
}
