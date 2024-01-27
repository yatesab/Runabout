using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CoreSystem : MonoBehaviour, ShipSystem
{
    public bool isPatched {get;set;} = false;
    public float PowerLevel {get;set;} = 0f;
    public float HeatLevel { get; set; } = 0f;
    public ShipSystem PatchedToSystem {get;set;}

    public virtual void PatchToSystem(float powerUpgrade)
    {
        PowerLevel += powerUpgrade;
    }

    public virtual void RemovePatchToSystem(float powerUpgrade)
    {
        PowerLevel -= powerUpgrade;
    }

    public virtual void PatchFromSystem(ShipSystem InSystem, float powerDowngrade)
    {
        PatchedToSystem = InSystem;
        PowerLevel -= powerDowngrade;
        
        isPatched = true;
    }

    public virtual void RemovePatchFromSystem(float powerDowngrade)
    {
        PatchedToSystem = null;
        PowerLevel += powerDowngrade;

        isPatched = false;
    }
}
