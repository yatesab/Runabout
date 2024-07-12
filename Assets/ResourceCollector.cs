using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceCollector : TractorBeam
{
    [SerializeField] private Transform collectionLocation;
    [SerializeField] private float distanceToLocation = 0.5f;
    public void FixedUpdate()
    {
        if (beamActivated)
        {
            float dist = Vector3.Distance(currentTarget.transform.position, collectionLocation.position);

            if(dist < distanceToLocation) {
                CollectTarget();
            } else
            {
                currentTarget.transform.position = Vector3.Lerp(currentTarget.transform.position, collectionLocation.position, tractorBeamStrength * Time.fixedDeltaTime);
            }
        }
        else if (grabColliders.Count > 0 && !beamActivated)
        {
            SetupNextTarget();
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public void SetupNextTarget()
    {
        foreach(Collider collider in grabColliders)
        {
            if (!collider.GetComponent<Rigidbody>().isKinematic)
            {
                beamActivated = true;

                currentTarget.GetComponent<Rigidbody>().isKinematic = true;

                lineRenderer.positionCount = 2;
                SetLinePositions();
            }
        }
    }

    public void CollectTarget()
    {
        beamActivated = false;
        Destroy(currentTarget.gameObject);

        lineRenderer.positionCount = 0;

        if (grabColliders.Contains(currentTarget))
        {
            grabColliders.Remove(currentTarget);
        }

        currentTarget = null;

        // Call event for saving resource
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add object to radar list
        if (!grabColliders.Contains(other) && other.tag == "Grab")
        {
            grabColliders.Add(other);
        }

    }

    void OnTriggerExit(Collider other)
    {
        // Remove object from radar
        if (grabColliders.Contains(other) && other.tag == "Grab")
        {
            grabColliders.Remove(other);
        }
    }
}
