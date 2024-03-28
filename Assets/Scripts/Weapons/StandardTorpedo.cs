using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTorpedo : Torpedo
{
    // Update is called once per frame
    public new void Update()
    {
        base.Update();

        if (distance < distanceFromTarget)
        {
            AddForceToColliders();

            HandleExplodeTorpedo();
        }
    }

    void FixedUpdate()
    {
        missleBody.AddForce(transform.forward * torpedoSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        AddForceToColliders();

        HandleExplodeTorpedo();
    }
}
