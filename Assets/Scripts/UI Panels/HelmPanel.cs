using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelmPanel : MonoBehaviour
{
    public ThrottleGroup throttleGroup;
    public PhysicsShip physicsShip;
    
    public TMP_Text portPercentage;
    public TMP_Text starboardPercentage;
    public TMP_Text velocity;
    public TMP_Text rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        portPercentage.text = Mathf.Round(throttleGroup.LeftThrottlePercentage * 100).ToString() + " %";
        starboardPercentage.text = Mathf.Round(throttleGroup.RightThrottlePercentage * 100).ToString() + " %";
        velocity.text = physicsShip.Velocity.ToString();
        rotation.text = physicsShip.AngularVelocity.ToString();
    }
}
