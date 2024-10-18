using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransporterSystem : MonoBehaviour
{
    [SerializeField] private LayerMask m_LayerMask;
    [SerializeField] private List<Collider> teleportAreaItemList;
    [SerializeField] private GameObject[] startingItems;

    private Queue<GameObject> bufferItems = new Queue<GameObject>();
    private int maxBufferSize = 2;

    public void Start()
    {
        // If starting items provided then we enqueue them
        if (startingItems.Length > 0)
        {
            foreach (var item in startingItems)
            {
                bufferItems.Enqueue(item);
            }
        }
    }

    public void LoadNextBufferItem()
    {
        if (bufferItems.Count > 0 && teleportAreaItemList.Count <= 0)
        {
            GameObject newItem = bufferItems.Dequeue();
            Object.Instantiate(newItem, transform.position, transform.rotation);
        }
    }

    public void TransportDeliveryItems()
    {
        if (Physics.CheckSphere(transform.position, 100f, m_LayerMask, QueryTriggerInteraction.Collide) && teleportAreaItemList.Count > 0)
        {
            for (int i = 0; i < teleportAreaItemList.Count; i++)
            {
                ShipConditionManager.instance.AddDeliveryPoints(1);

                Destroy(teleportAreaItemList[i]);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        teleportAreaItemList.Add(other);
    }

    public void OnTriggerExit(Collider other)
    {
        teleportAreaItemList.Remove(other);
    }
}
