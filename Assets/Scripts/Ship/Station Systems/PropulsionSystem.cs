using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulsionSystem : StationSystem
{
    public Rigidbody _shipBody;
    public FlipSwitch _reverseSwitch;

    [Header("Thrust Settings")]
    [SerializeField] private float thrust = 300f;
    [SerializeField] private float thrustGlideReduction = -0.01f;
    [SerializeField] private float thrusterHeatThreshold = 0.75f;

    [Header("Pitch / Yaw / Roll Settings")]
    [SerializeField] private float pitchTorque = 1000f;
    [SerializeField] private float yawTorque = 1000f;
    [SerializeField] private float rollTorque = 1000f;

    [Header("Strafe Settings")]
    [SerializeField] private float strafeThrust = 50f;
    [SerializeField] private float leftRightGlideReduction = 0.111f;
    [SerializeField] private float upDownGlideReduction = 0.05f;

    private float yawRotation = 0f;
    private float pitchRotation = 0f;
    private float rollRotation = 0f;
    private float leftThrustPercentage = 0f;
    private float rightThrustPercentage = 0f;
    private float highestPercentage = 0f;
    private float thrustDiff = 0f;
    private float glide = 0f;
    private float horizontalGlide = 0f;
    private float verticalGlide = 0f;

    private bool startStrafing = false;
    private bool startUpDown = false;

    // Start is called before the first frame update
    void Update()
    {
        if(highestPercentage > thrusterHeatThreshold && HeatLevel < maxHeatLevel)
        {
            isHeating = true;
            AddHeat(Time.deltaTime * PowerLevel);
        } else
        {
            isHeating = false;
        }

        if(!isOverheated && HeatLevel >= maxHeatLevel)
        {
            isOverheated = true;
        }
    }

    private void FixedUpdate()
    {
        OnRotateShip();

        OnThrustShip();

        OnStrafeShip();

        OnUpDownShip();
    }

    /**
        Thrust Handle Methods
    */
    private void AddForceToShip()
    {
        // If reverse switch is flipped then make the percentage negative
        float currentThrustPercentage = highestPercentage;
        if (_reverseSwitch.switchOn)
        {
            currentThrustPercentage *= -1;
        }

        // Need to correct power level if system is overheated
        float currentPowerLevel = PowerLevel;
        if (isOverheated)
        {
            currentPowerLevel -= 0.5f;
        }

        float currentThrust = thrust * currentThrustPercentage * currentPowerLevel;

        _shipBody.AddForce(_shipBody.transform.forward * currentThrust * Time.fixedDeltaTime, ForceMode.Impulse);
        glide = currentThrust;
    }

    private void OnThrustShip()
    {
        if(highestPercentage > 0.1f || highestPercentage < -0.1f)
        {
            if(!AudioManager.instance.GetSource("Ship Engines").isPlaying)
            {
                AudioManager.instance.Play("Ship Engines");
            }

            AudioManager.instance.GetSource("Ship Engines").volume = 0.4f * highestPercentage;

            AddForceToShip();
        } else {
            // Stop Sound from engines
            AudioManager.instance.Stop("Ship Engines");

            // Slow forces on ship
            _shipBody.AddForce(Vector3.back * glide * Time.deltaTime, ForceMode.Force);
            glide *= thrustGlideReduction;
        }
    }

    private void OnRotateShip()
    {
        // Pitch
        _shipBody.AddRelativeTorque(Vector3.right * -pitchRotation * pitchTorque * PowerLevel * Time.deltaTime);
        // Yaw
        _shipBody.AddRelativeTorque(Vector3.up * yawRotation * yawTorque * PowerLevel * Time.deltaTime);
        // Roll
        _shipBody.AddRelativeTorque(Vector3.back * rollRotation * rollTorque * PowerLevel * Time.deltaTime);
    }

    private void OnStrafeShip()
    {
        if(startStrafing)
        {
            _shipBody.AddRelativeForce(Vector3.right * strafeThrust * PowerLevel * Time.fixedDeltaTime);
            horizontalGlide = strafeThrust;
        } else 
        {
            _shipBody.AddRelativeForce(Vector3.right * horizontalGlide * Time.fixedDeltaTime);
            horizontalGlide *= leftRightGlideReduction;
        }
    }

    private void OnUpDownShip()
    {
        if(startUpDown)
        {
            _shipBody.AddRelativeForce(Vector3.up * strafeThrust * PowerLevel * Time.fixedDeltaTime);
            verticalGlide = strafeThrust;
        } else 
        {
            _shipBody.AddRelativeForce(Vector3.up * verticalGlide * Time.fixedDeltaTime);
            verticalGlide *= upDownGlideReduction;
        }
    }

    public void SetThrustPercentage(float newPercentage)
    {
        highestPercentage = newPercentage;
    }

    public void SetLeftThrustPercentage(float newPercentage)
    {
        leftThrustPercentage = newPercentage;
        highestPercentage = Mathf.Max(leftThrustPercentage, rightThrustPercentage);
        thrustDiff = leftThrustPercentage - rightThrustPercentage;
    }

    public void SetRightThrustPercentage(float newPercentage)
    {
        rightThrustPercentage = newPercentage;
        highestPercentage = Mathf.Max(leftThrustPercentage, rightThrustPercentage);
        thrustDiff = leftThrustPercentage - rightThrustPercentage;
    }

    public void SetYawRotation(float newYaw)
    {
        yawRotation = newYaw;
    }

    public void SetPitchRotation(float newPitch)
    {
        pitchRotation = newPitch;
    }

    public void SetRollRotation(float newRoll)
    {
        rollRotation = newRoll;
    }

    public void SetStrafingStarted(bool isStrafing)
    {
        startStrafing = isStrafing;
    }

    public void SetUpDownStarted(bool isUpDown)
    {
        startUpDown = isUpDown;
    }
}
