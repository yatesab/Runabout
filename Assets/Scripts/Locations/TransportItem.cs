using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportItem : MonoBehaviour
{

    public Item item;
    public GameObject itemPrefab;
    public bool itemTransported = false;

    private void Start() {
    }

    public Item GetItem()
    {
        if(item == null) return null;

        return item;
    }

    public GameObject HandleTransportItem(Transform parentForItem)
    {
        itemTransported = true;
        return Instantiate(itemPrefab);
    }
}
