using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTorpedo : Torpedo
{
    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (distance < distanceFromTarget)
        {
            AddForceToCollisions();

            HandleExplodeTorpedo();
        }
    }

    void FixedUpdate()
    {
        missleBody.AddForce(transform.forward * torpedoSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger");
        AddForceToCollider(other);

        HandleExplodeTorpedo();
    }

    private void AddForceToCollisions()
    {
        Collider[] hitColliders = GetExplosionRadiusColliders();

        foreach(Collider collider in hitColliders)
        {
            if (collider.attachedRigidbody)
            {
                AddForceToCollider(collider);
            }
        }
    }

    private Collider[] GetExplosionRadiusColliders()
    {
        RaycastHit hit;

        return Physics.OverlapSphere(transform.position, explosionRadius);
    }
}
