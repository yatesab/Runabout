using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTorpedo : Torpedo
{
    public GameObject gravityWellParticles;

    public float pullRadius = 50f;
    public float pullForce = 40f;
    public float pullTimeAmount = 2f;

    private bool pullActive = false;
    private float currentPullTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(!pullActive)
        {
            base.Update();

            if (distance < distanceFromTarget)
            {
                //Pull In objects before explosion
                pullActive = true;
                Instantiate(gravityWellParticles, TargetPosition, transform.rotation);
            }
        }
    }

    void FixedUpdate()
    {
        if(!pullActive)
        {
            missleBody.AddForce(transform.forward * torpedoSpeed);
        } else
        {
            if (currentPullTime < pullTimeAmount)
            {
                PullCollidersWithForce();

                currentPullTime += Time.fixedDeltaTime;
            }
            else
            {
                AddForceToCollisions();

                HandleExplodeTorpedo();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        pullActive = true;
        Instantiate(gravityWellParticles, TargetPosition, transform.rotation);

        TargetPosition = other.ClosestPoint(transform.position);

        // Destroy rigidbody and stop all movement
        DestroyTorpedoRigidbody();
    }

    private void DestroyTorpedoRigidbody()
    {
        Rigidbody torpedoBody = GetComponent<Rigidbody>();
        Destroy(torpedoBody);

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Destroy(mesh);
    }

    private void AddForceToCollisions()
    {
        Collider[] hitColliders = GetExplosionRadiusColliders(explosionRadius);

        foreach(Collider collider in hitColliders)
        {
            if (collider.attachedRigidbody)
            {
                AddForceToCollider(collider);
            }
        }
    }

    private Collider[] GetExplosionRadiusColliders(float checkRadius)
    {
        RaycastHit hit;

        return Physics.OverlapSphere(transform.position, checkRadius);
    }

    private void PullCollidersWithForce()
    {
        Collider[] hitColliders = GetExplosionRadiusColliders(pullRadius);

        foreach (Collider collider in hitColliders)
        {
            if(collider.attachedRigidbody)
            {
                float distance = Vector3.Distance(TargetPosition, collider.transform.position) / pullRadius;

                Vector3 pullDirection = (TargetPosition - collider.transform.position).normalized;

                //Pull in colliders
                collider.attachedRigidbody.AddForce(pullDirection * pullForce * (1 - distance));
            }
        }
    }
}
