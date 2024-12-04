using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class TransporterSystem : MonoBehaviour
{
    [SerializeField] private LayerMask m_LayerMask;
    [SerializeField] private GameObject m_Transporter_Center;
    [SerializeField] private List<Collider> transportAreaItemList;
    [SerializeField] private GameObject[] startingItems;
    [SerializeField] private TMP_Text availableTransportText;
    [SerializeField] private TMP_Text bufferAmountText;
    [SerializeField] private Vector3 transporterPadSize;

    public Queue<GameObject> bufferItems = new Queue<GameObject>();
    private int maxBufferSize = 2;
    private BoxCollider boxCollider;

    public void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        // If starting items provided then we enqueue them
        if (startingItems.Length > 0)
        {
            foreach (GameObject item in startingItems)
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
            for (int i = 0; i < location.currentResources; i++)
            {
                AddItemToBuffer(ShipConditionManager.instance.TransporterPickupArea.spawnObject);
                location.currentResources--;
            }

            location.StartTimer();
        }
    }

    public void LoadNextBufferItem()
    {
        Collider[] hitColliders = Physics.OverlapBox(m_Transporter_Center.transform.position, transporterPadSize / 2, m_Transporter_Center.transform.rotation, m_LayerMask);

        if (bufferItems.Count > 0 && hitColliders.Length <= 0)
        {
            GameObject newItem = bufferItems.Dequeue();
            Object.Instantiate(newItem, transform.position, transform.rotation);
        }
    }

    public void TransportDeliveryItems()
    {
        //Make Item List
        Collider[] hitColliders = Physics.OverlapBox(m_Transporter_Center.transform.position, transporterPadSize / 2, m_Transporter_Center.transform.rotation, m_LayerMask);

        if (ShipConditionManager.instance.TransporterDeliveryArea != null && hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                BoxController boxContoller = hitColliders[i].GetComponent<BoxController>();

                if (GameStateManager.instance.CanDeliverHere(boxContoller, ShipConditionManager.instance.TransporterDeliveryArea))
                {
                    GameStateManager.instance.AddDeliveryPoints(1);

                    Destroy(hitColliders[i].GetComponentInParent<BoxController>().gameObject);
                }
            }
        }
    }

    public void AddItemsToBuffer(GameObject[] newItems)
    {
        if (bufferItems.Count < maxBufferSize)
        {
            foreach (var item in newItems)
            {
                bufferItems.Enqueue(item);
            }
        }
    }

    public void AddItemToBuffer(GameObject newItem)
    {
        if (bufferItems.Count < maxBufferSize)
        {
            bufferItems.Enqueue(newItem);
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    //if (other.bounds.Intersects(boxCollider.bounds))
    //    //{
    //        transportAreaItemList.Add(other);
    //    //}
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    transportAreaItemList.Remove(other);
    //}
}
