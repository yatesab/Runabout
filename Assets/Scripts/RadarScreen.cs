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

    // Update is called once per frame
    void Update()
    {
        float arrayCount = shipRadar.TriggerList.Count < blipCollection.Count ? blipCollection.Count : shipRadar.TriggerList.Count;

        for (var i = 0; i < arrayCount; i++)
        {
            if (shipRadar.TriggerList.Count <= i)
            {
                foreach(GameObject blip in blipCollection.Skip(i))
                {
                    Destroy(blip);
                }

                blipCollection = blipCollection.Take(i).ToList();
                break;
            }
            else
            {
                if (blipCollection.Count <= i)
                {
                    blipCollection.Add(Object.Instantiate(radarBlip, transform, false));
                }

                blipCollection[i].transform.localPosition = CalculateLocation(shipRadar.transform.InverseTransformPoint(shipRadar.TriggerList[i].transform.position));
            }
        }
    }

    private Vector3 CalculateLocation(Vector3 triggerPosition)
    {
        Vector3 distancePercentage = triggerPosition / shipRadar.RadarRadius;

        // Spawn a thing here
        return displayRadius * distancePercentage;
    }
}
