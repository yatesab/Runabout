using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGoo : Item
{

    public AlienGoo()
    {
        Name = "Alien Goo";
        Weight = 25f;
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
