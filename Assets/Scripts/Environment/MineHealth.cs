using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHealth : HealthComponent
{
    public GameObject explosionParticles;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        Instantiate(explosionParticles);
    }
}
