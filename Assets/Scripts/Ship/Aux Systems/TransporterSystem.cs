using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransporterSystem : MonoBehaviour
{
    public bool canTransport = false;
    public LayerMask transportLayer;

    [Header("Buttons")]
    public Button scanButton;
    public Button engageButton;

    [Header("Transporter Pad")]
    public Transform transporterPad;
    public Transform cargoHold;

    [Header("Panel Objects")]
    public GameObject toggleObject;
    public Transform transportListContainer;
    public ToggleGroup _toggleGroup;

    private float startingYPosition = 130f;
    private int _itemToTransportIndex;

    public void Start()
    {
        scanButton.onClick.AddListener(ScanAreaForTransportItems);
        engageButton.onClick.AddListener(InitiateTransport);

        _toggleGroup = transportListContainer.GetComponent<ToggleGroup>();
    }

    public void Update()
    {
        engageButton.enabled = _itemToTransportIndex != null;
    }

    public void ScanAreaForTransportItems()
    {
        foreach(Transform child in transportListContainer)
        {
            Destroy(child.gameObject);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, 50, transportLayer);

        float yPosition = startingYPosition;
        foreach (Collider collider in colliders)
        {
            TransportLocation transportLocation = collider.GetComponent<TransportLocation>();
            TransportItem[] transportItems = transportLocation.transporterItems;

            for (int i = 0; i < transportItems.Length; i++)
            {
                TransportItem transportItem = transportItems[i];
                if (!transportItem.itemTransported)
                {
                    GameObject listItem = Instantiate(toggleObject, transportListContainer);
                    RectTransform test = listItem.GetComponent<RectTransform>();

                    test.anchoredPosition = new Vector2(test.anchoredPosition.x, yPosition);
                    yPosition -= 25f;

                    Toggle itemToggle = listItem.GetComponent<Toggle>();
                    itemToggle.group = _toggleGroup;
                    itemToggle.isOn = false;
                    itemToggle.onValueChanged.AddListener(delegate { SetSelectedItem(i); });

                    listItem.GetComponentInChildren<Text>().text = transportItem.item.name;
                }
            }
        }
    }

    public void SetSelectedItem(int index)
    {
        _itemToTransportIndex = index;
    }

    public void InitiateTransport()
    {
        //_itemToTransportIndex.HandleCreateItem(transform);
        //transportedItem.transform.parent = cargoHold.transform;
        //transportedItem.transform.position = new Vector3(transporterPad.position.x, transporterPad.position.y + 1, transporterPad.position.z);

        // Update and rescan the list of transport items
        ScanAreaForTransportItems();
    }

    /*public bool InitiateTransport()
    {
        if(pickupPod != null){
            PickupTransport();
            return true;
        } else if(dropoffPod != null)
        {
            DropoffTransport();
            return true;
        }

        return false;
    }

    public void PickupTransport()
    {

        if(!pickupPod.itemTransported)
        {
            Item newItem = pickupPod.GetPickupItem();

            if(cargoHold.AddToCargoHold(newItem))
            {
                pickupPod = null;
                canTransport = false;
            } else 
            {
                Debug.Log("Not Enough Room");
            }
        } else 
        {
            Debug.Log("Nothing To Transport");
        }
    }

    public void DropoffTransport()
    {
        if(!dropoffPod.itemTransported)
        {
            Item checkItem = dropoffPod.GetDropoffItem();

            if(cargoHold.CheckCargoHold(checkItem))
            {
                cargoHold.RemoveItemFromCargo(checkItem);
                dropoffPod.TransportDropoffItem();
                canTransport = false;
                dropoffPod = null;
            } else 
            {
                Debug.Log("Item not in cargo hold");
            }
        } else {
            Debug.Log("Item already delivered");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        pickupPod = other.GetComponent<PickupPod>();
        dropoffPod = other.GetComponent<DropoffPod>();

        if(pickupPod != null && !pickupPod.itemTransported)
        {
            canTransport = true;
        } else if(dropoffPod != null && !dropoffPod.itemTransported)
        {
            canTransport = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        pickupPod = null;
        dropoffPod = null;
        canTransport = false;
    }*/
}
