using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadarScreen : MonoBehaviour
{
    [SerializeField] private ShipRadar shipRadar;
    [SerializeField] private float displayRadius;
    [SerializeField] private GameObject radarBlip;

    private List<GameObject> blipCollection = new List<GameObject>();

    public void Start()
    {
        shipRadar.OnAddRadarTarget += AddBlip;
        shipRadar.OnRemoveRadarTarget += RemoveBlip;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            UpdateBlips();
        } catch(System.Exception e)
        {
            Debug.Log(e.ToString());
            
            shipRadar.ScanRadius();
            UpdateBlipList();

            UpdateBlips();
        }
    }

    private void UpdateBlips()
    {
        for (var i = 0; i < shipRadar.TriggerList.Count; i++)
        {
            Vector3 newLocation = CalculateLocation(shipRadar.transform.InverseTransformPoint(shipRadar.TriggerList[i].transform.position));
            blipCollection[i].transform.localPosition = newLocation;
        }
    }

    private void UpdateBlipList()
    {
        foreach(GameObject blip in blipCollection)
        {
            Destroy(blip);
        }

        blipCollection.Clear();
        foreach(Collider collider in shipRadar.TriggerList)
        {
            blipCollection.Add(Object.Instantiate(radarBlip, transform, false));
        }
    }

    private void AddBlip()
    {
        blipCollection.Add(Object.Instantiate(radarBlip, transform, false));
    }

    private void RemoveBlip()
    {
        foreach (GameObject blip in blipCollection.Skip(shipRadar.TriggerList.Count))
        {
            Destroy(blip);
        }

        blipCollection = blipCollection.Take(shipRadar.TriggerList.Count).ToList();
    }

    private Vector3 CalculateLocation(Vector3 triggerPosition)
    {
        Vector3 distancePercentage = triggerPosition / shipRadar.RadarRadius;

        // Spawn a thing here
        return displayRadius * distancePercentage;
    }
}
