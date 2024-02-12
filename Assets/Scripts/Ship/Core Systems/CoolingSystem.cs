using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolingSystem : CoreSystem
{
    public float waterTemperature = 0f;

    public float dangerTemperature = 20.0f;

    public float criticalTemperature = 30.0f;

    public Lever propulsionLever;
    public Lever shieldLever;
    public Lever weaponLever;

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

    public float ShieldHeatLevel { get; set; } = 0f;
    public bool ShieldIsOverheated { get; set; } = false;

    public float WeaponHeatLevel {  get; set; } = 0f;
    public bool WeaponIsOverheated { get; set; } = false;

    // Update is called once per frame
    void Update()
    {
        CoolSystems();
        UpdateCoolingMode();
    }

    private float GetHeatRemovalPercentage()
    {
        float heatRemovalPercentage = 1f;

        if (waterTemperatureMode == WaterTemperatureMode.Danger)
        {
            heatRemovalPercentage -= 0.5f;
        }
        else if (waterTemperatureMode == WaterTemperatureMode.Critical)
        {
            heatRemovalPercentage -= 0.8f;
        }

        return heatRemovalPercentage;
    }

    private float GetHeatRemoval(StationSystem station, float valvePercentage)
    {
        float heatRemovalPercentage = GetHeatRemovalPercentage();

        float heatValue = Time.deltaTime;
        heatValue *= 1f + valvePercentage;

        return heatValue * heatRemovalPercentage;
    }

    private void CoolSystems()
    {
        // Propulsion heating and cooling
        if (propulsionSystem.HeatLevel > 0f)
        {
            float heat = GetHeatRemoval(propulsionSystem, propulsionLever.leverPercentage);

            propulsionSystem.RemoveHeat(heat);
            waterTemperature += heat;
        }

        // Weapon heating and cooling
        if (weaponSystem.HeatLevel > 0f)
        {
            float heat = GetHeatRemoval(weaponSystem, weaponLever.leverPercentage);

            weaponSystem.RemoveHeat(heat);
            waterTemperature += heat;
        }

        // Shield heating and cooling
        if (shieldSystem.HeatLevel > 0f)
        {
            float heat = GetHeatRemoval(shieldSystem, shieldLever.leverPercentage);

            shieldSystem.RemoveHeat(heat);
            waterTemperature += heat;
        }
    }

    private void UpdateCoolingMode()
    {
        if (waterTemperature > 0f)
        {
            waterTemperature -= Time.deltaTime;
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
}
