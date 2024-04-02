using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    protected float weaponPowerLevel = 4f;
    protected float weaponExtraPower = 0f;
    protected int weaponPatchedSystems = 0;

    protected float enginePowerLevel = 4f;
    protected float engineExtraPower = 0f;
    protected int enginePatchedSystems = 0;

    protected float shieldPowerLevel = 4f;
    protected float shieldExtraPower = 0f;
    protected int shieldPatchedSystems = 0;

    protected float dialOneSetting = 0f;
    protected float dialTwoSetting = 0f;
    protected float dialThreeSetting = 0f;

    public void Update()
    {
        Debug.Log("Weapon Power: " + WeaponPower);
        Debug.Log("Engine Power: " + EnginePower);
        Debug.Log("Shield Power: " + ShieldPower);
    }

    public float WeaponPower
    {
        get { 
            return GetPowerLevel(weaponPowerLevel, dialTwoSetting, engineExtraPower, enginePatchedSystems, dialThreeSetting, shieldExtraPower, shieldPatchedSystems); 
        }
    }

    public float EnginePower
    {
        get
        {
            return GetPowerLevel(enginePowerLevel, dialOneSetting, shieldExtraPower, shieldPatchedSystems, dialTwoSetting, weaponExtraPower, weaponPatchedSystems);
        }
    }

    public float ShieldPower
    {
        get
        {
            return GetPowerLevel(shieldPowerLevel, dialOneSetting, engineExtraPower, enginePatchedSystems, dialThreeSetting, weaponExtraPower, weaponPatchedSystems);
        }
    }

    public float GetPowerLevel(float startingPowerLevel, float dialOne, float extraPowerOne, float patchedSystemsOne, float dialTwo, float extraPowerTwo, float patchedSystemTwo)
    {
        float power = startingPowerLevel;

        if (dialOne == -1 && patchedSystemsOne != 0)
        {
            power += extraPowerOne;
            power /= patchedSystemsOne;
        }

        if (dialTwo == 1 && patchedSystemTwo != 0)
        {
            power += extraPowerTwo;
            power /= patchedSystemTwo;
        }

        return power;
    }

    public void ChangeDialOne(float newDialSetting)
    {
        dialOneSetting = newDialSetting;

        switch(newDialSetting)
        {
            case -1:
                shieldPatchedSystems += 1;
                break;
            case 1:
                enginePatchedSystems += 1;
                break;
            default:
            case 0:
                shieldPatchedSystems = 0;
                enginePatchedSystems = 0;
                break;
        }
    }

    public void ChangeDialTwo(float newDialSetting)
    {
        dialTwoSetting = newDialSetting;

        switch (newDialSetting)
        {
            case -1:
                enginePatchedSystems += 1;
                break;
            case 1:
                weaponPatchedSystems += 1;
                break;
            default:
            case 0:
                weaponPatchedSystems = 0;
                enginePatchedSystems = 0;
                break;
        }
    }

    public void ChangeDialThree(float newDialSetting)
    {
        dialThreeSetting = newDialSetting;

        switch (newDialSetting)
        {
            case -1:
                weaponPatchedSystems += 1;
                break;
            case 1:
                shieldPatchedSystems += 1;
                break;
            default:
            case 0:
                shieldPatchedSystems = 0;
                weaponPatchedSystems = 0;
                break;
        }
    }

    public void ChangeEnginePowerLevel(float newEnginePowerLevel)
    {
        enginePowerLevel = newEnginePowerLevel;
        engineExtraPower = 4f - enginePowerLevel;
    }

    public void ChangeWeaponPowerLevel(float newWeaponPowerLevel)
    {
        weaponPowerLevel = newWeaponPowerLevel;
        weaponExtraPower = 4f - weaponPowerLevel;
    }

    public void ChangeShieldPowerLevel(float newShieldPowerLevel)
    {
        shieldPowerLevel = newShieldPowerLevel;
        shieldExtraPower = 4f - shieldPowerLevel;
    }
}
