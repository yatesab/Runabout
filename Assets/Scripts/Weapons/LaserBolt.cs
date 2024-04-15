using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBolt : Projectile
{
    private Vector3 startLocation;
    
    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();

        startLocation = transform.position;
        projectileBody.AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, startLocation);

        if(distance > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        HandleDamage(collider);

        Destroy(gameObject);
    }
}
