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
    [SerializeField] private TMP_Text availableTransportText;

    public Queue<GameObject> bufferItems = new Queue<GameObject>();
    private int maxBufferSize = 2;

    public Collider teleportLocation { get; set; }

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

    public void Update()
    {
        if (teleportLocation != null)
        {
            availableTransportText.text = "Available";
        } else
        {
            availableTransportText.text = "Not Available";
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
        if (teleportLocation != null && teleportAreaItemList.Count > 0)
        {
            for (int i = 0; i < teleportAreaItemList.Count; i++)
            {
                GameStateManager.instance.AddDeliveryPoints(1);

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
