using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterSystem : MonoBehaviour
{
    private SphereCollider transporterCollider;
    public bool canTransport = false;

    public CargoHold cargoHold;

    private PickupPod pickupPod;
    private DropoffPod dropoffPod;

    public bool InitiateTransport()
    {
        if(pickupPod != null){
            PickupTransport();
            return true;
        } else if(dropoffPod != null)
        {
            DropoffTransport();
            return true;
        }

        return false;
    }

    public void PickupTransport()
    {

        if(!pickupPod.itemTransported)
        {
            Item newItem = pickupPod.GetPickupItem();

            if(cargoHold.AddToCargoHold(newItem))
            {
                pickupPod = null;
                canTransport = false;
            } else 
            {
                Debug.Log("Not Enough Room");
            }
        } else 
        {
            Debug.Log("Nothing To Transport");
        }
    }

    public void DropoffTransport()
    {
        if(!dropoffPod.itemTransported)
        {
            Item checkItem = dropoffPod.GetDropoffItem();

            if(cargoHold.CheckCargoHold(checkItem))
            {
                cargoHold.RemoveItemFromCargo(checkItem);
                dropoffPod.TransportDropoffItem();
                canTransport = false;
                dropoffPod = null;
            } else 
            {
                Debug.Log("Item not in cargo hold");
            }
        } else {
            Debug.Log("Item already delivered");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        pickupPod = other.GetComponent<PickupPod>();
        dropoffPod = other.GetComponent<DropoffPod>();

        if(pickupPod != null && !pickupPod.itemTransported)
        {
            canTransport = true;
        } else if(dropoffPod != null && !dropoffPod.itemTransported)
        {
            canTransport = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        pickupPod = null;
        dropoffPod = null;
        canTransport = false;
    }
}
