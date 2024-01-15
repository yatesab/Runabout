using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropoffPod : MonoBehaviour
{
    public bool itemTransported = false;
    public Item itemToCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetDropoffItem()
    {
        if(itemToCheck == null) return null;

        return itemToCheck;
    }

    public void TransportDropoffItem()
    {
        itemTransported = true;
    }
}
