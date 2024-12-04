using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterSensor : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "delivery" || other.tag == "pickup")
        {
            DeliveryArea deliveryArea = other.GetComponent<DeliveryArea>();
            if(deliveryArea != null )
            {
                ShipConditionManager.instance.TransporterDeliveryArea = deliveryArea;
            } else
            {
                PickupArea pickupArea = other.GetComponent<PickupArea>();
                if (pickupArea != null)
                {
                    ShipConditionManager.instance.TransporterPickupArea = pickupArea;
                }
            }
        }
    }   
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "delivery" || other.tag == "pickup")
        {
            DeliveryArea deliveryArea = other.GetComponent<DeliveryArea>();
            if (deliveryArea != null)
            {
                ShipConditionManager.instance.TransporterDeliveryArea = null;
            }
            else
            {
                PickupArea pickupArea = other.GetComponent<PickupArea>();
                if (pickupArea != null)
                {
                    ShipConditionManager.instance.TransporterPickupArea = null;
                }
            }
        }
    }
}
