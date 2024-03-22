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
    public float distanceFromTarget = 2f;

    public Transform Target { get; set; }
    public Vector3 TargetPosition { get; set; }

    protected Rigidbody missleBody;
    protected Vector3 currentLocation;
    protected float distance;

    // Start is called before the first frame update
    protected void Start()
    {
        missleBody = GetComponent<Rigidbody>();

        distance = Vector3.Distance(transform.position, TargetPosition);
    }

    // Update is called once per frame
    protected void Update()
    {
        // Need to add something for when your target is a transform that is moving. TODO
        transform.LookAt(TargetPosition);

        distance = Vector3.Distance(transform.position, TargetPosition);

        if (distance < distanceFromTarget && missleBody)
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

    protected void AddForceToCollider(Collider collider)
    {
        distance = Vector3.Distance(transform.position, collider.transform.position) / explosionRadius;

        collider.attachedRigidbody.AddExplosionForce(explosionForce * (1 - distance), transform.position, explosionRadius);

        HealthComponent healthComponent = collider.GetComponent<HealthComponent>();

        if (healthComponent)
        {
            healthComponent.TakeDamage(torpedoDamage);
        }
    }

    protected void HandleExplodeTorpedo()
    {
        // Play Explosion Sound
        if (!AudioManager.instance.GetSource("Mine Explosion").isPlaying)
        {
            AudioManager.instance.Play("Mine Explosion");
        }

        // Create the explosion effect at the targetPosition
        GameObject explosion = Instantiate(explosionParticles, TargetPosition, transform.rotation);

        // Scaleing up the explosion for effect right now
        explosion.transform.localScale = new Vector3(10, 10, 10);

        // Destroy this game object
        Destroy(gameObject);
    }
}
