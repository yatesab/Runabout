using UnityEngine;

public class PhysicsShip : MonoBehaviour
{
    [SerializeField] private Transform shipInterior;

    [SerializeField] private PowerSystem powerSystem;

    [SerializeField] private ShipPart portEngine;
    [SerializeField] private ShipPart starboardEngine;
    [SerializeField] private ShipPart shipBody;
    [SerializeField] private AutomaticDoor shipDoor;

    [SerializeField] private bool shipDocked;
    public Vector3 Velocity { get { return shipRigidbody.velocity; } }
    public Vector3 AngularVelocity { get {  return shipRigidbody.angularVelocity; } }

    private Rigidbody shipRigidbody;
    private bool initiateLiftOff = false;
    private bool initiateDocking = false;
    private Vector3 moveToPosition;
    private Quaternion moveToRotation;
    private bool canDock = false;
    private float lerpPercent;

    // Start is called before the first frame update
    void Start()
    {
        shipRigidbody = GetComponent<Rigidbody>();

        if(!shipDocked)
        {
            powerSystem.TurnOnPower();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(shipDocked)
        {
            SyncShip(shipInterior.position, shipInterior.rotation);
        }

        if (initiateLiftOff)
        {
            SyncShip(Vector3.Slerp(transform.position, moveToPosition, 0.5f * Time.deltaTime), Quaternion.Slerp(transform.rotation, moveToRotation, 0.5f * Time.deltaTime));
            float distance = Vector3.Distance(transform.position, moveToPosition);

            if (distance <= 0f)
            {
                initiateLiftOff = false;

                powerSystem.TurnOnPower();
            }
        }


        if (initiateDocking)
        {
            SyncShip(Vector3.Slerp(transform.position, moveToPosition, 0.5f * Time.deltaTime), Quaternion.Slerp(transform.rotation, moveToRotation, 0.5f * Time.deltaTime));
            float distance = Vector3.Distance(transform.position, moveToPosition);

            if (distance <= 0f)
            {
                shipDoor.forceClosed = false;
                shipDocked = true;

                initiateDocking = false;
            }
        }

        if (portEngine.health <= 0 || starboardEngine.health <= 0 || shipBody.health <= 0)
        {
            // Do Something When Part Is Destroyed
        }
    }

    private void SyncShip(Vector3 newPosition, Quaternion newRotation)
    {
        transform.position = newPosition;
        transform.rotation = newRotation;
    }

    public void InitiateLiftOff()
    {
        shipDoor.forceClosed = true;
        shipDocked = false;

        initiateLiftOff = true;

        moveToPosition = transform.position;
        moveToRotation = transform.rotation;
    }

    public void InitiateDockShip()
    {
        if(canDock)
        {
            powerSystem.ShutOffPower();

            initiateDocking = true;
            moveToPosition = shipInterior.position;
            moveToRotation = shipInterior.rotation;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Landing" && !shipDocked)
        {
            canDock = true;
        }
    }
}
