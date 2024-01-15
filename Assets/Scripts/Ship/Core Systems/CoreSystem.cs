using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CoreSystem : MonoBehaviour, ShipSystem
{
    public bool isPatched {get;set;} = false;
    public float PowerLevel {get;set;} = 1f;
    public ShipSystem PatchedToSystem {get;set;}

    public void PatchToSystem(float powerUpgrade)
    {
        PowerLevel += powerUpgrade;
        
    }

    public void RemovePatchToSystem(float powerUpgrade)
    {
        PowerLevel -= powerUpgrade;

    }

    public void PatchFromSystem(ShipSystem InSystem, float powerDowngrade)
    {
        PatchedToSystem = InSystem;
        PowerLevel -= powerDowngrade;
        
        isPatched = true;
    }

    public void RemovePatchFromSystem(float powerDowngrade)
    {
        PatchedToSystem = null;
        PowerLevel += powerDowngrade;

        isPatched = false;
    }
}
