using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance { get; private set; }

    [SerializeField] private int deliveryPoints = 0;
    [SerializeField] private TMP_Text dpDisplayText;
    [SerializeField] private GameObject[] pickupArray;

    private int deliverySiteAmount;
    private int pickupSiteAmount;
    private int totalPackageAmount;

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
    }

    public void Update()
    {
        dpDisplayText.text = deliveryPoints.ToString();
    }

    public void AddDeliveryPoints(int newPoints)
    {
        deliveryPoints += newPoints;
    }
}
