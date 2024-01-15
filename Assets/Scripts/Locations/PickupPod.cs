using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPod : MonoBehaviour
{

    public Item pickupItem;

    public bool itemTransported = false;

    private void Start() {
    }

    public Item GetPickupItem()
    {
        if(pickupItem == null) return null;

        return pickupItem;
    }

    public void TransportItem()
    {
        itemTransported = true;
    }
}
