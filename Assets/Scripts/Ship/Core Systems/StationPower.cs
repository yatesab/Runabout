using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationPower : MonoBehaviour
{

    public SubsystemPower subsystemOne;
    public SubsystemPower subsystemTwo;
    public SubsystemPower subsystemThree;

    public int AuxPower { get; set; } = 0;
    public int ExtraPower { get { return PatchedSystem == null ? 0 : PatchedSystem.AuxPower; } }
    public bool IsPatched { get; set; } = false;
    public StationPower PatchedSystem { get; set; } = null;
    public int powerLevelLimit = 8;

    public StationPowerBar auxBar;
    public StationPowerBar extraBar;

    public void UpdateAuxBars()
    {
        auxBar.UpdateBar(AuxPower);
    }

    public void UpdateExtraBars()
    {
        Debug.Log(ExtraPower);
        extraBar.UpdateBar(ExtraPower);
    }

    public bool RequestPower(int requestAmount)
    {
        if(!IsPatched && AuxPower > 0)
        {
            AuxPower -= requestAmount;

            UpdateAuxBars();
            return true;
        }

        return false;
    }

    public bool RequestExtraPower(int requestAmount)
    {
        int assignedExtraPower = subsystemOne.ExtraPower + subsystemTwo.ExtraPower + subsystemThree.ExtraPower;

        if (ExtraPower > 0 && assignedExtraPower < ExtraPower)
        {
            UpdateAuxBars();
            return true;
        }

        return false;
    }

    public bool AddPower(int suppliedAmount)
    {
        if(AuxPower < powerLevelLimit)
        {
            AuxPower += suppliedAmount;

            UpdateAuxBars();
            return true;
        }

        return false;
    }

    public void ClearExtraPower()
    {
        subsystemOne.ClearExtraPower();
        subsystemTwo.ClearExtraPower();
        subsystemThree.ClearExtraPower();
    }
}
