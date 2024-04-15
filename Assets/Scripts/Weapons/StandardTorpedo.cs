using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTorpedo : Torpedo
{
    // Update is called once per frame
    public new void Update()
    {
        base.Update();

        if (distance >= maxDistance)
        {
            AddForceToColliders();

            HandleExplodeTorpedo();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AddForceToColliders();

        HandleExplodeTorpedo();
    }
}
