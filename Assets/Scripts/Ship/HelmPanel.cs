using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelmPanel : MonoBehaviour
{
    public Throttle throttleLeft;
    public TMP_Text leftPercentage;

    public Throttle throttleRight;
    public TMP_Text rightPercentage;

    public PropulsionSystem propulsionSystem;
    public TMP_Text heatLevel;
    public TMP_Text overheated;

    public PowerCore powerCore;
    public TMP_Text propulsionPowerLevel;
    public TMP_Text weaponsPowerLevel;
    public TMP_Text shieldPowerLevel;
    public TMP_Text auxPowerLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        propulsionPowerLevel.text = powerCore.PropulsionPower.ToString();
        shieldPowerLevel.text = powerCore.ShieldPower.ToString();
        weaponsPowerLevel.text = powerCore.WeaponPower.ToString();

        leftPercentage.text = Mathf.Round(throttleLeft.ThrottlePercentage * 100).ToString() + " %";
        rightPercentage.text = Mathf.Round(throttleRight.ThrottlePercentage * 100).ToString() + " %";
        heatLevel.text = Mathf.Round(propulsionSystem.HeatLevel).ToString();
        overheated.text = "Overheated: " + propulsionSystem.isOverheated.ToString();
    }
}
