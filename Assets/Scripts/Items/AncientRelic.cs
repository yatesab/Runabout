using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientRelic : Item
{

    public AncientRelic()
    {
        Name = "Ancient Relic";
        Weight = 50f;
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
