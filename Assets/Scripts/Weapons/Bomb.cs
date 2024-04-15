using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    public GameObject explosionParticles;
    public float explosionRadius = 20f;
    public float explosionForce = 150f;
    public float maxDeploymentTime = 5f;

    public Vector3 TargetPosition { get; set; }

    protected float timeDeployed;

    // Update is called once per frame
    public void Update()
    {
        timeDeployed += Time.deltaTime;
    }
    public void FixedUpdate()
    {
        ApplyForceToBody(transform.up * -1);
    }
    protected void HandleExplodeBomb()
    {
        // Play Explosion Sound
        if (!AudioManager.instance.GetSource("Mine Explosion").isPlaying)
        {
            AudioManager.instance.Play("Mine Explosion");
        }

        // Create the explosion effect at the targetPosition
        GameObject explosion = Instantiate(explosionParticles, transform.position, transform.rotation);

        // Scaleing up the explosion for effect right now
        explosion.transform.localScale = new Vector3(40, 40, 40);

        // Destroy this game object
        Destroy(gameObject);
    }

    protected Collider[] GetCollidersInRadius(float checkRadius)
    {
        Collider[] colliders = new Collider[100];
        Physics.OverlapSphereNonAlloc(transform.position, checkRadius, colliders);

        return colliders;
    }

    protected void AddForceToCollider(Collider collider)
    {
        float colliderDistance = Vector3.Distance(transform.position, collider.transform.position) / explosionRadius;

        collider.attachedRigidbody.AddExplosionForce(explosionForce * (1 - colliderDistance), transform.position, explosionRadius);

        HandleDamage(collider);
    }

    protected void AddForceToColliders()
    {
        Collider[] hitColliders = GetCollidersInRadius(explosionRadius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.attachedRigidbody)
            {
                AddForceToCollider(collider);
            }
        }
    }
}
