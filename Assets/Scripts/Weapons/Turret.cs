using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform meshOffset;
    [SerializeField] private Transform muzzle;
    [SerializeField] private LaserTurret laserTurret;

    public Vector3 TargetPosition { get; set; }

    public void Update()
    {
        // Look at current target
        meshOffset.LookAt(TargetPosition);
    }

    public void FireSelectedWeapon(int selectedWeaponGroup, int selectedWeaponType, LayerMask layerMask)
    {
        switch(selectedWeaponGroup)
        {
            case 0:
                laserTurret.FireWeapon(selectedWeaponType, layerMask, muzzle);
                break;
        }
    }

    public void StopSelectedWeapon(int selectedWeaponGroup, int selectedWeaponType)
    {
        switch (selectedWeaponGroup)
        {
            case 0:
                laserTurret.StopFireWeapon(selectedWeaponType, muzzle);
                break;
        }
    }

    public float GetMaxDistance(int selectedWeaponGroup)
    {
        switch (selectedWeaponGroup)
        {
            case 0:
            default:
                return laserTurret.MaxDistance;
        }
    }
}
