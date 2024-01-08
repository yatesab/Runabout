using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoHold : MonoBehaviour
{

    public float maxWeight = 120f;
    public float currentWeight = 0f;
    public List<Item> itemsInCargoHold = new List<Item>();

    public bool AddToCargoHold(Item newItem)
    {
        float newMaxWeight = newItem.Weight + currentWeight;
        if(!(newMaxWeight > maxWeight))
        {
            currentWeight += newItem.Weight;
            itemsInCargoHold.Add(newItem);

            return true;
        } else 
        {
            return false;
        }
    }

    public bool RemoveItemFromCargo()
    {
        return true;
    }
}
