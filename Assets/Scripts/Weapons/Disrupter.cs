using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disrupter : Weapon
{
    public GameObject laserBolt;
    public float disrupterCooldownTime = 2f;

    public float currentCooldown = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(disrupterCooldownTime > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public override void FireWeapon(TurretArmMirror turretArmMirror)
    {
        if (currentCooldown <= 0)
        {
            GameObject bolt = Instantiate(laserBolt, turretArmMirror.muzzle.position, turretArmMirror.muzzle.rotation);

            currentCooldown = disrupterCooldownTime;
        }
    }

    public override void StopFireWeapon(TurretArmMirror turretArmMirror)
    {
        //Stop weapon
    }
}
