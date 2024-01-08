using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPod : MonoBehaviour
{

    public Item setItem;

    private void Start() {
        setItem = new AlienGoo();
    }

    public Item GetSetItem()
    {
        if(setItem == null) return null;

        setItem = null;
        return setItem;
    }
}
