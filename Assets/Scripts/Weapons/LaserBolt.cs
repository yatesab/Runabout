using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBolt : MonoBehaviour
{
    public float boltDamage = 10f;

    private Rigidbody laserBody;
    private Vector3 startLocation;
    private Vector3 currentLocation;

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
        currentLocation = transform.position;
        float distance = Vector3.Distance(currentLocation, startLocation);

        if(distance > 500f)
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
