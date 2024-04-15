using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : HealthComponent
{
    private float maxPartHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxPartHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
