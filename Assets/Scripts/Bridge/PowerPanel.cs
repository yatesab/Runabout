using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerPanel : MonoBehaviour
{
    [Header("Power Core")]
    [SerializeField] private PowerCore powerCore;

    [Header("Aux System")]
    [SerializeField] private Button auxButton;
    [SerializeField] private TMP_Text auxPowerLevel;

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
        auxButton.onClick.AddListener(PatchToAuxSystem);

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
        auxPowerLevel.text = powerCore.AuxPower.ToString();
        auxButton.interactable  = powerCore.PatchFrom != null;

        propulsionPowerLevel.text = powerCore.PropulsionPower.ToString();
        propulsionButton.interactable  = powerCore.PatchFrom != null;

        shieldPowerLevel.text = powerCore.ShieldPower.ToString();
        shieldButton.interactable  = powerCore.PatchFrom != null;

        weaponPowerLevel.text = powerCore.WeaponPower.ToString();
        weaponButton.interactable  = powerCore.PatchFrom != null;
    }

    private void PatchSystems(PowerCore.SystemType systemType)
    {
        Debug.Log(systemType.ToString());
        if (powerCore.SetPatchToSystem(systemType))
        {
            // After setting patch to system try to patch systems together.
            if (powerCore.PatchSelectedSystems())
            {
                // Display Success in Patching systems
                AudioManager.instance.Play("Button Success");

                selectedPatchButton.text = "Remove Patch";
                selectedPatchButton = null;
            }
            else
            {
                powerCore.ClearSelectedSystems();

                // Display Error with color or message
                Debug.Log("Error with Patching Systems Together");
            }
        }
        else
        {
            powerCore.ClearSelectedSystems();

            // Display Error with color or message
            Debug.Log("Error with Patch To System");
        }

    }

    private void PatchFrom(PowerCore.SystemType systemType, bool systemIsPatched, TMP_Text buttonText)
    {
        Debug.Log(systemType.ToString());

        if (systemIsPatched)
        {
            powerCore.RemoveSystemPatch(systemType);
            AudioManager.instance.Play("Button Success");
            buttonText.text = "Patch";
        }
        else
        {
            powerCore.SetPatchFromSystem(systemType);
            AudioManager.instance.Play("Button Select");
            buttonText.text = "Patching To";
            selectedPatchButton = buttonText;
        }
    }

    /*
    * Aux System
    **/

    private void PatchToAuxSystem()
    {
        PatchSystems(PowerCore.SystemType.Aux);
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
