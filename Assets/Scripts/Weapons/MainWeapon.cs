using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : WeaponControl
{
    [SerializeField] private Turret portTurret;
    [SerializeField] private Turret starboardTurret;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float maxDistance = portTurret.weaponList[portTurret.selectedWeapon].MaxDistance;

        GetTargetHitInfo(maxDistance);

        if (TriggerActivated)
        {
            portTurret.FireSelectedWeapon(layerMask);
            starboardTurret.FireSelectedWeapon(layerMask);
        }
    }

    protected override void UpdateTargetPosition(Vector3 newTargetPosition)
    {
        portTurret.TargetPosition = newTargetPosition;
        starboardTurret.TargetPosition = newTargetPosition;
    }

    public override void StopFiringWeapon()
    {
        portTurret.StopSelectedWeapon();
        starboardTurret.StopSelectedWeapon();
    }
}
