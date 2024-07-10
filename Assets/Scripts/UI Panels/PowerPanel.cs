using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerPanel : MonoBehaviour
{
    public PowerSystem powerSystem;

    [Header("Extra Power")]
    public TMP_Text EngineTotalPower;
    public TMP_Text WeaponsTotalPower;
    public TMP_Text ShieldTotalPower;

    [Header("Power Sliders")]
    public TMP_Text EnginePower;
    public TMP_Text WeaponsPower;
    public TMP_Text ShieldPower;

    public void Start()
    {
        powerSystem.onPowerLevelChange += UpdatePowerLevels;
    }

    public void UpdatePowerLevels()
    {
        EngineTotalPower.text = powerSystem.EngineTotalPower.ToString();
        EnginePower.text = powerSystem.EnginePower.ToString();

        WeaponsTotalPower.text = powerSystem.WeaponTotalPower.ToString();
        WeaponsPower.text = powerSystem.WeaponPower.ToString();

        ShieldTotalPower.text = powerSystem.ShieldTotalPower.ToString();
        ShieldPower.text = powerSystem.ShieldPower.ToString();
    }
}
