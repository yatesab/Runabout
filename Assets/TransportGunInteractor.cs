using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransportGunInteractor : MonoBehaviour
{
    [SerializeField] private InputActionReference triggerAction;
    [SerializeField] private InputActionReference grabAction;
    [SerializeField] private LayerMask transportLayer;

    private LineRenderer lineRenderer;
    private int bufferMaximum = 2;
    private int itemToAccess = 0;
    private int numberOfItems = 0;
    private Rigidbody[] dematerializedList;
    private bool grabButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        triggerAction.action.performed += HandleTransportItem;

        grabAction.action.performed += HandleGrabButtonPressed;
        grabAction.action.canceled += HandleGrabButtonReleased;

        dematerializedList = new Rigidbody[bufferMaximum];
    }

    // Update is called once per frame
    void Update()
    {
        if(numberOfItems > 0)
        {
            dematerializedList[itemToAccess].MovePosition(transform.position + transform.TransformDirection(Vector3.forward) * 2);
        }
    }

    private void HandleTransportItem(InputAction.CallbackContext obj)
    {
        if (grabButtonPressed)
        {
            // Dematerialize
            if (numberOfItems < bufferMaximum)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100f, transportLayer))
                {
                    // Dematerialize
                    dematerializedList[numberOfItems] = hit.rigidbody;
                    numberOfItems++;

                    BoxController controller = hit.rigidbody.gameObject.GetComponent<BoxController>();
                    controller.DeactivateBoxSegments();
                }
            }
        }
        else
        {
            // Rematerialize
            if (numberOfItems > 0)
            {
                Rigidbody newObject = dematerializedList[itemToAccess];
                numberOfItems--;

                BoxController controller = newObject.gameObject.GetComponent<BoxController>();
                controller.ReactivateBoxSegments();
            }
        }
    }

    private void HandleGrabButtonPressed(InputAction.CallbackContext obj)
    {
        grabButtonPressed = true;
    }

    private void HandleGrabButtonReleased(InputAction.CallbackContext obj)
    {
        grabButtonPressed = false;
    }
}
