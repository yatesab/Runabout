using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRadar : MonoBehaviour
{

    private SphereCollider radar;

    public List<Collider> TriggerList;

    // Start is called before the first frame update
    void Start()
    {
        TriggerList = new List<Collider>();
        radar = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if(!TriggerList.Contains(other))
        {
            TriggerList.Remove(other);
        }
    }
}
