using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHealth : HealthComponent
{
    public GameObject explosionParticles;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (health <= 0)
        {
            ExplodeMine();

            Destroy(this.gameObject);
        }
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void ExplodeMine()
    {
        AudioManager.instance.Play("Mine Explosion");

        GameObject explosion = Instantiate(explosionParticles, transform.position, transform.rotation);

        explosion.transform.localScale = new Vector3(5, 5, 5);
    }
}
