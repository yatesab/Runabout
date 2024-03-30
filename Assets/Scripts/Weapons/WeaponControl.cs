using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] protected Transform centerPoint;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected GameObject predictionPoint;

    protected int selectedWeapon = 0;
    protected int selectedWeaponType = 0;

    public void ChangeWeapon(float stage)
    {
        selectedWeapon = (int)stage;
    }

    public void ChangeWeaponType(float stage)
    {
        selectedWeaponType = (int)stage;
    }

    public void UpdateRotation(Quaternion rotation)
    {
        centerPoint.localRotation = rotation;
    }

    protected void GetTargetHitInfo(float maxDistance)
    {
        RaycastHit hit;

        if (TargetInfo.IsTargetInRange(centerPoint.position, centerPoint.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
        {
            predictionPoint.transform.position = hit.point;
        }
        else
        {
            // Get location in world space
            Vector3 newLocation = centerPoint.position + centerPoint.forward * maxDistance;

            predictionPoint.transform.position = newLocation;
        }
    }
}
