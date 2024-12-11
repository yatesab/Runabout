using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationResource
{
    public Item resourceItem;
    public int resourceAmount;

    public StationResource(Item startingItem)
    {
        resourceItem = startingItem;
        resourceAmount = 1;
    }

    public void AddResourceAmount()
    {
        resourceAmount++;
    }
}
