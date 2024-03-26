using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disrupter : Weapon
{
    [SerializeField] private PhysicsShip physicsShip;

    [SerializeField] private GameObject laserBolt;
    [SerializeField] private float disrupterCooldownTime = 2f;

    [SerializeField] private float currentCooldown = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public override void FireWeapon(Transform muzzle)
    {
        if (currentCooldown <= 0)
        {
            AudioManager.instance.Play("Laser Bolt");

            GameObject bolt = Instantiate(laserBolt, muzzle.position, muzzle.rotation);
            bolt.GetComponent<Rigidbody>().velocity = physicsShip.Velocity;

            bolt.GetComponent<LaserBolt>().TargetPosition = TargetPosition;
            currentCooldown = disrupterCooldownTime;
        }
    }

}
