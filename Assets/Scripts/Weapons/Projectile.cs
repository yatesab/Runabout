using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;

    public float maxDistance { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void HandleDamage(Collider collider)
    {
        HealthComponent healthComponent = collider.GetComponent<HealthComponent>();

        if (healthComponent)
        {
            healthComponent.TakeDamage(damage);
        }
        else
        {
            healthComponent = collider.GetComponentInParent<HealthComponent>();
            
            if (healthComponent)
            {
                healthComponent.TakeDamage(damage);
            }
        }
    }
}
