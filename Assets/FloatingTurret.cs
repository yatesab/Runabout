using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTurret : MonoBehaviour
{
    public Transform muzzle;

    [Header("Disruptor Settings")]
    [SerializeField] private GameObject laserBolt;
    [SerializeField] private float disrupterCooldownTime = 0.5f;

    private float currentDisruptorCooldown = 0f;

    private bool isTriggered;
    private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTriggered && _target != null)
        {
            transform.LookAt(_target);
            FireDisruptor();
        }

        if (currentDisruptorCooldown > 0)
        {
            currentDisruptorCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(_target == null)
        {
            isTriggered = true;
            _target = collider.transform;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        isTriggered = false;
        _target = null;
    }

    public void FireDisruptor()
    {
        if (currentDisruptorCooldown <= 0)
        {
            GameObject bolt = Instantiate(laserBolt, muzzle.position, muzzle.rotation);
            bolt.GetComponent<LaserBolt>().maxDistance = 50f;

            currentDisruptorCooldown = disrupterCooldownTime;
        }
    }
}
