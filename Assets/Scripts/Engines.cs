using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tilia.Interactions.Controllables.LinearDriver;

public class Engines : MonoBehaviour
{

    public Rigidbody _shipBody;

    public float Pitch { set; get; }
    public float Yaw { set; get; }
    public float Roll { set; get; }
    public float PortThrottle { get; set; }
    public float StarboardThrottle { get; set; }
    public float YawThrottle {  get; set; }

    [Header("Thrust Settings")]
    [SerializeField] private float thrust = 200f;
    [SerializeField] private float thrustGlideReduction = -0.01f;
    [SerializeField] private float thrusterHeatThreshold = 0.75f;

    [Header("Pitch / Yaw / Roll Settings")]
    [SerializeField] private float pitchTorque = 1000f;
    [SerializeField] private float yawTorque = 1000f;
    [SerializeField] private float rollTorque = 1000f;

    private float thrustPercentage = 0f;
    private float glide = 0f;

    public void Update()
    {
        SetThrustPercentage();
    }

    public void FixedUpdate()
    {
        OnRotateShip();
        OnThrustShip();
    }

    private void AddForceToShip()
    {
        _shipBody.AddForce(_shipBody.transform.forward * thrustPercentage * thrust * Time.fixedDeltaTime, ForceMode.Force);
        glide = thrustPercentage;
    }

    private void OnThrustShip()
    {
        if (thrustPercentage > 0.1f)
        {
            if (!AudioManager.instance.GetSource("Ship Engines").isPlaying)
            {
                AudioManager.instance.Play("Ship Engines");
            }

            //AudioManager.instance.GetSource("Ship Engines").volume = 0.4f * thrustPercentage;

            AddForceToShip();
        }
        else
        {
            // Stop Sound from engines
            AudioManager.instance.Stop("Ship Engines");

            // Slow forces on ship
            _shipBody.AddForce(Vector3.back * glide * Time.fixedDeltaTime, ForceMode.Force);
            glide *= thrustGlideReduction;
        }
    }

    private void OnRotateShip()
    {
        //float powerLevel = GetCurrentPowerLevels();
        // Pitch
        _shipBody.AddRelativeTorque(Vector3.right * -Pitch * pitchTorque * Time.fixedDeltaTime);
        // Yaw
        _shipBody.AddRelativeTorque(Vector3.up * (Yaw + YawThrottle) * yawTorque * Time.fixedDeltaTime);
        // Roll
        _shipBody.AddRelativeTorque(Vector3.back * Roll * rollTorque * Time.fixedDeltaTime);
    }

    public void SetThrustPercentage()
    {
        thrustPercentage = Mathf.Max(PortThrottle, StarboardThrottle) - YawThrottle;
    }
}
