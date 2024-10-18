using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipConditionManager : MonoBehaviour
{
    public static ShipConditionManager instance { get; private set; }

    [SerializeField] private int deliveryPoints = 0;

    private PhysicsShip physicsShip;


    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Player Condition Manager");
        }
        instance = this;

        physicsShip = GetComponentInChildren<PhysicsShip>();

        LoadShipData();
    }

    public void SaveShipData()
    {
        GameSaveManager.SaveShipData(physicsShip.transform, deliveryPoints);
    }

    public void LoadShipData()
    {
        ShipData shipData = GameSaveManager.LoadShipData();

        Rigidbody shipBody = physicsShip.GetComponent<Rigidbody>();

        shipBody.position = shipData.Position;
        shipBody.rotation = shipData.Rotation;
        deliveryPoints = shipData.deliveryPoints;
    }

    public void AddDeliveryPoints(int newPoints)
    {
        deliveryPoints += newPoints;
    }
}
