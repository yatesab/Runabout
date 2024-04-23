using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    public float WeaponPower { get; set; } = 4f;
    public float WeaponTotalPower { get; set; }

    public float EnginePower { get; set; } = 4f;
    public float EngineTotalPower { get; set; }

    public float ShieldPower { get; set; } = 4f;
    public float ShieldTotalPower { get; set; }

    protected float weaponExtraPower = 0f;
    protected int weaponPatchedSystems = 0;

    protected float engineExtraPower = 0f;
    protected int enginePatchedSystems = 0;

    protected float shieldExtraPower = 0f;
    protected int shieldPatchedSystems = 0;

    protected float dialOneSetting = 0f;
    protected float dialTwoSetting = 0f;
    protected float dialThreeSetting = 0f;

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

    protected void UpdateAllPowerLevels()
    {
        WeaponTotalPower = GetPowerLevel(WeaponPower, dialTwoSetting, engineExtraPower, enginePatchedSystems, dialThreeSetting, shieldExtraPower, shieldPatchedSystems);
        EngineTotalPower = GetPowerLevel(EnginePower, dialOneSetting, shieldExtraPower, shieldPatchedSystems, dialTwoSetting, weaponExtraPower, weaponPatchedSystems);
        ShieldTotalPower = GetPowerLevel(ShieldPower, dialThreeSetting, weaponExtraPower, weaponPatchedSystems, dialOneSetting, engineExtraPower, enginePatchedSystems);
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

        UpdateAllPowerLevels();
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

        UpdateAllPowerLevels();
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

        UpdateAllPowerLevels();
    }

    public void ChangeEnginePowerLevel(float newEnginePowerLevel)
    {
        EnginePower = newEnginePowerLevel;
        engineExtraPower = 4f - newEnginePowerLevel;

        UpdateAllPowerLevels();
    }

    public void ChangeWeaponPowerLevel(float newWeaponPowerLevel)
    {
        WeaponPower = newWeaponPowerLevel;
        weaponExtraPower = 4f - newWeaponPowerLevel;

        UpdateAllPowerLevels();
    }

    public void ChangeShieldPowerLevel(float newShieldPowerLevel)
    {
        ShieldPower = newShieldPowerLevel;
        shieldExtraPower = 4f - newShieldPowerLevel;

        UpdateAllPowerLevels();
    }

    public void ShutOffPower()
    {
        EngineTotalPower = 0f;
        ShieldTotalPower = 0f;
        WeaponTotalPower = 0f;
    }

    public void TurnOnPower()
    {
        UpdateAllPowerLevels();
    }
}
