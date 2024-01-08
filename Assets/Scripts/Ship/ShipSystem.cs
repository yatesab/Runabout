using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipSystem : MonoBehaviour
{
    public float PatchLimit { get; set; } = 300f;
    public float PowerLevel { get; set; } = 1f;
    public bool isDepleted { get; set; } = false;
    public bool isPatched { get; set; } = false;
    public bool hasPatch { get; set; } = false;
    public ShipSystem PatchedSystem { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PatchInSystem()
    {
        float newPowerLevel = PowerLevel + 0.5f;

        if(newPowerLevel <= 2f)
        {
            PowerLevel = newPowerLevel;

            hasPatch = true;
        }
    }

    public void PatchToSystem()
    {
        PowerLevel -= 0.25f;

        isPatched = true;
    }

    public void RemovePatchToSystem()
    {
        PowerLevel += 0.25f;

        isPatched = false;
    }

    public void RemovePatchInSystem()
    {
        if(hasPatch)
        {
            PowerLevel -= 0.5f;

            hasPatch = false;
        }
    }
}
