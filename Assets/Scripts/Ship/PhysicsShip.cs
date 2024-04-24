using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class PhysicsShip : MonoBehaviour
{
    [SerializeField] private GameConditionManager gameConditionManager;

    [SerializeField] private PowerSystem powerSystem;

    [SerializeField] private ShipPart portEngine;
    [SerializeField] private ShipPart starboardEngine;
    [SerializeField] private ShipPart shipBody;

    [SerializeField] private Transform environment;
    [SerializeField] private Transform meshShip;
    [SerializeField] private Transform skyboxCamera;

    public Vector3 Velocity { get { return shipRigidbody.velocity; } }

    public Vector3 AngularVelocity { get {  return shipRigidbody.angularVelocity; } }

    private Rigidbody shipRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        shipRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (portEngine.health <= 0 || starboardEngine.health <= 0 || shipBody.health <= 0)
        {
            gameConditionManager.ActivateGameOver();
        }
    }

    public void ShutOffPower()
    {
        powerSystem.ShutOffPower();
    }
}
