using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAlias : MonoBehaviour
{
    public Transform shipRigidbody;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = shipRigidbody.position;
        transform.rotation = shipRigidbody.rotation;
    }
}
