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

    [SerializeField] private Rigidbody _shipBody;
    [SerializeField] private PowerSystem powerSystem;

    [Header("Thrust Settings")]
    [SerializeField] private float thrust = 200f;
    [SerializeField] private float thrustGlideReduction = -0.01f;

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
        if(powerSystem.EngineTotalPower > 0)
        {
            OnRotateShip();
            OnThrustShip();
        }
    }

    private void AddForceToShip()
    {
        float totalThrust = thrust * powerSystem.EngineTotalPower;
        _shipBody.AddForce(_shipBody.transform.forward * totalThrust * thrustPercentage * Time.fixedDeltaTime, ForceMode.Force);
        glide = thrust * thrustPercentage;
    }

    private void OnThrustShip()
    {
        if (thrustPercentage >= 0.1f)
        {
            //if (!AudioManager.instance.GetSource("Ship Engines").isPlaying)
            //{
            //    AudioManager.instance.Play("Ship Engines");
            //}

            //AudioManager.instance.GetSource("Ship Engines").volume = 0.4f * thrustPercentage;

            AddForceToShip();
        }
        else
        {
            //if (AudioManager.instance.GetSource("Ship Engines").isPlaying)
            //{
                // Stop Sound from engines
                //AudioManager.instance.Stop("Ship Engines");
            //}

            // Slow forces on ship
            _shipBody.AddForce(Vector3.back * glide * Time.fixedDeltaTime, ForceMode.Force);
            glide *= thrustGlideReduction;
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
