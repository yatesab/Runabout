using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolingSystem : CoreSystem
{
    public float waterTemperature = 0f;

    public float dangerTemperature = 20.0f;

    public float criticalTemperature = 30.0f;

    public enum WaterTemperatureMode
    {
        Cool,
        Normal,
        Danger,
        Critical
    }
    public WaterTemperatureMode waterTemperatureMode;

    public float PropulsionHeatLevel { get; set; } = 0f;
    public bool PropulsionIsOverheated { get; set; } = false;
    public bool _propulsionValveOpen = false;

    public float ShieldHeatLevel { get; set; } = 0f;
    public bool ShieldIsOverheated { get; set; } = false;
    public bool _shieldValveOpen = false;

    public float WeaponHeatLevel {  get; set; } = 0f;
    public bool WeaponIsOverheated { get; set; } = false;
    public bool _weaponValveOpen = false;

    // Update is called once per frame
    void Update()
    {
        UpdateCoolingMode();

        CoolSystems();
    }

    private float GetHeatRemoval(StationSystem station, bool stationValve)
    {
        float heatRemoval = 1f;

        if (waterTemperatureMode == WaterTemperatureMode.Danger)
        {
            heatRemoval -= 0.5f;
        } else if (waterTemperatureMode == WaterTemperatureMode.Critical)
        {
            heatRemoval -= 0.8f;
        }

        if (!stationValve)
        {
            // Lower Heat Removal
            heatRemoval = heatRemoval / 2f;
        }

        return heatRemoval;
    }

    private void CoolSystems()
    {
        // Propulsion heating and cooling
        if (!propulsionSystem.isHeating && propulsionSystem.HeatLevel > 0f)
        {
            propulsionSystem.RemoveHeat(Time.deltaTime * GetHeatRemoval(propulsionSystem, _propulsionValveOpen));
        }

        // Weapon heating and cooling
        if (!weaponSystem.isHeating && weaponSystem.HeatLevel > 0f)
        {
            weaponSystem.RemoveHeat(Time.deltaTime * GetHeatRemoval(weaponSystem, _weaponValveOpen));
        }

        // Shield heating and cooling
        if (!shieldSystem.isHeating && shieldSystem.HeatLevel > 0f)
        {
            shieldSystem.RemoveHeat(Time.deltaTime * GetHeatRemoval(shieldSystem, _shieldValveOpen));
        }
    }

    private void UpdateCoolingMode()
    {
        if(!_propulsionValveOpen || !_shieldValveOpen || !_weaponValveOpen)
        {
            waterTemperature -= Time.deltaTime;
        } else
        {
            // For each system valve open we add extra heat to the system.
            // If this reatches a certain level then all systems get less cooling.
            if (_propulsionValveOpen)
            {
                waterTemperature += Time.deltaTime;
            }
            if (_shieldValveOpen)
            {
                waterTemperature += Time.deltaTime;
            }
            if (_weaponValveOpen)
            {
                waterTemperature += Time.deltaTime;
            }
        }


        // If no valves open then cool water
        
        if (waterTemperature >= criticalTemperature)
        {
            waterTemperatureMode = WaterTemperatureMode.Critical;
        }
        else if (waterTemperature >= dangerTemperature)
        {
            waterTemperatureMode = WaterTemperatureMode.Danger;
        }
        else if (waterTemperatureMode != WaterTemperatureMode.Normal)
        { 
            waterTemperatureMode = WaterTemperatureMode.Normal; 
        }
    }

    public void OpenSystemValve(SystemType systemType)
    {
        switch (systemType)
        {
            case SystemType.Propulsion:
                _propulsionValveOpen = true;
                break;
            case SystemType.Shield:
                _shieldValveOpen = true; 
                break;
            case SystemType.Weapon:
                _weaponValveOpen = true; 
                break;
            default:
                break;
        }
    }
}
