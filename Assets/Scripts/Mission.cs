using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public List<int> deliveryLocations;
    public List<int> boxList;

    // Start is called before the first frame update
    public Mission()
    {
        deliveryLocations = new List<int>();
        boxList = new List<int>();
    }

    public void GenerateDeliveryLocations(int numberOfLocations)
    {
        for(int i = 0; i < numberOfLocations;i++)
        {
            deliveryLocations.Add(0);
        }
    }

    public void GenerateBoxes(int numberOfBoxes)
    {
        for (int i = 0; i < numberOfBoxes; i++)
        {
            boxList.Add(0);
        }
    }
}
