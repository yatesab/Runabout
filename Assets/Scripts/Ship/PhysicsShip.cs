using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsShip : MonoBehaviour
{
    public Vector3 Velocity { get { return shipRigidbody.velocity; } }

    public Vector3 AngularVelocity { get {  return shipRigidbody.angularVelocity; } }

    private Rigidbody shipRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        shipRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
