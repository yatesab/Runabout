using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullSensor : MonoBehaviour
{
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
        if (other.tag == "warp")
        {
            ShipConditionManager.instance.nearWarpGate = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "warp")
        {
            ShipConditionManager.instance.nearWarpGate = false;
        }
    }
}
