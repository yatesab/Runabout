using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance { get; private set; }

    [SerializeField] private int deliveryPoints = 0;
    [SerializeField] private TMP_Text dpDisplayText;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject timerGameObject;

    private int deliverySiteAmount;
    private int pickupSiteAmount;
    private int totalPackageAmount;
    private int currentIndex = 0;
    private List<BoxController> boxesList;
    private List<DeliveryArea> deliveryAreas;
    private List<PickupLocation> pickupLocations;
    private List<DeliveryLocation> deliveryLocations;
    private Mission currentMission;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Player Condition Manager");
        }
        instance = this;

        deliverySiteAmount = 3;
        pickupSiteAmount = 1;
        totalPackageAmount = 3;

        boxesList = new List<BoxController>();
        deliveryAreas = new List<DeliveryArea>();
        pickupLocations = new List<PickupLocation>();
        deliveryLocations = new List<DeliveryLocation>();

        DeliveryLocation newLocation = timerGameObject.AddComponent<DeliveryLocation>();
        newLocation.ID = 0;
        deliveryLocations.Add(newLocation);

        DeliveryLocation newLocation2 = timerGameObject.AddComponent<DeliveryLocation>();
        newLocation2.ID = 1;
        deliveryLocations.Add(newLocation2);
    }

    public void Update()
    {
        dpDisplayText.text = deliveryPoints.ToString();
    }

    public void AddDeliveryPoints(int newPoints)
    {
        deliveryPoints += newPoints;
    }

    public void AddDeliveryLocation(DeliveryArea area)
    {
        deliveryAreas.Add(area);
    }

    public void ActivatePickupLocation()
    {
        PickupArea localPickupArea = ShipConditionManager.instance.TransporterPickupArea;
        if (localPickupArea != null)
        {
            PickupLocation newLocation = timerGameObject.AddComponent<PickupLocation>();
            newLocation.ID = localPickupArea.locationID;
            newLocation.MaxResources = localPickupArea.maxResourceLimit;
            newLocation.MaxResetTime = localPickupArea.resourceResetTime;
            localPickupArea.isActive = true;

            newLocation.StartTimer();
            pickupLocations.Add(newLocation);
            Debug.Log("New Pickup Area Activated");
        } else
        {
            Debug.LogError("No Pickup Area To Activate");
        }
    }

    public bool CanDeliverHere(BoxController deliveryBox, DeliveryArea location)
    {
        int? foundArea = currentMission.deliveryLocations.Find(area => area == location.locationID);
        int? foundBox = currentMission.boxList.Find(box => box == deliveryBox.BoxID);

        return foundArea != null && foundBox != null;
    }

    public PickupLocation? GetPickupLocation(int locationID)
    {
        foreach(PickupLocation location in pickupLocations)
        {
            if (location.ID == locationID)
            {
                return location;
            }
        }

        return null;
    }

    public int GetTransporterPickupAreaResourceAmount()
    {
        PickupLocation location = GetPickupLocation(ShipConditionManager.instance.TransporterPickupArea.locationID);

        return location.currentResources;
    }
    
    public DeliveryLocation? GetDeliveryLocation(int locationID)
    {
        foreach (DeliveryLocation location in deliveryLocations)
        {
            if (location.ID == locationID)
            {
                return location;
            }
        }

        return null;
    }
}
