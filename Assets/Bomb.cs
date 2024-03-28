using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionParticles;
    public float explosionRadius = 20f;
    public float bombSpeed = 75f;
    public float bombDamage = 100f;
    public float explosionForce = 150f;
    public float maxDeploymentTime = 5f;

    protected Rigidbody bombBody;
    protected float timeDeployed;

    // Start is called before the first frame update
    public void Start()
    {
        bombBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Update()
    {
        timeDeployed += Time.deltaTime;
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
            healthComponent.TakeDamage(bombDamage);
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
