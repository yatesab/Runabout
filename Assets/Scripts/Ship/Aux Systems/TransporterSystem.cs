using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class TransporterSystem : MonoBehaviour
{
    [SerializeField] private LayerMask interior_LayerMask;
    [SerializeField] private LayerMask exterior_LayerMask;

    [SerializeField] private GameObject m_Transporter_Center;
    [SerializeField] private List<Collider> transportAreaItemList;
    [SerializeField] private Item[] startingItems;
    [SerializeField] private TMP_Text availableTransportText;
    [SerializeField] private TMP_Text bufferAmountText;
    [SerializeField] private Vector3 transporterPadSize;

    public Queue<Item> bufferItems = new Queue<Item>();
    private int maxBufferSize = 2;
    private BoxCollider boxCollider;

    public void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        // If starting items provided then we enqueue them
        if (startingItems.Length > 0)
        {
            foreach (Item item in startingItems)
            {
                bufferItems.Enqueue(item);
            }
        }
    }

    public void Update()
    {
        if (ShipConditionManager.instance.TransporterDeliveryArea != null)
        {
            availableTransportText.text = "Available";
        } else
        {
            availableTransportText.text = "Not Available";
        }

        bufferAmountText.text = bufferItems.Count.ToString();
    }

    public void PullItemsIntoBuffer()
    {
        if(ShipConditionManager.instance.TransporterPickupArea != null)
        {
            PickupLocation location = GameStateManager.instance.GetPickupLocation(ShipConditionManager.instance.TransporterPickupArea.locationID);
                        
            // Add open spots to ship buffer
            for (int i = 0; i <= location.currentResources; i++)
            {
                AddItemToBuffer(ShipConditionManager.instance.TransporterPickupArea.locationItem);
                location.currentResources--;
            }

            if (location.IsTimerRunning == false)
            {
                location.StartTimer();
            }
        }
    }

    public void LoadNextBufferItem()
    {
        Collider[] hitColliders = Physics.OverlapBox(m_Transporter_Center.transform.position, transporterPadSize / 2, m_Transporter_Center.transform.rotation, interior_LayerMask);

        if (bufferItems.Count > 0 && hitColliders.Length <= 0)
        {
            Item newItem = bufferItems.Dequeue();
            GameObject newBox = Object.Instantiate(newItem.itemObject, transform.position, transform.rotation);

            newBox.GetComponent<BoxController>().boxItem = newItem;
        }
    }

    public void TransportDeliveryItems()
    {
        //Make Item List
        Collider[] hitColliders = Physics.OverlapBox(m_Transporter_Center.transform.position, transporterPadSize / 2, m_Transporter_Center.transform.rotation, interior_LayerMask);

        if (ShipConditionManager.instance.TransporterDeliveryArea != null && hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                BoxController boxContoller = hitColliders[i].GetComponent<BoxController>();
                DeliveryLocation location = GameStateManager.instance.GetDeliveryLocation(ShipConditionManager.instance.TransporterDeliveryArea.locationID);

                if (location)
                {
                    location.AddResource(boxContoller.boxItem);
                    location.PrintOutResources();

                    Destroy(hitColliders[i].GetComponentInParent<BoxController>().gameObject);
                }
            }
        }
    }

    public void AddItemsToBuffer(Item[] newItems)
    {
        if (bufferItems.Count < maxBufferSize)
        {
            foreach (Item item in newItems)
            {
                bufferItems.Enqueue(item);
            }
        }
    }

    public void AddItemToBuffer(Item newItem)
    {
        if (bufferItems.Count < maxBufferSize)
        {
            bufferItems.Enqueue(newItem);
        }
    }
}
