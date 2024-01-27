using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxSystem : CoreSystem
{
    public TransporterSystem transporterSystem;

    public override void PatchToSystem(float powerUpgrade)
    {
        PowerLevel = powerUpgrade;
        isPatched = true;

        transporterSystem.gameObject.SetActive(isPatched);
    }

    public override void RemovePatchToSystem(float powerUpgrade)
    {
        PowerLevel = powerUpgrade;
        isPatched = false;

        transporterSystem.gameObject.SetActive(isPatched);
    }
}
