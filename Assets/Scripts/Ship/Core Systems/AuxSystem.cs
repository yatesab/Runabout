using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxSystem : MonoBehaviour, ShipSystem
{
    public bool isPatched {get;set;} = false;
    public float PowerLevel {get;set;} = 0f;
    public TransporterSystem transporterSystem;

    public void PatchToSystem(float powerUpgrade)
    {
        PowerLevel = powerUpgrade;
        isPatched = true;

        transporterSystem.gameObject.SetActive(isPatched);
    }

    public void RemovePatchToSystem(float powerUpgrade)
    {
        PowerLevel = powerUpgrade;
        isPatched = false;

        transporterSystem.gameObject.SetActive(isPatched);
    }
}
