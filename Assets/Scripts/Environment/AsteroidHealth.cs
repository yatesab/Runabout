using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : HealthComponent
{
    private Rigidbody asteroidBody;
    [SerializeField] private GameObject crystal;

    public void Start()
    {
        float generalScale = transform.localScale.x / 2;

        health = generalScale / 2;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (health <= 0)
        {
            Instantiate(crystal, transform.position, transform.rotation);

            Destroy(this.gameObject);
        }
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
