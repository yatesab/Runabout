using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCore : CoreSystem
{
    public CoolingSystem coolingSystem;

    public float PropulsionPower { get { return propulsionSystem.PowerLevel; } }
    public bool PropulsionIsPatched { get; set; } = false;
    public SystemType PropulsionPatchSystem { get; set; }

    public float ShieldPower { get { return shieldSystem.PowerLevel; } }
    public bool ShieldIsPatched { get; set; } = false;
    public SystemType ShieldPatchSystem { get; set; }

    public float WeaponPower { get { return weaponSystem.PowerLevel; } }
    public bool WeaponIsPatched { get; set; } = false;
    public SystemType WeaponPatchSystem { get; set; }

    public SystemType PatchFrom { get; set; }
    public SystemType PatchTo { get; set; }

    private float patchFromUpgrade = 0.5f;
    private float patchToDowngrade = 0.25f;

    public void ClearSelectedSystems()
    {
        PatchFrom = SystemType.None;
        PatchTo = SystemType.None;
    }

    public bool SetPatchFromSystem(SystemType system)
    {
        PatchFrom = system;
        switch (system)
        {
            case SystemType.Propulsion:
                if (PropulsionIsPatched)
                {
                    return false;
                }
                else
                {
                    PatchFrom = system;
                    return true;
                }
            case SystemType.Shield:
                if (ShieldIsPatched)
                {
                    return false;
                }
                else
                {
                    PatchFrom = system;
                    return true;
                }
            case SystemType.Weapon:
                if (WeaponIsPatched)
                {
                    return false;
                }
                else
                {
                    PatchFrom = system;
                    return true;
                }
            case SystemType.None:
            default:
                return false;
        }
    }

    public void SetPatchToSystem(SystemType system)
    {
        PatchTo = system;
    }

    public void PatchSelectedSystems()
    {
        if(PatchTo != SystemType.None && PatchFrom != SystemType.None)
        {
            switch (PatchFrom)
            {
                case SystemType.Propulsion:
                    propulsionSystem.PatchFromSystem(patchToDowngrade);
                    PropulsionIsPatched = true;
                    PropulsionPatchSystem = PatchTo;
                    break;
                case SystemType.Shield:
                    shieldSystem.PatchFromSystem(patchToDowngrade);
                    ShieldIsPatched = true;
                    ShieldPatchSystem = PatchTo;
                    break;
                case SystemType.Weapon:
                    weaponSystem.PatchFromSystem(patchToDowngrade);
                    WeaponIsPatched = true;
                    WeaponPatchSystem = PatchTo;
                    break;
                default:
                    break;
            }

            switch (PatchTo)
            {
                case SystemType.Propulsion:
                    propulsionSystem.PatchToSystem(patchFromUpgrade);
                    break;
                case SystemType.Shield:
                    shieldSystem.PatchToSystem(patchFromUpgrade);
                    break;
                case SystemType.Weapon:
                    weaponSystem.PatchToSystem(patchFromUpgrade);
                    break;
                default:
                    break;
            }
        }
    }

    /**
    * Removes a specific systems patch using its reference to the system its patched to
    **/
    public void RemoveSystemPatch(SystemType systemToRemove)
    {
        SystemType patchToRemove = SystemType.None;

        switch(systemToRemove)
        {
            case SystemType.Propulsion:
                propulsionSystem.RemovePatchFromSystem(patchToDowngrade);
                PropulsionIsPatched = false;
                patchToRemove = PropulsionPatchSystem;
                PropulsionPatchSystem = SystemType.None;
                break;
            case SystemType.Shield:
                shieldSystem.RemovePatchFromSystem(patchToDowngrade);
                ShieldIsPatched = false;
                patchToRemove = ShieldPatchSystem;
                ShieldPatchSystem = SystemType.None;
                break;
            case SystemType.Weapon:
                weaponSystem.RemovePatchFromSystem(patchToDowngrade);
                WeaponIsPatched = false;
                patchToRemove = WeaponPatchSystem;
                WeaponPatchSystem = SystemType.None;
                break;
            case SystemType.None:
            default:
                break;
        }

        switch (patchToRemove)
        {
            case SystemType.Propulsion:
                propulsionSystem.RemovePatchToSystem(patchFromUpgrade);
                break;
            case SystemType.Shield:
                shieldSystem.RemovePatchToSystem(patchFromUpgrade);
                break;
            case SystemType.Weapon:
                weaponSystem.RemovePatchToSystem(patchFromUpgrade);
                break;
            case SystemType.None:
            default:
                break;
        }
    }
}
