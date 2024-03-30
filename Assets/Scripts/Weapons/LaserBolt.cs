using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBolt : MonoBehaviour
{
    public float boltDamage = 10f;
    public float MaxDistance { get; set; }

    private Rigidbody laserBody;
    private Vector3 startLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        laserBody = GetComponent<Rigidbody>();

        startLocation = transform.position;
        laserBody.AddForce(transform.forward * 500);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, startLocation);

        if(distance > MaxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthComponent healthComponent = other.GetComponent<HealthComponent>();

        if (healthComponent)
        {
            healthComponent.TakeDamage(5f);
        }

        Destroy(gameObject);
    }
}
