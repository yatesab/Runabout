using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int AmmoCapacity {get;set;}

    public int CurrentAmmo {get;set;}

    public abstract void FireWeapon(TurretArmMirror turretArmMirror);
    public abstract void StopFireWeapon(TurretArmMirror turretArmMirror);
}
