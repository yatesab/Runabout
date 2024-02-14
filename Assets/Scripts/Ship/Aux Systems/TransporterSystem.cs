using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransporterSystem : MonoBehaviour
{
    public bool canTransport = false;
    public Button scanButton;
    public Transform transportListContainer;
    public GameObject toggleObject;

    public LayerMask transportLayer;

    private ToggleGroup _toggleGroup;
    private float startingYPosition = 130f;

    public void Start()
    {
        scanButton.onClick.AddListener(ScanAreaForTransportItems);
        _toggleGroup = transportListContainer.GetComponent<ToggleGroup>();
    }

    public void Update()
    {
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
            TransportItem[] transportItems = collider.GetComponents<TransportItem>();
            foreach (TransportItem transportItem in transportItems)
            {
                GameObject listItem = Instantiate(toggleObject, transportListContainer);
                RectTransform test = listItem.GetComponent<RectTransform>();

                test.anchoredPosition = new Vector2(test.anchoredPosition.x, yPosition);
                yPosition -= 20f;

                Toggle itemToggle = listItem.GetComponent<Toggle>();
                itemToggle.group = _toggleGroup;
                itemToggle.isOn = false;

                Item pickupItem = transportItem.GetItem();
                listItem.GetComponentInChildren<Text>().text = pickupItem.Name;
            }
        }
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
