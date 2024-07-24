using UnityEngine;
using System.Collections.Generic;
using System;

public class ShipTractorBeam : TractorBeam
{
    public event Action scanAction;
    [SerializeField] private float tractorBeamScanRadius = 50f;

    public void FixedUpdate()
    {
        if (beamActivated)
        {
            currentTarget.transform.position = Vector3.Slerp(currentTarget.transform.position, transform.TransformPoint(targetMoveLocation), tractorBeamStrength * Time.fixedDeltaTime);
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public void StartTractorBeam(int colliderIndex)
    {
        currentTarget = grabColliders[colliderIndex];
        beamActivated = true;

        currentTarget.GetComponent<Rigidbody>().isKinematic = true;

        targetMoveLocation = transform.InverseTransformPoint(currentTarget.transform.position);

        lineRenderer.positionCount = 2;
        SetLinePositions();
    }

    public void StopTractorBeam()
    {
        beamActivated = false;
        currentTarget.GetComponent<Rigidbody>().isKinematic = false;

        currentTarget = null;

        lineRenderer.positionCount = 0;
    }

    public void ScanArea()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, tractorBeamScanRadius);

        grabColliders.Clear();
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Grab")
            {
                grabColliders.Add(hitCollider);
            }
        }

        scanAction.Invoke();
    }
}
