using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
   
    protected Rigidbody projectileBody;

    public float maxDistance { get; set; }

    // Start is called before the first frame update
    public void Start()
    {
        projectileBody = GetComponent<Rigidbody>();
    }

    protected void ApplyForceToBody(Vector3 forceDirection)
    {
        projectileBody.AddForce(forceDirection * speed);
    }

    protected void HandleDamage(Collider collider)
    {
        if(collider.CompareTag("Ship"))
        {
            // This is for hitting the ship which has its health component in the parent
            HealthComponent healthComponent = collider.GetComponentInParent<HealthComponent>();

            if (healthComponent)
            {
                healthComponent.TakeDamage(damage);
            }
        } else
        {
            HealthComponent healthComponent = collider.GetComponent<HealthComponent>();

            if (healthComponent)
            {
                healthComponent.TakeDamage(damage);
            }
        }
    }


}
