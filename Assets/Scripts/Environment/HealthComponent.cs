using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float health;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    public virtual void HealDamage(float healAmount)
    {
        health += healAmount;
    }

    protected virtual void OnDestroy()
    {

    }
}
