using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCore : CoreSystem
{
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
                    propulsionSystem.RemovePower(patchToDowngrade);
                    PropulsionIsPatched = true;
                    PropulsionPatchSystem = PatchTo;
                    break;
                case SystemType.Shield:
                    shieldSystem.RemovePower(patchToDowngrade);
                    ShieldIsPatched = true;
                    ShieldPatchSystem = PatchTo;
                    break;
                case SystemType.Weapon:
                    weaponSystem.RemovePower(patchToDowngrade);
                    WeaponIsPatched = true;
                    WeaponPatchSystem = PatchTo;
                    break;
                default:
                    break;
            }

            switch (PatchTo)
            {
                case SystemType.Propulsion:
                    propulsionSystem.AddPower(patchFromUpgrade);
                    break;
                case SystemType.Shield:
                    shieldSystem.AddPower(patchFromUpgrade);
                    break;
                case SystemType.Weapon:
                    weaponSystem.AddPower(patchFromUpgrade);
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
                if (PropulsionIsPatched)
                {
                    propulsionSystem.AddPower(patchToDowngrade);
                    PropulsionIsPatched = false;
                    patchToRemove = PropulsionPatchSystem;
                    PropulsionPatchSystem = SystemType.None;
                }
                break;
            case SystemType.Shield:
                if (ShieldIsPatched)
                {
                    shieldSystem.AddPower(patchToDowngrade);
                    ShieldIsPatched = false;
                    patchToRemove = ShieldPatchSystem;
                    ShieldPatchSystem = SystemType.None;
                }
                break;
            case SystemType.Weapon:
                if (WeaponIsPatched)
                {
                    weaponSystem.AddPower(patchToDowngrade);
                    WeaponIsPatched = false;
                    patchToRemove = WeaponPatchSystem;
                    WeaponPatchSystem = SystemType.None;
                }
                break;
            case SystemType.None:
            default:
                break;
        }

        switch (patchToRemove)
        {
            case SystemType.Propulsion:
                propulsionSystem.RemovePower(patchFromUpgrade);
                break;
            case SystemType.Shield:
                shieldSystem.RemovePower(patchFromUpgrade);
                break;
            case SystemType.Weapon:
                weaponSystem.RemovePower(patchFromUpgrade);
                break;
            case SystemType.None:
            default:
                break;
        }
    }
}
