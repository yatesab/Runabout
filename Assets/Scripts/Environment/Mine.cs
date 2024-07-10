using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private float explosionRadius = 20f;
    [SerializeField] private float explosionForce = 100f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float trackingTimeLimit = 10f;
    [SerializeField] private float damage;
    [SerializeField] private AudioControl MineAudio;

    private bool isTriggered;
    private Transform _target;
    private float trackingTime = 0f;
    private Collider mineCollider;
    private Rigidbody mineBody;

    public void Start()
    {
        mineBody = GetComponent<Rigidbody>();
        mineCollider = GetComponent<Collider>();
    }

    public void Update() 
    {
        TrackTarget();    
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(_target == null && collider.tag == "proximity")
        {
            isTriggered = true;
            _target = collider.transform;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        AddForceToColliders();

        DestoryMine();
    }

    public void StartMineExplosion()
    {
        DestoryMine();
    }

    private void PhysicsMovement()
    {
        transform.LookAt(_target);
        mineBody.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    private void TrackTarget()
    {
        if (isTriggered && mineBody != null)
        {
            trackingTime += Time.deltaTime;

            PhysicsMovement();

            if (trackingTime > trackingTimeLimit)
            {
                AddForceToColliders();

                DestoryMine();
            }
        }
    }

    private void DestoryMine()
    {
        // Play Explosion Sound
        if (!MineAudio.GetSource("Explosion").isPlaying)
        {
            MineAudio.Play("Explosion");
        }

        // Create the explosion effect at the targetPosition
        GameObject explosion = Instantiate(explosionParticles, transform.position, transform.rotation);

        // Scaleing up the explosion for effect right now
        explosion.transform.localScale = new Vector3(10, 10, 10);

        Destroy(gameObject);
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

    protected void HandleDamage(Collider collider)
    {
        if (collider.CompareTag("Ship"))
        {
            // This is for hitting the ship which has its health component in the parent
            HealthComponent healthComponent = collider.GetComponentInParent<HealthComponent>();

            if (healthComponent)
            {
                healthComponent.TakeDamage(damage);
            }
        }
        else
        {
            HealthComponent healthComponent = collider.GetComponent<HealthComponent>();

            if (healthComponent)
            {
                healthComponent.TakeDamage(damage);
            }
        }
    }

    protected Collider[] GetCollidersInRadius(float checkRadius)
    {
        return Physics.OverlapSphere(transform.position, checkRadius);
    }
}
