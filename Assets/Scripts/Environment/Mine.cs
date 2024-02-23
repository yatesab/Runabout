using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float speed = 1f;
    public GameObject explosionParticles;

    private bool isTriggered;

    private Transform _target;
    private Rigidbody rb;
    private AudioSource explosionSound;
    private float trackingTimeLimit = 10f;
    private float trackingTime = 0f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        explosionSound = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        TrackTarget();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ship" && _target == null)
        {
            Debug.Log("Mine In Proximity");
            isTriggered = true;
            _target = other.transform;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Access the Rigidbody of the explosionCollider object
        Rigidbody colliderRigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (colliderRigidbody == null)
        {
            Debug.LogWarning("Colliding object has no Rigidbody!");
            return;
        }
        // Get the first contact point (adjust if you need multiple)
        Vector3 contactPoint = other.contacts[0].point;

        // Calculate force direction based on collision normal
        Vector3 forceDirection = other.contacts[0].normal.normalized;

        Instantiate(explosionParticles, contactPoint, Quaternion.LookRotation(forceDirection));
        DestoryMine();
    }

    private void TrackTarget()
    {
        if (isTriggered)
        {
            trackingTime = Time.deltaTime;
            transform.LookAt(_target);
            rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

            if (trackingTime > trackingTimeLimit)
            {
                DestoryMine();
            }
        }
    }

    private void DestoryMine()
    {
        // Apply force with adjustable magnitude and mode
        if (!AudioManager.instance.GetSource("Mine Explosion").isPlaying)
        {
            AudioManager.instance.Play("Mine Explosion");
        }

        Destroy(this.gameObject);
    }
}
