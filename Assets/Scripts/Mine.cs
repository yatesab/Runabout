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
    }
}
