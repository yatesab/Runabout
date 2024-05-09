using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    [SerializeField] private PhysicsShip physicsShip;
    public float MaxDistance;

    [Header("Disruptor Settings")]
    [SerializeField] private GameObject laserBolt;
    [SerializeField] private float disrupterCooldownTime = 0.5f;

    [SerializeField] private float currentDisruptorCooldown = 0f;

    [Header("Phaser Settings")]
    [SerializeField] private GameObject laserHitParticles;

    public float laserDamage = 0.25f;
    public float laserHeatThreshold = 20f;
    public float laserHeatRate = 0.25f;
    public float laserCoolRate = 0.5f;
    public float currentLaserHeat = 0f;

    [Header("Audio Settings")]
    [SerializeField] private AudioControl audioControl;

    private float timeBetweenDamage = 0.25f;
    private float currentTimeBetweenDamage = 0f;
    private LineRenderer _beam;

    // Start is called before the first frame update
    void Start()
    {
        _beam = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDisruptorCooldown > 0)
        {
            currentDisruptorCooldown -= Time.deltaTime;
        }
    }

    public void FireWeapon(int selectedWeaponType, LayerMask layerMask, Transform muzzle)
    {
        switch(selectedWeaponType)
        {
            case 0:
                FireDisruptor(muzzle);
                break;
            case 1:
                FirePhaser(layerMask, muzzle);
                break;
        }
    }

    public void StopFireWeapon(int selectedWeaponType, Transform muzzle)
    {
        switch (selectedWeaponType)
        {
            case 0:
                break;
            case 1:
                audioControl.Stop("Beam");

                _beam.enabled = false;
                _beam.SetPosition(0, muzzle.position);
                _beam.SetPosition(1, muzzle.position);
                break;
        }
    }

    private void ApplyDamage(HealthComponent healthComponent)
    {
        currentTimeBetweenDamage += Time.deltaTime;

        if (currentTimeBetweenDamage > timeBetweenDamage)
        {
            currentTimeBetweenDamage = 0f;
            healthComponent.TakeDamage(laserDamage);
        }
    }

    public void FireDisruptor(Transform muzzle)
    {
        if (currentDisruptorCooldown <= 0)
        {
            if (!audioControl.GetSource("Blast").isPlaying)
            {
                audioControl.Play("Blast");
            }

            GameObject bolt = Instantiate(laserBolt, muzzle.position, muzzle.rotation);
            bolt.GetComponent<Rigidbody>().velocity = physicsShip.Velocity;
            bolt.GetComponent<LaserBolt>().maxDistance = MaxDistance;

            currentDisruptorCooldown = disrupterCooldownTime;
        }
    }

    public void FirePhaser(LayerMask layerMask, Transform muzzle)
    {
        if (!audioControl.GetSource("Beam").isPlaying)
        {
            audioControl.Play("Beam");
        }

        _beam.enabled = true;
        RaycastHit hit;

        if (TargetInfo.IsTargetInRange(muzzle.position, muzzle.TransformDirection(Vector3.forward), out hit, MaxDistance, layerMask))
        {
            Instantiate(laserHitParticles, hit.point, Quaternion.LookRotation(hit.normal));

            HealthComponent healthComponent = hit.collider.GetComponent<HealthComponent>();

            if (healthComponent)
            {
                ApplyDamage(healthComponent);

                _beam.SetPosition(0, muzzle.position);
                _beam.SetPosition(1, hit.point);
            }
        }
        else
        {
            // Get location in world space
            Vector3 newLocation = muzzle.position + muzzle.forward * MaxDistance;

            _beam.SetPosition(0, muzzle.position);
            _beam.SetPosition(1, newLocation);
        }
    }
}
