using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipStats : MonoBehaviour
{

    public Rigidbody shipOrigin;

    public TMP_Text speedText;
    public TMP_Text rotationText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = "Speed: " + shipOrigin.velocity.ToString();
        rotationText.text = "Rotation: " + shipOrigin.angularVelocity.ToString();
    }
}
