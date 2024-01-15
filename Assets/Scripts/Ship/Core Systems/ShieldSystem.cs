using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSystem : CoreSystem
{
    [SerializeField] private float maxHealth = 1000f;
    public float Health {get;set;} = 0f;

    [SerializeField] private float maxShield = 1000f;
    public float Shield {get;set;} = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Shield = maxShield;
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShieldTakeDamage(float damage)
    {
        Shield -= damage;
    }

    public void HealthTakeDamage(float damage)
    {
        Health -= damage;
    }

    private void OnCollisionEnter(Collision other) {
        if(Shield > 0)
        {
            // Remove shiled from the ship
            ShieldTakeDamage(1000f);
        } else 
        {
            // Remove Health from the ship since no shield was present
            HealthTakeDamage(1000f);
        }
    }
}
