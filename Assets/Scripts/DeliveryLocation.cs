using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryLocation : MonoBehaviour
{
    public int ID {  get; set; }
    public List<StationResource> resources;

    // Start is called before the first frame update
    void Start()
    {
        resources = new List<StationResource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddResource(Item newResource)
    {
        bool isMissingResrouce = true;

        for (int i = 0; i < resources.Count; i++)
        {
            if(resources[i].resourceItem == newResource)
            {
                resources[i].AddResourceAmount();
                isMissingResrouce = false;
            }
        }

        if(isMissingResrouce)
        {
            resources.Add(new StationResource(newResource));
        }
    }

    public void PrintOutResources()
    {
        foreach (StationResource resource in resources)
        {
            Debug.Log(resource.resourceItem.Name);
            Debug.Log(resource.resourceAmount);
        }
    }
}
