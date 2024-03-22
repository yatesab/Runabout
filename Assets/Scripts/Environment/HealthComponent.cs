using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public GameObject explosionParticles;

    public float health = 100f;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <= 0)
        {
            // Apply force with adjustable magnitude and mode
            if (!AudioManager.instance.GetSource("Mine Explosion").isPlaying)
            {
                AudioManager.instance.Play("Mine Explosion");
            }

            GameObject explosion = Instantiate(explosionParticles, transform.position, transform.rotation);

            explosion.transform.localScale = new Vector3(5, 5, 5);

            Destroy(this.gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        
    }
}
