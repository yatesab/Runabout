using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : WeaponControl
{
    [SerializeField] private Turret portTurret;
    [SerializeField] private Turret starboardTurret;

    public bool TriggerActivated { get; set; }

    // Update is called once per frame
    public void Update()
    {
        GetTargetHitInfo(portTurret.GetMaxDistance(selectedWeapon));

        UpdateTurretPosition();

        if (TriggerActivated)
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

    public void StopFiringWeapon()
    {
        portTurret.StopSelectedWeapon(selectedWeapon, selectedWeaponType);
        starboardTurret.StopSelectedWeapon(selectedWeapon, selectedWeaponType);
    }
}
