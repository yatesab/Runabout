using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCrate : Item
{
    public CrystalCrate()
    {
        Name = "Create Of Crystals";
        Weight = 70f;
    }

    public string Name 
    {
        get;
        set;
    }
    public float Weight
    {
        get;
        set;
    }
}
