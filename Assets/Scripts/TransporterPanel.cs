using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TransporterPanel : MonoBehaviour
{
    public CargoHold cargoHold;
    public TransporterSystem transporterSystem;

    public TMP_Text cargoList;
    public Button pickupButton;
    public Button dropoffButton;

    // Start is called before the first frame update
    void Start()
    {
        pickupButton.onClick.AddListener(transporterSystem.PickupTransport);
        dropoffButton.onClick.AddListener(transporterSystem.DropoffTransport);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScreen();
    }

    public void UpdateScreen()
    {
        cargoList.text = "";
        foreach(Item item in cargoHold.itemsInCargoHold )
        {
            cargoList.text = cargoList.text + "\n" + item.Name;
        }

        if(transporterSystem != null)
        {
            pickupButton.interactable = transporterSystem.canTransport;
            dropoffButton.interactable = transporterSystem.canTransport;
        }
    }

    public void StartTransport()
    {
        if(transporterSystem.InitiateTransport())
        {
            Debug.Log("Transport Started");
        } else 
        {
            Debug.Log("Issue with Transport");
        }
    }
}
