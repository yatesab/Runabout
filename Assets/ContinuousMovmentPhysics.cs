using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinuousMovmentPhysics : MonoBehaviour
{
    [Header("Ground Settings")]
    public LayerMask groundLayer;

    [Header("Movement Settings")]
    [SerializeField] private InputActionReference moveInputSource;
    public float maxVelocityChange = 4f;
    public float speed = 5;
    
    [Header("Snap Turn Settings")]    
    [SerializeField] private InputActionReference turnReference;
    public float turnDegrees = 45f;

    private CapsuleCollider _bodyCollider;
    private Rigidbody _body;
    private Transform _directionSource;

    void Start(){
        _body = GetComponent<Rigidbody>();
        _bodyCollider = GetComponentInChildren<CapsuleCollider>();
        _directionSource = transform.Find("Camera Offset");

        turnReference.action.performed += onSnapTurn;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector2 moveInput =  moveInputSource.action.ReadValue<Vector2>();
        Vector3 targetVelocity;

        if (moveInput != Vector2.zero)
        {
            // Calculate how fast we should be moving based on location
            targetVelocity = new Vector3(moveInput.x, 0, moveInput.y);
            targetVelocity = _directionSource.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = _body.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            _body.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    public bool CheckIfGrounded()
    {
        Vector3 start = _bodyCollider.transform.TransformPoint(_bodyCollider.center);
        float rayLength = _bodyCollider.height / 2 - _bodyCollider.radius + 0.05f;

        bool hasHit = Physics.SphereCast(start, _bodyCollider.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHit;
    }

    private void onSnapTurn(InputAction.CallbackContext obj)
    {
        Vector2 turnDirection = obj.ReadValue<Vector2>();

        if (turnDirection.x < 0)
        {
            RotatePlayer(-turnDegrees);
        }
        else if (turnDirection.x > 0)
        {
            RotatePlayer(turnDegrees);
        }
    }

    private void RotatePlayer(float playerRotation)
    {
        _body.transform.Rotate(0f, playerRotation, 0f, Space.Self);
    }
}
