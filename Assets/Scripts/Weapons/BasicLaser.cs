using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLaser : Weapon
{
    [SerializeField] private float _maxLength;
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private LayerMask layerMask;
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

    public override void FireWeapon(TurretArmMirror turretArmMirror)
    {
        if (!overHeated)
        {
            _beam.enabled = true;

            // Cast ray and create laser line
            RaycastHit hit;

            if (TargetInfo.IsTargetInRange(turretArmMirror.muzzle.position, turretArmMirror.muzzle.TransformDirection(Vector3.forward), out hit, _maxLength, layerMask))
            {
                Instantiate(laserHitParticles, hit.point, Quaternion.LookRotation(hit.normal));

                HealthComponent healthComponent = hit.collider.GetComponent<HealthComponent>();

                if (healthComponent)
                {
                    ApplyDamage(healthComponent);
                    
                    _beam.SetPosition(0, turretArmMirror.muzzle.position);
                    _beam.SetPosition(1, hit.point);
                }
            }
            else
            {
                Vector3 hitPosition = turretArmMirror.muzzle.position + turretArmMirror.muzzle.forward * _maxLength;

                _beam.SetPosition(0, turretArmMirror.muzzle.position);
                _beam.SetPosition(1, hitPosition);
            }

            HeatLaser();
        }
    }

    public override void StopFireWeapon(TurretArmMirror turretArmMirror)
    {
        isFireing = false;
        _beam.enabled = false;
        _beam.SetPosition(0, turretArmMirror.muzzle.position);
        _beam.SetPosition(1, turretArmMirror.muzzle.position);
    }
}
