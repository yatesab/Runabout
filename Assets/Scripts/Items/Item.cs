using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Transporter System/Item", order = 0)]
public class Item : ScriptableObject
{
    public string Name;
    public float Weight;
    public bool isDelivered;
}
