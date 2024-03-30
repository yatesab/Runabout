using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLaser : MonoBehaviour
{
    public GameObject laserHitParticles;

    public float laserDamage = 0.25f;
    public float laserHeatThreshold = 20f;
    public float laserHeatRate = 0.25f;
    public float laserCoolRate = 0.5f;
    public float currentLaserHeat = 0f;

    private bool isFireing;
    private bool overHeated = false;
    private float timeBetweenDamage = 0.25f;
    private float currentTimeBetweenDamage = 0f;
    private LineRenderer _beam;

    public void Start()
    {
        _beam = GetComponent<LineRenderer>();
    }

    private void Update() 
    {
        CoolLaser();
    }

    private void ApplyDamage(HealthComponent healthComponent)
    {
        currentTimeBetweenDamage += Time.deltaTime;

        if(currentTimeBetweenDamage > timeBetweenDamage)
        {
            currentTimeBetweenDamage = 0f;
            healthComponent.TakeDamage(laserDamage);
        }
    }

    void HeatLaser()
    {
        if(isFireing && currentLaserHeat < laserHeatThreshold)
        {
            currentLaserHeat += laserHeatRate * Time.deltaTime;

            if(currentLaserHeat >= laserHeatThreshold)
            {
                overHeated = true;
                isFireing = false;
            }
        }
    }

    void CoolLaser()
    {
        if(overHeated)
        {
            if(currentLaserHeat / laserHeatThreshold <= 0.5f)
            {
                overHeated = false;
            }
        }

        if(currentLaserHeat > 0f)
        {
            currentLaserHeat -= laserCoolRate * Time.deltaTime;
        }
    }

    public void FireWeapon(Transform muzzle)
    {
        
    }

    public void StopFireWeapon(Transform muzzle)
    {

        AudioManager.instance.Stop("Laser Beam");

        isFireing = false;
        _beam.enabled = false;
        _beam.SetPosition(0, muzzle.position);
        _beam.SetPosition(1, muzzle.position);
    }
}
