using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryArea : MonoBehaviour
{
    private Collider box;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if(other.bounds.Intersects(box.bounds))
        {
            
        }
    }
}
