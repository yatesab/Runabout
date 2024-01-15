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
    [SerializeField] private Button propulsionRemoveButton;
    [SerializeField] private TMP_Text propulsionPowerLevel;

    [Header("Shields System")]
    [SerializeField] private Button shieldButton;
    [SerializeField] private Button shieldPatchButton;
    [SerializeField] private Button shieldRemoveButton;
    [SerializeField] private TMP_Text shieldPowerLevel;

    [Header("Weapons System")]
    [SerializeField] private Button weaponButton;
    [SerializeField] private Button weaponPatchButton;
    [SerializeField] private Button weaponRemoveButton;
    [SerializeField] private TMP_Text weaponPowerLevel;

    // Start is called before the first frame update
    void Start()
    {
        auxButton.onClick.AddListener(PatchToAuxSystem);

        propulsionButton.onClick.AddListener(PatchToPropulsionSystem);
        propulsionPatchButton.onClick.AddListener(PatchFromPropulsionSystem);
        propulsionRemoveButton.onClick.AddListener(RemovePropulsionPatch);

        shieldButton.onClick.AddListener(PatchToShieldSystem);
        shieldPatchButton.onClick.AddListener(PatchFromShieldSystem);
        shieldRemoveButton.onClick.AddListener(RemoveShieldPatch);

        weaponButton.onClick.AddListener(PatchToWeaponSystem);
        weaponPatchButton.onClick.AddListener(PatchFromWeaponSystem);
        weaponRemoveButton.onClick.AddListener(RemoveWeaponPatch);
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
        propulsionPatchButton.interactable  = !powerCore.PropulsionIsPatched;
        propulsionRemoveButton.interactable  = powerCore.PropulsionIsPatched;

        shieldPowerLevel.text = powerCore.ShieldPower.ToString();
        shieldButton.interactable  = powerCore.PatchFrom != null;
        shieldPatchButton.interactable  = !powerCore.ShieldIsPatched;
        shieldRemoveButton.interactable  = powerCore.ShieldIsPatched;

        weaponPowerLevel.text = powerCore.WeaponPower.ToString();
        weaponButton.interactable  = powerCore.PatchFrom != null;
        weaponPatchButton.interactable  = !powerCore.WeaponIsPatched;
        weaponRemoveButton.interactable  = powerCore.WeaponIsPatched;
    }

    /*
    * Aux System
    **/

    private void PatchToAuxSystem()
    {
        if(powerCore.SetPatchToSystem(PowerCore.SystemType.Aux))
        {
            // After setting patch to system try to patch systems together.
            if(powerCore.PatchSelectedSystems())
            {
                // Display Success in Patching systems
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

    /*
    * Propulsion System
    **/

    private void PatchToPropulsionSystem()
    {
        if(powerCore.SetPatchToSystem(PowerCore.SystemType.Propulsion))
        {
            // After setting patch to system try to patch systems together.
            if(powerCore.PatchSelectedSystems())
            {
                // Display Success in Patching systems
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

    private void PatchFromPropulsionSystem()
    {
        powerCore.SetPatchFromSystem(PowerCore.SystemType.Propulsion);
    }

    private void RemovePropulsionPatch()
    {
        powerCore.RemoveSystemPatch(PowerCore.SystemType.Propulsion);
    }

    /*
    * Shield System
    **/

    private void PatchToShieldSystem()
    {
        if(powerCore.SetPatchToSystem(PowerCore.SystemType.Shield))
        {
            // After setting patch to system try to patch systems together.
            if(powerCore.PatchSelectedSystems())
            {
                // Display Success in Patching systems
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

    private void PatchFromShieldSystem()
    {
        powerCore.SetPatchFromSystem(PowerCore.SystemType.Shield);
    }

    private void RemoveShieldPatch()
    {
        powerCore.RemoveSystemPatch(PowerCore.SystemType.Shield);
    }

    /*
    * Weapon System
    **/
    private void PatchToWeaponSystem()
    {
        if(powerCore.SetPatchToSystem(PowerCore.SystemType.Weapon))
        {
            // After setting patch to system try to patch systems together.
            if(powerCore.PatchSelectedSystems())
            {
                // Display Success in Patching systems
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

    private void PatchFromWeaponSystem()
    {
        powerCore.SetPatchFromSystem(PowerCore.SystemType.Weapon);
    }

    private void RemoveWeaponPatch()
    {
        powerCore.RemoveSystemPatch(PowerCore.SystemType.Weapon);
    }
}
