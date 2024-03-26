using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float health = 100f;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    protected virtual void OnDestroy()
    {
        
    }
}
