using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerControls : MonoBehaviour
{
    [Header("Power Core")]
    [SerializeField] private PowerCore powerCore;
    [SerializeField] private TMP_Text propulsionPowerLevel;
    [SerializeField] private TMP_Text shieldPowerLevel;
    [SerializeField] private TMP_Text weaponPowerLevel;

    public PowerCore.SystemType DialOneLeft;
    public PowerCore.SystemType DialOneRight;
    public PowerCore.SystemType DialTwoLeft;
    public PowerCore.SystemType DialTwoRight;
    public PowerCore.SystemType DialThreeLeft;
    public PowerCore.SystemType DialThreeRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScreen();
    }

    public void UpdateScreen()
    {
        propulsionPowerLevel.text = powerCore.PropulsionPower.ToString();

        shieldPowerLevel.text = powerCore.ShieldPower.ToString();

        weaponPowerLevel.text = powerCore.WeaponPower.ToString();
    }

    public void HandleDialOneUpdate(int selection)
    {
        if(selection == -1)
        {
            powerCore.SetPatchFromSystem(DialOneRight);
            powerCore.SetPatchToSystem(DialOneLeft);
            powerCore.PatchSelectedSystems();
        } else if(selection == 1)
        {
            powerCore.SetPatchFromSystem(DialOneLeft);
            powerCore.SetPatchToSystem(DialOneRight);
            powerCore.PatchSelectedSystems();
        } else
        {
            powerCore.RemoveSystemPatch(DialOneLeft);
            powerCore.RemoveSystemPatch(DialOneRight);
        }
    }

    public void HandleDialOTwoUpdate(int selection)
    {
        if (selection == -1)
        {
            powerCore.SetPatchFromSystem(DialTwoRight);
            powerCore.SetPatchToSystem(DialTwoLeft);
            powerCore.PatchSelectedSystems();
        }
        else if (selection == 1)
        {
            powerCore.SetPatchFromSystem(DialTwoLeft);
            powerCore.SetPatchToSystem(DialTwoRight);
            powerCore.PatchSelectedSystems();
        }
        else
        {
            powerCore.RemoveSystemPatch(DialTwoLeft);
            powerCore.RemoveSystemPatch(DialTwoRight);
        }
    }
    public void HandleDialOThreeUpdate(int selection)
    {
        if (selection == -1)
        {
            powerCore.SetPatchFromSystem(DialThreeRight);
            powerCore.SetPatchToSystem(DialThreeLeft);
            powerCore.PatchSelectedSystems();
        }
        else if (selection == 1)
        {
            powerCore.SetPatchFromSystem(DialThreeLeft);
            powerCore.SetPatchToSystem(DialThreeRight);
            powerCore.PatchSelectedSystems();
        }
        else
        {
            powerCore.RemoveSystemPatch(DialThreeLeft);
            powerCore.RemoveSystemPatch(DialThreeRight);
        }
    }
}
