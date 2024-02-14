using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportItem : MonoBehaviour
{

    public Item pickupItem;

    public bool itemTransported = false;

    private void Start() {
    }

    public Item GetItem()
    {
        if(pickupItem == null) return null;

        return pickupItem;
    }

    public void HandleTransportItem()
    {
        itemTransported = true;
    }
}
