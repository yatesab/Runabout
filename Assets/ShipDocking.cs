using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDocking : MonoBehaviour
{
    public  bool ShipDocked { get; set; } = true;
    private bool liftOffInitiated = false;
    private Vector3 liftLocation = new Vector3(0, 4, 16);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(liftOffInitiated)
        {
            transform.position = Vector3.Lerp(transform.position, liftLocation, 0.5f * Time.fixedDeltaTime);
            
            if (transform.position.y > liftLocation.y -0.1)
            {
                liftOffInitiated = false;

                ShipDocked = false;
            }
        }
    }

    public void InitiateLiftOff()
    {
        liftOffInitiated = true;
        liftLocation = transform.position + new Vector3(0f, 2f, 0f);
    }
}
