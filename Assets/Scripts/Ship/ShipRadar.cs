using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRadar : MonoBehaviour
{
    public float RadarRadius { get { return radar.radius; } }
    private SphereCollider radar;

    public List<Collider> TriggerList;

    // Start is called before the first frame update
    void Start()
    {
        TriggerList = new List<Collider>();
        radar = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add object to radar list
        if(!TriggerList.Contains(other))
        {
            TriggerList.Add(other);
        }

    }

    void OnTriggerExit(Collider other)
    {
        // Remove object from radar
        if(TriggerList.Contains(other))
        {
            TriggerList.Remove(other);
        }
    }
}
