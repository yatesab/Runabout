using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupArea : MonoBehaviour
{
    public int locationID;
    public float resourceResetTime;
    public int maxResourceLimit;
    public bool isActive = false;
    public GameObject spawnObject;
}
