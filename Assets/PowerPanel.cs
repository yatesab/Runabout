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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EngineTotalPower.text = powerSystem.EngineTotalPower.ToString();
        WeaponsTotalPower.text = powerSystem.WeaponTotalPower.ToString();
        ShieldTotalPower.text = powerSystem.ShieldTotalPower.ToString();

        EnginePower.text = powerSystem.EnginePower.ToString();
        WeaponsPower.text = powerSystem.WeaponPower.ToString();
        ShieldPower.text = powerSystem.ShieldPower.ToString();
    }
}
