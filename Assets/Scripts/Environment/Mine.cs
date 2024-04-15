using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float trackingTimeLimit = 10f;

    private bool isTriggered;
    private Transform _target;
    private float trackingTime = 0f;
    private Collider mineCollider;
    private Rigidbody mineBody;

    public void Start()
    {
        mineBody = GetComponent<Rigidbody>();
        mineCollider = GetComponent<Collider>();
    }

    public void Update() 
    {
        TrackTarget();    
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(_target == null)
        {
            isTriggered = true;
            _target = collider.transform;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Access the Rigidbody of the explosionCollider object
        Rigidbody colliderRigidbody = other.gameObject.GetComponent<Rigidbody>();

        if (colliderRigidbody == null)
        {
            Debug.LogWarning("Colliding object has no Rigidbody!");
            return;
        }

        DestoryMine();
    }

    public void StartMineExplosion()
    {
        DestoryMine();
    }

    private void PhysicsMovement()
    {
        transform.LookAt(_target);
        mineBody.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    private void TrackTarget()
    {
        if (isTriggered && mineBody != null)
        {
            trackingTime = Time.deltaTime;

            PhysicsMovement();

            if (trackingTime > trackingTimeLimit)
            {
                DestoryMine();
            }
        }
    }

    private void DestoryMine()
    {
        // Play Explosion Sound
        if (!AudioManager.instance.GetSource("Mine Explosion").isPlaying)
        {
            AudioManager.instance.Play("Mine Explosion");
        }

        // Create the explosion effect at the targetPosition
        GameObject explosion = Instantiate(explosionParticles, transform.position, transform.rotation);

        // Scaleing up the explosion for effect right now
        explosion.transform.localScale = new Vector3(10, 10, 10);

        Destroy(gameObject);
    }
}
