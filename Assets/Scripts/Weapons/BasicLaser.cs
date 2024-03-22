using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLaser : Weapon
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

    public override void FireWeapon(Transform muzzle)
    {
        if (!overHeated)
        {
            isFireing = true;

            if (!AudioManager.instance.GetSource("Laser Beam").isPlaying)
            {
                AudioManager.instance.Play("Laser Beam");
            }

            _beam.enabled = true;

            if (TargetHit)
            {
                Instantiate(laserHitParticles, TargetPosition, Quaternion.LookRotation(HitPoint.normal));

                HealthComponent healthComponent = HitPoint.collider.GetComponent<HealthComponent>();

                if (healthComponent)
                {
                    ApplyDamage(healthComponent);
                    
                    _beam.SetPosition(0, muzzle.position);
                    _beam.SetPosition(1, TargetPosition);
                }
            }
            else
            {
                _beam.SetPosition(0, muzzle.position);
                _beam.SetPosition(1, TargetPosition);
            }

            HeatLaser();
        }
    }

    public override void StopFireWeapon(Transform muzzle)
    {
        base.StopFireWeapon(muzzle);

        AudioManager.instance.Stop("Laser Beam");

        isFireing = false;
        _beam.enabled = false;
        _beam.SetPosition(0, muzzle.position);
        _beam.SetPosition(1, muzzle.position);
    }
}
