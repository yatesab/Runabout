using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralHealth : HealthComponent
{
    [SerializeField] private GameObject crystal;

    // Start is called before the first frame update
    void Start()
    {
        
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
