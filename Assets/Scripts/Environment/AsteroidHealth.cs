using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : HealthComponent
{
    private Rigidbody asteroidBody;

    public void Start()
    {
        asteroidBody = GetComponent<Rigidbody>();

        float generalScale = transform.localScale.x / 2;

        asteroidBody.mass = generalScale;
        health = generalScale / 2;
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
