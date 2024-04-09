using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHealth : HealthComponent
{
    public Mine mine;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (health <= 0)
        {
            mine.StartMineExplosion();
        }
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
