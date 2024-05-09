using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : Projectile
{
    public GameObject explosionParticles;
    public float explosionRadius = 20f;
    public float explosionForce = 100f;

    public Vector3 TargetPosition { get; set; }

    protected Vector3 startLocation;
    protected float distance;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        startLocation = transform.position;
        distance = 0f;
    }

    // Update is called once per frame
    protected void Update()
    {
        if(TargetPosition != null)
        {
            transform.LookAt(TargetPosition);
        }

        distance = Vector3.Distance(transform.position, startLocation);
    }

    public void FixedUpdate()
    {
        ApplyForceToBody(transform.forward);
    }

    protected void HandleExplodeTorpedo()
    {
        // Play Explosion Sound
        //if (!AudioManager.instance.GetSource("Mine Explosion").isPlaying)
        //{
        //    AudioManager.instance.Play("Mine Explosion");
        //}

        // Create the explosion effect at the targetPosition
        GameObject explosion = Instantiate(explosionParticles, transform.position, transform.rotation);

        // Scaleing up the explosion for effect right now
        explosion.transform.localScale = new Vector3(10, 10, 10);

        // Destroy this game object
        Destroy(gameObject);
    }

    protected Collider[] GetCollidersInRadius(float checkRadius)
    {
        return Physics.OverlapSphere(transform.position, checkRadius);
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
