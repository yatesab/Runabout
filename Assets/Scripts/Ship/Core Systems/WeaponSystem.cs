using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : CoreSystem
{
    public Weapon leftWeapon;
    public Weapon rightWeapon;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
