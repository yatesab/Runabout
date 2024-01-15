using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulsionSystem : CoreSystem
{
    [Header("Thrust Settings")]
    [SerializeField] private float thrust = 300f;
    [SerializeField] private float thrustGlideReduction = -0.01f;

    [Header("Pitch / Yaw / Roll Settings")]
    [SerializeField] private float pitchTorque = 1000f;
    // [SerializeField] private float pitchGlideReduction = 0.1f;
    [SerializeField] private float yawTorque = 1000f;
    // [SerializeField] private float yawGlideReduction = 0.1f;

    [SerializeField] private float rollTorque = 1000f;
    // [SerializeField] private float rollGlideReduction = 0.1f;


    [Header("Strafe Settings")]
    [SerializeField] private float strafeThrust = 50f;
    [SerializeField] private float leftRightGlideReduction = 0.111f;
    [SerializeField] private float upDownGlideReduction = 0.05f;

    private Rigidbody body;

    private float yawRotation = 0f;
    private float pitchRotation = 0f;
    private float rollRotation = 0f;
    private float leftThrustPercentage = 0f;
    private float rightThrustPercentage = 0f;
    private float highestPercentage = 0f;
    private float thrustDiff = 0f;
    private float glide = 0f;
    // private float pitchGlide = 0f;
    // private float yawGlide = 0f;
    // private float rollGlide = 0f;
    private float horizontalGlide = 0f;
    private float verticalGlide = 0f;

    private bool startStrafing = false;
    private bool startUpDown = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponentInParent<Rigidbody>();
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
    private void OnThrustShip()
    {
        if(highestPercentage > 0.1f || highestPercentage < -0.1f)
        {
            float currentThrust = thrust * highestPercentage * PowerLevel;

            body.AddForce(body.transform.forward * currentThrust * Time.fixedDeltaTime, ForceMode.Impulse);
            glide = currentThrust;
        } else {
            body.AddForce(Vector3.back * glide * Time.deltaTime, ForceMode.Force);
            glide *= thrustGlideReduction;
        }
    }

    private void OnRotateShip()
    {
        // Pitch
        body.AddRelativeTorque(Vector3.right * pitchRotation * pitchTorque * PowerLevel * Time.deltaTime);
        // Yaw
        body.AddRelativeTorque(Vector3.up * yawRotation * yawTorque * PowerLevel * Time.deltaTime);
        // Roll
        body.AddRelativeTorque(Vector3.back * rollRotation * rollTorque * PowerLevel * Time.deltaTime);

        // if(pitchRotation > 0.1f || pitchRotation < -0.1f)
        // {
        //     float currentPitch = pitchRotation * pitchTorque;

        //     body.AddTorque(body.transform.right * currentPitch * Time.fixedDeltaTime, ForceMode.Force);
        //     pitchGlide = currentPitch;
        // } else
        // {
        //     body.AddTorque(body.transform.right * pitchGlide * Time.fixedDeltaTime, ForceMode.Force);
        //     pitchGlide *= pitchGlideReduction;
        // }

        // if(yawRotation > 0.1f || yawRotation < -0.1f)
        // {
        //     float currentYaw = yawRotation * yawTorque;

        //     body.AddTorque(body.transform.up * currentYaw * Time.fixedDeltaTime, ForceMode.Force);
        //     yawGlide = currentYaw;
        // } else
        // {
        //     body.AddTorque(body.transform.up * yawGlide * Time.fixedDeltaTime, ForceMode.Force);
        //     yawGlide *= yawGlideReduction;
        // }

        // if(rollRotation > 0.1f || rollRotation < -0.1f)
        // {
        //     float currentRoll = rollRotation * rollTorque;

        //     body.AddTorque(body.transform.forward * currentRoll * Time.fixedDeltaTime, ForceMode.Force);
        //     rollGlide = currentRoll;
        // } else
        // {
        //     body.AddTorque(body.transform.forward * rollGlide * Time.fixedDeltaTime, ForceMode.Force);
        //     rollGlide *= rollGlideReduction;
        // }
    }

    private void OnStrafeShip()
    {
        if(startStrafing)
        {
            body.AddRelativeForce(Vector3.right * strafeThrust * PowerLevel * Time.fixedDeltaTime);
            horizontalGlide = strafeThrust;
        } else 
        {
            body.AddRelativeForce(Vector3.right * horizontalGlide * Time.fixedDeltaTime);
            horizontalGlide *= leftRightGlideReduction;
        }
    }

    private void OnUpDownShip()
    {
        if(startUpDown)
        {
            body.AddRelativeForce(Vector3.up * strafeThrust * PowerLevel * Time.fixedDeltaTime);
            verticalGlide = strafeThrust;
        } else 
        {
            body.AddRelativeForce(Vector3.up * verticalGlide * Time.fixedDeltaTime);
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
