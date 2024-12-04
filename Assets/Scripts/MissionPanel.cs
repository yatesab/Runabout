using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text availableTransportText;
    [SerializeField] private TMP_Text availableResourcesText;
    [SerializeField] private Button activatePickupButton;

   public void Update()
    {
        if (ShipConditionManager.instance.TransporterDeliveryArea != null)
        {
            availableTransportText.text = "Delivery Available";
        }
        else
        {
            availableTransportText.text = "Delivery Not Available";
        }

        if (ShipConditionManager.instance.TransporterPickupArea != null)
        {
            if (ShipConditionManager.instance.TransporterPickupArea.isActive)
            {
                availableResourcesText.text = GameStateManager.instance.GetTransporterPickupAreaResourceAmount().ToString();
                activatePickupButton.interactable = false;
            }
            else
            {
                activatePickupButton.interactable = true;
            }
        }
        else
        {
            activatePickupButton.interactable = false;
            availableResourcesText.text = "";
        }

    }

    public void ActivatePickupArea()
    {
        if (ShipConditionManager.instance.TransporterPickupArea.isActive == false)
        {
            // Activate Pickup Area to start timer
            GameStateManager.instance.ActivatePickupLocation();
        }
    }
}
