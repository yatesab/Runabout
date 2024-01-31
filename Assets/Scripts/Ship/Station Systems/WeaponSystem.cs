using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : StationSystem
{
    public Weapon leftWeapon;
    public Weapon rightWeapon;

    public void FireLeftWeapon()
    {
        //Launch projectile based on weapon choice
        leftWeapon.FireWeapon();
    }

    public void StopLeftWeapon()
    {
        leftWeapon.StopFireWeapon();
    }

    public void FireRightWeapon()
    {
        //Launch projectile based on weapon choice
        rightWeapon.FireWeapon();
    }

    public void StopRightWeapon()
    {
        rightWeapon.StopFireWeapon();
    }
}
