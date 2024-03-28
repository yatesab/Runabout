using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBomb : Bomb
{
    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();

        if (timeDeployed >= maxDeploymentTime)
        {
            // Explode The Thing!!!!
            AddForceToColliders();

            HandleExplodeBomb();
        }
    }

    void FixedUpdate()
    {
        bombBody.AddForce((transform.up * -1) * bombSpeed);
    }

    protected void HandleExplodeBomb()
    {
        // Play Explosion Sound
        if (!AudioManager.instance.GetSource("Mine Explosion").isPlaying)
        {
            AudioManager.instance.Play("Mine Explosion");
        }

        // Create the explosion effect at the targetPosition
        GameObject explosion = Instantiate(explosionParticles, transform.position, transform.rotation);

        // Scaleing up the explosion for effect right now
        explosion.transform.localScale = new Vector3(40, 40, 40);

        // Destroy this game object
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        AddForceToColliders();

        HandleExplodeBomb();
    }
}
