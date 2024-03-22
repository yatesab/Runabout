using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubsystemPower : MonoBehaviour
{
    public int CurrentPower = 4;
    public int ExtraPower { get; set; } = 0;
    public int PowerLevel { get { return CurrentPower + ExtraPower; } }

    private StationPower stationPower;
    private int _powerChangeAmount = 1;
    public int powerLevelLimit = 8;

    // Start is called before the first frame update
    void Start()
    {
        stationPower = GetComponentInParent<StationPower>();
    }

    public bool AddToPower()
    {
        if (CurrentPower < powerLevelLimit)
        {
            if (stationPower.RequestPower(_powerChangeAmount))
            {
                CurrentPower += _powerChangeAmount;
                return true;
            }
            else if (stationPower.RequestExtraPower(_powerChangeAmount))
            {
                ExtraPower += _powerChangeAmount;

                return true;
            }
        }

        return false;
    }

    public bool RemoveFromPower()
    {
        if(CurrentPower > 0)
        {
            if (ExtraPower > 0)
            {
                ExtraPower -= _powerChangeAmount;
                return true;
            }
            else if (stationPower.AddPower(_powerChangeAmount))
            {
                CurrentPower -= _powerChangeAmount;

                return true;
            }
        }

        return false;
    }

    public void ClearExtraPower()
    {
        ExtraPower = 0;
    }
}
