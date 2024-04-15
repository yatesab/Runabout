using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBomb : Bomb
{
    // Update is called once per frame
    public new void Update()
    {
        base.Update();

        if (timeDeployed >= maxDeploymentTime)
        {
            // Explode The Thing!!!!
            AddForceToColliders();

            HandleExplodeBomb();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AddForceToColliders();

        HandleExplodeBomb();
    }
}
