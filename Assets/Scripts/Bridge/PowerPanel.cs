using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerPanel : MonoBehaviour
{
    [Header("Power Core")]
    [SerializeField] private PowerCore powerCore;

    [Header("Propulsion System")]
    [SerializeField] private Button propulsionButton;
    [SerializeField] private Button propulsionPatchButton;
    [SerializeField] private TMP_Text propulsionPowerLevel;
    [SerializeField] private TMP_Text propulsionPatchButtonText;

    [Header("Shields System")]
    [SerializeField] private Button shieldButton;
    [SerializeField] private Button shieldPatchButton;
    [SerializeField] private TMP_Text shieldPowerLevel;
    [SerializeField] private TMP_Text shieldPatchButtonText;

    [Header("Weapons System")]
    [SerializeField] private Button weaponButton;
    [SerializeField] private Button weaponPatchButton;
    [SerializeField] private TMP_Text weaponPowerLevel;
    [SerializeField] private TMP_Text weaponPatchButtonText;

    private TMP_Text selectedPatchButton;

    // Start is called before the first frame update
    void Start()
    {
        propulsionButton.onClick.AddListener(PatchToPropulsionSystem);
        propulsionPatchButton.onClick.AddListener(PatchFromPropulsionSystem);

        shieldButton.onClick.AddListener(PatchToShieldSystem);
        shieldPatchButton.onClick.AddListener(PatchFromShieldSystem);

        weaponButton.onClick.AddListener(PatchToWeaponSystem);
        weaponPatchButton.onClick.AddListener(PatchFromWeaponSystem);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScreen();
    }

    public void UpdateScreen()
    {
        propulsionPowerLevel.text = powerCore.PropulsionPower.ToString();
        propulsionButton.interactable  = powerCore.PatchFrom != PowerCore.SystemType.None;

        shieldPowerLevel.text = powerCore.ShieldPower.ToString();
        shieldButton.interactable  = powerCore.PatchFrom != PowerCore.SystemType.None;

        weaponPowerLevel.text = powerCore.WeaponPower.ToString();
        weaponButton.interactable  = powerCore.PatchFrom != PowerCore.SystemType.None;
    }

    private void PatchSystems(PowerCore.SystemType systemType)
    {
        powerCore.SetPatchToSystem(systemType);
        powerCore.PatchSelectedSystems();

        // Display Success in Patching systems
        AudioManager.instance.Play("Button Success");

        selectedPatchButton.text = "Remove Patch";
        selectedPatchButton = null;
    }

    private void PatchFrom(PowerCore.SystemType systemType, bool systemIsPatched, TMP_Text buttonText)
    {
        if (systemIsPatched)
        {
            powerCore.RemoveSystemPatch(systemType);
            AudioManager.instance.Play("Button Success");
            buttonText.text = "Patch";
        }
        else
        {
            if(powerCore.SetPatchFromSystem(systemType))
            {
                AudioManager.instance.Play("Button Select");
                buttonText.text = "Patching To";
                selectedPatchButton = buttonText;
            }
        }
    }

    /*
    * Propulsion System
    **/

    private void PatchToPropulsionSystem()
    {
        PatchSystems(PowerCore.SystemType.Propulsion);
    }

    private void PatchFromPropulsionSystem()
    {
        PatchFrom(PowerCore.SystemType.Propulsion, powerCore.PropulsionIsPatched, propulsionPatchButtonText);
    }

    /*
    * Shield System
    **/

    private void PatchToShieldSystem()
    {
        PatchSystems(PowerCore.SystemType.Shield);
    }

    private void PatchFromShieldSystem()
    {
        PatchFrom(PowerCore.SystemType.Shield, powerCore.ShieldIsPatched, shieldPatchButtonText);
    }

    /*
    * Weapon System
    **/
    private void PatchToWeaponSystem()
    {
        PatchSystems(PowerCore.SystemType.Weapon); 
    }

    private void PatchFromWeaponSystem()
    {
        PatchFrom(PowerCore.SystemType.Weapon, powerCore.WeaponIsPatched, weaponPatchButtonText);
    }
}
