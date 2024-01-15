using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ShipSystem
{
    public bool isPatched {get;set;}
    public float PowerLevel {get;set;}

    public void PatchToSystem(float powerUpgrade);

    public void RemovePatchToSystem(float powerUpgrade);
}
