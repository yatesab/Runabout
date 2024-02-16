using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float speed = 1f;

    private bool isTriggered;

    private Transform _target;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        TrackTarget();    
    }

    private void TrackTarget()
    {
        if(isTriggered)
        {
            transform.LookAt(_target);
            rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ship")
        {
            Debug.Log("Mine In Proximity");
            isTriggered = true;
            _target = other.transform;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);

        // Access the Rigidbody of the other object
        Rigidbody otherRigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (otherRigidbody == null)
        {
            Debug.LogWarning("Colliding object has no Rigidbody!");
            return;
        }
        // Get the first contact point (adjust if you need multiple)
        Vector3 contactPoint = other.contacts[0].point;

        // Calculate force direction based on collision normal
        Vector3 forceDirection = other.contacts[0].normal.normalized;

        // Apply force with adjustable magnitude and mode
        otherRigidbody.AddForceAtPosition(forceDirection * 1000f, contactPoint, ForceMode.Impulse);
    }
}
