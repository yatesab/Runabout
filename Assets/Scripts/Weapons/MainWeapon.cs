using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MainWeapon : WeaponControl
{
    [SerializeField] private Turret portTurret;
    [SerializeField] private Turret starboardTurret;
    [SerializeField] private PowerSystem powerSystem;

    public bool TriggerActivated { get; set; }

    // Update is called once per frame
    public void Update()
    {
        GetTargetHitInfo(portTurret.GetMaxDistance(selectedWeapon));

        UpdateTurretPosition();

        if (powerSystem.WeaponTotalPower > 0 && TriggerActivated)
        {

            portTurret.FireSelectedWeapon(selectedWeapon, selectedWeaponType, layerMask);
            starboardTurret.FireSelectedWeapon(selectedWeapon, selectedWeaponType, layerMask);
        }
    }

    private void UpdateTurretPosition()
    {
        portTurret.TargetPosition = predictionPoint.transform.position;
        starboardTurret.TargetPosition = predictionPoint.transform.position;
    }

    public void StartFireingWeapon(ActivateEventArgs args)
    {
        TriggerActivated = true;
    }

    public void StopFiringWeapon(DeactivateEventArgs args)
    {
        TriggerActivated = false;

        portTurret.StopSelectedWeapon(selectedWeapon, selectedWeaponType);
        starboardTurret.StopSelectedWeapon(selectedWeapon, selectedWeaponType);
    }
}
