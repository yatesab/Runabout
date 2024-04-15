using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] private ShipPart portEngine;
    [SerializeField] private ShipPart starboardEngine;
    [SerializeField] private ShipPart shipBody;

    public void Update()
    {
        if(portEngine.health <= 0)
        {

        }

        if (starboardEngine.health <= 0)
        {

        }

        if (shipBody.health <= 0)
        {

        }
    }
}
