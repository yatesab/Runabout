using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] protected Transform centerPoint;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected GameObject predictionPoint;

    public bool TriggerActivated { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            UpdateTargetPosition(hit.point);
            predictionPoint.transform.position = hit.point;
        }
        else
        {
            // Get location in world space
            Vector3 newLocation = centerPoint.position + centerPoint.forward * maxDistance;

            UpdateTargetPosition(newLocation);
            predictionPoint.transform.position = newLocation;
        }
    }

    protected virtual void UpdateTargetPosition(Vector3 newTargetPosition) { }

    public virtual void StopFiringWeapon() {}
}
