using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public GameObject explosionParticles;
    public float explosionRadius = 20f;
    public float torpedoSpeed = 200f;
    public float torpedoDamage = 50f;
    public float explosionForce = 100f;
    public float MaxDistance { get; set; } = 100f;

    public Vector3 TargetPosition { get; set; }

    protected Rigidbody missleBody;
    protected Vector3 startLocation;
    protected float distance;

    // Start is called before the first frame update
    protected void Start()
    {
        missleBody = GetComponent<Rigidbody>();
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

        if (distance >= MaxDistance && missleBody)
        {
            DestroyTorpedoRigidbody();
        }
    }

    protected void DestroyTorpedoRigidbody()
    {
        Destroy(missleBody);

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Destroy(mesh);
    }

    protected void HandleExplodeTorpedo()
    {
        // Play Explosion Sound
        if (!AudioManager.instance.GetSource("Mine Explosion").isPlaying)
        {
            AudioManager.instance.Play("Mine Explosion");
        }

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

        HealthComponent healthComponent = collider.GetComponent<HealthComponent>();

        if (healthComponent)
        {
            healthComponent.TakeDamage(torpedoDamage);
        }
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
