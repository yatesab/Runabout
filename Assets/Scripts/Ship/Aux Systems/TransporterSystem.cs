using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransporterSystem : MonoBehaviour
{
    public LayerMask transportLayer;

    [SerializeField] private CargoHold cargoHold;

    public void ScanAreaForTransportItems()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100, transportLayer);

        foreach (Collider collider in colliders)
        {
            TransportLocation transportLocation = collider.GetComponent<TransportLocation>();

            foreach (Item transportItem in transportLocation.transporterItems)
            {
                // Eventually this will just fill a list that is used.
                cargoHold.AddToCargoHold(transportItem);
            }

            transportLocation.transporterItems = new Item[0];
        }
    }

    public void InitiateTransport()
    {
        ScanAreaForTransportItems();
    }
}
