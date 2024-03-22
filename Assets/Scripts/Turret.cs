using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Weapon[] weaponList;
    public int selectedWeapon = 0;

    public Transform meshOffset;
    public Transform muzzle;

    public Vector3 TargetPosition { get; set; }

    // Update is called once per frame
    void Update()
    {
        // Look at current target
        meshOffset.LookAt(TargetPosition);
    }

    public void ChangeWeaponType(float stage)
    {
        selectedWeapon = (int) stage;
    }

    public void FireSelectedWeapon(LayerMask layerMask)
    {
        GetTargetHitInfo(layerMask);
    }

    private void GetTargetHitInfo(LayerMask layerMask)
    {
        RaycastHit hit;

        if (TargetInfo.IsTargetInRange(muzzle.position, muzzle.TransformDirection(Vector3.forward), out hit, weaponList[selectedWeapon].MaxDistance, layerMask))
        {
            weaponList[selectedWeapon].TargetHit = true;
            weaponList[selectedWeapon].HitPoint = hit;
            weaponList[selectedWeapon].TargetPosition = hit.point;
        }
        else
        {
            // Get location in world space
            Vector3 newLocation = muzzle.position + muzzle.forward * weaponList[selectedWeapon].MaxDistance;

            weaponList[selectedWeapon].TargetHit = false;
            weaponList[selectedWeapon].TargetPosition = newLocation;
        }

        weaponList[selectedWeapon].FireWeapon(muzzle);
    }

    public void StopSelectedWeapon()
    {
        weaponList[selectedWeapon].StopFireWeapon(muzzle);
    }
}
