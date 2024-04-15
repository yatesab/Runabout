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
    public new void Update()
    {
        if(!pullActive)
        {
            base.Update();

            if (distance >= maxDistance)
            {
                DestroyTorpedoRigidbody();
                CreateGravityWell();
            }
        }
    }

    public new void FixedUpdate()
    {
        if(!pullActive)
        {
            base.FixedUpdate();
        } else
        {
            if (currentPullTime < pullTimeAmount)
            {
                PullCollidersWithForce();

                currentPullTime += Time.fixedDeltaTime;
            }
            else
            {
                AddForceToColliders();

                HandleExplodeTorpedo();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TargetPosition = collision.collider.ClosestPoint(transform.position);

        CreateGravityWell();
    }
    protected void DestroyTorpedoRigidbody()
    {
        Destroy(projectileBody);

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Destroy(mesh);
    }

    private void CreateGravityWell()
    {
        pullActive = true;
        Instantiate(gravityWellParticles, TargetPosition, transform.rotation);

        DestroyTorpedoRigidbody();
    }


    private void PullCollidersWithForce()
    {
        Collider[] hitColliders = GetCollidersInRadius(pullRadius);

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
