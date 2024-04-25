using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoHold : MonoBehaviour
{
    [SerializeField] private CargoPanel cargoPanel;

    public float maxWeight = 120f;
    public float currentWeight = 0f;
    public List<Item> itemsInCargoHold = new List<Item>();

    public bool AddToCargoHold(Item _item)
    {
        float newMaxWeight = _item.Weight + currentWeight;
        if(!(newMaxWeight > maxWeight))
        {
            currentWeight += _item.Weight;
            itemsInCargoHold.Add(_item);

            cargoPanel.UpdateList(itemsInCargoHold);

            return true;
        } else 
        {
            return false;
        }
    }

    public bool CheckCargoHold(Item checkItem)
    {
        return itemsInCargoHold.Contains(checkItem);
    }

    public void RemoveItemFromCargo(Item checkItem)
    {
        itemsInCargoHold.Remove(checkItem);
    }
}
