using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : HealthComponent
{
    private Rigidbody shipBody;

    public void Start()
    {
        shipBody = GetComponent<Rigidbody>();

        health = health * shipBody.mass;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
