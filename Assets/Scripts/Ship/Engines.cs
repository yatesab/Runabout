using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engines : MonoBehaviour
{
    public float Pitch { set; get; }
    public float Yaw { set; get; }
    public float Roll { set; get; }
    public float PortThrottle { get; set; }
    public float StarboardThrottle { get; set; }
    public float YawThrottle {  get; set; }
    public float LeftRightStrafe { get; set; }
    public float UpDownStrafe { get; set; }

    [SerializeField] private Rigidbody _shipBody;
    [SerializeField] private PowerSystem powerSystem;

    [Header("Thrust Settings")]
    [SerializeField] private float thrust = 200f;

    [Header("Pitch / Yaw / Roll Settings")]
    [SerializeField] private float pitchTorque = 1000f;
    [SerializeField] private float yawTorque = 1000f;
    [SerializeField] private float rollTorque = 1000f;

    [Header("Strafe Settings")]
    [SerializeField] private float strafeAmount;


    private float thrustPercentage = 0f;
    private AudioControl audioControl;

    public void Start()
    {
        audioControl = GetComponent<AudioControl>();
    }

    public void Update()
    {
        SetThrustPercentage();
    }

    public void FixedUpdate()
    {
        if(powerSystem.EngineTotalPower > 0)
        {
            OnRotateShip();
            OnThrustShip();
            OnStrafeShip();
        }
    }

    private void AddForceToShip()
    {
        float totalThrust = thrust * powerSystem.EngineTotalPower;
        _shipBody.AddForce(_shipBody.transform.forward * totalThrust * thrustPercentage * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void OnStrafeShip()
    {
        
        if(LeftRightStrafe > 0f)
        {
            _shipBody.AddForce(_shipBody.transform.right * strafeAmount * LeftRightStrafe * Time.fixedDeltaTime, ForceMode.Force);
        } else if (UpDownStrafe > 0f)
        {
            _shipBody.AddForce(_shipBody.transform.up * strafeAmount * UpDownStrafe * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    private void OnThrustShip()
    {
        if (thrustPercentage >= 0.1f)
        {
            if (!audioControl.GetSource("Engines").isPlaying)
            {
                audioControl.Play("Engines");
            }

            audioControl.GetSource("Engines").volume = 0.4f * thrustPercentage;

            AddForceToShip();
        }
        else
        {
            if (audioControl.GetSource("Engines").isPlaying)
            {
                //Stop Sound from engines
                audioControl.Stop("Engines");
            }
        }
    }

    private void OnRotateShip()
    {
        // Pitch
        float pitchAmount = pitchTorque * powerSystem.EngineTotalPower;
        _shipBody.AddRelativeTorque(Vector3.right * pitchAmount * Pitch * Time.fixedDeltaTime);

        // Yaw
        float yawAmount = yawTorque * powerSystem.EngineTotalPower;
        _shipBody.AddRelativeTorque(Vector3.up * yawAmount * (Yaw + YawThrottle) * Time.fixedDeltaTime);

        // Roll
        float rollAmount = rollTorque * powerSystem.EngineTotalPower;
        _shipBody.AddRelativeTorque(Vector3.back * rollAmount * Roll * Time.fixedDeltaTime);
    }

    public void SetThrustPercentage()
    {
        thrustPercentage = Mathf.Max(PortThrottle, StarboardThrottle) - YawThrottle;
    }
}
