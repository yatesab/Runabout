using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRadar : MonoBehaviour
{
    public float RadarRadius { get { return radar.radius; } }
    private SphereCollider radar;

    public List<Collider> TriggerList;
    public event Action OnAddRadarTarget;
    public event Action OnRemoveRadarTarget;

    // Start is called before the first frame update
    void Start()
    {
        TriggerList = new List<Collider>();
        radar = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add object to radar list
        if(!TriggerList.Contains(other) && other.tag != "proximity")
        {
            TriggerList.Add(other);
            OnAddRadarTarget.Invoke();
        }

    }

    void OnTriggerExit(Collider other)
    {
        // Remove object from radar
        if(TriggerList.Contains(other) && other.tag != "proximity")
        {
            TriggerList.Remove(other);
            OnRemoveRadarTarget.Invoke();
        }
    }

    public void ScanRadius()
    {
        List<Collider> newList = new List<Collider>(Physics.OverlapSphere(transform.position, radar.radius, radar.includeLayers));

        TriggerList.Clear();
        foreach (Collider c in newList)
        {
            if(c.tag != "proximity")
            {
                TriggerList.Add(c);
            }
        }
    }
}
