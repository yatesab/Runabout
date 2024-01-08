using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterSystem : ShipSystem
{

    private SphereCollider transporterCollider;
    public bool canTransport = false;
    public TransporterPanel transporterPanel;

    public CargoHold cargoHold;

    private PickupPod pickupPod;
    

    public void PickupTransport()
    {
        Item newItem = pickupPod.GetSetItem();

        if(newItem != null)
        {
            if(cargoHold.AddToCargoHold(newItem))
            {
                transporterPanel.UpdateCargoList();
                transporterPanel.SetPickupButton(false);
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
        cargoHold.RemoveItemFromCargo();
        transporterPanel.UpdateCargoList();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        pickupPod = other.GetComponent<PickupPod>();

        if(pickupPod != null && pickupPod.setItem != null)
        {
            transporterPanel.SetPickupButton(true);
            canTransport = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        pickupPod = null;
        transporterPanel.SetPickupButton(false);
        canTransport = false;
    }
}
