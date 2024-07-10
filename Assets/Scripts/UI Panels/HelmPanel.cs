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
        throttleGroup.LeftThrottle.onThrottleChange += UpdatePortThrusterUI;
        throttleGroup.RightThrottle.onThrottleChange += UpdateStarboardThrusterUI;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity.text = physicsShip.Velocity.ToString();
        rotation.text = physicsShip.AngularVelocity.ToString();
    }

    void UpdatePortThrusterUI()
    {
        portPercentage.text = Mathf.Round(throttleGroup.LeftThrottle.ThrottlePercentage * 100).ToString() + " %";
    }

    void UpdateStarboardThrusterUI()
    {
        starboardPercentage.text = Mathf.Round(throttleGroup.RightThrottle.ThrottlePercentage * 100).ToString() + " %";
    }
}
