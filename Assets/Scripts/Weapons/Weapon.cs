using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int AmmoCapacity {get;set;}

    public int CurrentAmmo {get;set;}

    public float MaxDistance;

    public bool TargetHit { get; set; }

    public RaycastHit HitPoint { get; set; }

    public Vector3 TargetPosition { get; set; }

    public virtual void FireWeapon(int selectedWeaponType, LayerMask layerMask, Transform muzzle)
    {
        //Fire weapon
    }

    public virtual void StopFireWeapon(Transform muzzle)
    {
        //Stop weapon
    }
}
