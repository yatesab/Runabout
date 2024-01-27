using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCore : MonoBehaviour
{
    [Header("Aux System")]
    [SerializeField] private AuxSystem auxSystem;
    public float AuxPower 
    { 
        get { return auxSystem.PowerLevel; } 
    }

    [Header("Propulsion System")]
    [SerializeField] private PropulsionSystem propulsionSystem;
    public float PropulsionPower { 
        get { return propulsionSystem.PowerLevel; } 
    }
    public bool PropulsionIsPatched {
        get { return propulsionSystem.isPatched; }
    }

    [Header("Shield System")]
    [SerializeField] private ShieldSystem shieldSystem;
    public float ShieldPower { 
        get { return shieldSystem.PowerLevel; } 
    }
    public bool ShieldIsPatched {
        get { return shieldSystem.isPatched; }
    }

    [Header("Weapon System")]
    [SerializeField] private WeaponSystem weaponSystem;
    public float WeaponPower { 
        get { return weaponSystem.PowerLevel; } 
    }
    public bool WeaponIsPatched {
        get { return weaponSystem.isPatched; }
    }

    public enum SystemType 
    {
        Aux,
        Propulsion,
        Shield,
        Weapon
    }

    public CoreSystem PatchFrom {get; set;}
    public ShipSystem PatchTo  {get; set;}

    private float patchFromUpgrade = 0.5f;
    private float patchToDowngrade = 0.25f;

    public void Awake()
    {
        propulsionSystem.PowerLevel = 1f;
        weaponSystem.PowerLevel = 1f;
        shieldSystem.PowerLevel = 1f;
    }

    public bool PatchSelectedSystems()
    {
        if(!PatchFrom.isPatched)
        {
            PatchTo.PatchToSystem(patchFromUpgrade);

            PatchFrom.PatchFromSystem(PatchTo, patchToDowngrade);

            ClearSelectedSystems();

            return true;
        } else 
        {
            return false;
        }
    }

    public bool SetPatchToSystem(SystemType system)
    {
        switch(system)
        {
            case SystemType.Aux:
                if(PatchFrom != auxSystem)
                {
                    PatchTo = auxSystem;
                    return true;
                } else 
                {
                    return false;
                }
            case SystemType.Propulsion:
                if(PatchFrom != propulsionSystem)
                {
                    PatchTo = propulsionSystem;
                    return true;
                } else 
                {
                    return false;
                }
            case SystemType.Shield:
                if(PatchFrom != shieldSystem)
                {
                    PatchTo = shieldSystem;
                    return true;
                } else 
                {
                    return false;
                }
            case SystemType.Weapon:
                if(PatchFrom != weaponSystem)
                {
                    PatchTo = weaponSystem;
                    return true;
                } else 
                {
                    return false;
                }
            default:
                return false;
        }
    }

    public bool SetPatchFromSystem(SystemType system)
    {
        switch(system)
        {
            case SystemType.Weapon:
                PatchFrom = weaponSystem;
                return true;
            case SystemType.Propulsion:
                PatchFrom = propulsionSystem;
                return true;
            case SystemType.Shield:
                PatchFrom = shieldSystem;
                return true;
            default:
                return false;
        }
    }

    /**
    * Removes a specific systems patch using its reference to the system its patched to
    **/
    public bool RemoveSystemPatch(SystemType systemToRemove)
    {
        switch(systemToRemove)
        {
            case SystemType.Weapon:
                weaponSystem.PatchedToSystem.RemovePatchToSystem(patchFromUpgrade);
                weaponSystem.RemovePatchFromSystem(patchToDowngrade);
                return true;
            case SystemType.Propulsion:
                propulsionSystem.PatchedToSystem.RemovePatchToSystem(patchFromUpgrade);
                propulsionSystem.RemovePatchFromSystem(patchToDowngrade);
                return true;
            case SystemType.Shield:
                shieldSystem.PatchedToSystem.RemovePatchToSystem(patchFromUpgrade);
                shieldSystem.RemovePatchFromSystem(patchToDowngrade);
                return true;
            default:
                return false;
        }
    }

    public void ClearSelectedSystems()
    {
        PatchFrom = null;
        PatchTo = null;
    }
}
