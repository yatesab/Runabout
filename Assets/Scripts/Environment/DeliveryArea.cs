using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryArea : MonoBehaviour
{
    public int locationID;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.instance.AddDeliveryLocation(this);
    }
}
