using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterSensor : MonoBehaviour
{
    public TransporterSystem transportSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "delivery")
        {
            Debug.Log("Hit Delivery");
            transportSystem.teleportLocation = other;
        }

        if(other.tag == "pickup")
        {
            Debug.Log("Hit Pickup");
            transportSystem.AddItemsToBuffer(other.GetComponent<PickupSite>().packages);
        }
    }   
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "delivery")
        {
            Debug.Log("Exit Delivery");
            transportSystem.teleportLocation = null;
        }
    }
}
