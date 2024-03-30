using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : WeaponControl
{
    [SerializeField] protected TorpedoLauncher torpedoLauncher;
    [SerializeField] protected BombLauncher bombLauncher;

    public void Update()
    {
        switch (selectedWeapon)
        {
            case 0:
                GetTargetHitInfo(torpedoLauncher.MaxDistance);
                break;
            case 1:
                break;
        }
    }

    public void FireSecondaryWeapon()
    {
        switch(selectedWeapon)
        {
            case 0:
                torpedoLauncher.FireWeapon(selectedWeaponType, predictionPoint.transform.position);
                break;
            case 1:
                bombLauncher.FireWeapon(selectedWeaponType);
                break;
        }
    }

}
