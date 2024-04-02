using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCore : CoreSystem
{
    public StationPower propulsionPower;

    public StationPower shieldPower;

    public StationPower weaponPower;

    public SystemType PatchFrom { get; set; }
    public SystemType PatchTo { get; set; }

    public SystemType DialOneLeft;
    public SystemType DialOneRight;
    public SystemType DialTwoLeft;
    public SystemType DialTwoRight;
    public SystemType DialThreeLeft;
    public SystemType DialThreeRight;

    public StationPower GetPowerSystem(SystemType system)
    {
        switch(system)
        {
            case SystemType.Propulsion:
                return propulsionPower;
            case SystemType.Shield:
                return shieldPower;
            case SystemType.Weapon:
            default:
                return weaponPower;
        }
    }

    public void PatchSystemAuxPower(SystemType patchFrom, SystemType patchTo)
    {
        StationPower patchFromSystem = GetPowerSystem(patchFrom);
        StationPower patchToSystem = GetPowerSystem(patchTo);

        patchFromSystem.IsPatched = true;
        patchToSystem.PatchedSystem = patchFromSystem;
        patchToSystem.UpdateExtraBars();
    }

    public void RemoveSystemAuxPatch(SystemType dialLeft, SystemType dialRight)
    {
        StationPower leftSystem = GetPowerSystem(dialLeft);
        StationPower rightSystem = GetPowerSystem(dialRight);

        if(leftSystem.IsPatched)
        {
            leftSystem.IsPatched = false;
            rightSystem.PatchedSystem = null;
            rightSystem.UpdateExtraBars();
        } else
        {
            rightSystem.IsPatched = false;
            leftSystem.PatchedSystem = null;
            leftSystem.UpdateExtraBars();
        }
    }

    public void HandleDialOneUpdate(float selection)
    {
        if (selection == -1)
        {
            PatchSystemAuxPower(DialOneRight, DialOneLeft);
        }
        else if (selection == 1)
        {
            PatchSystemAuxPower(DialOneLeft, DialOneRight);
        }
        else
        {
            RemoveSystemAuxPatch(DialOneLeft, DialOneRight);
        }
    }

    public void HandleDialOTwoUpdate(float selection)
    {
        if (selection == -1)
        {
            PatchSystemAuxPower(DialTwoRight, DialTwoLeft);
        }
        else if (selection == 1)
        {
            PatchSystemAuxPower(DialTwoLeft, DialTwoRight);
        }
        else
        {
            RemoveSystemAuxPatch(DialTwoLeft, DialTwoRight);
        }
    }
    public void HandleDialOThreeUpdate(float selection)
    {
        if (selection == -1)
        {
            PatchSystemAuxPower(DialThreeRight, DialThreeLeft);

        }
        else if (selection == 1)
        {
            PatchSystemAuxPower(DialThreeLeft, DialThreeRight);

        }
        else
        {
            RemoveSystemAuxPatch(DialThreeLeft, DialThreeRight);
        }
    }
}
