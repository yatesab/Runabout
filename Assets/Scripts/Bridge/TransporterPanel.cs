using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TransporterPanel : MonoBehaviour
{
    public CargoHold cargoHold;

    public TMP_Text cargoList;
    public Button pickupButton;
    public Button dropoffButton;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCargoList()
    {
        cargoList.text = "";
        foreach(Item item in cargoHold.itemsInCargoHold )
        {
            cargoList.text = cargoList.text + "\n" + item.Name;
        }
    }

    public void SetPickupButton(bool state)
    {
        pickupButton.enabled = state;
    }

    public void SetDropoffButton(bool state)
    {
        dropoffButton.enabled = state;
    }
}
