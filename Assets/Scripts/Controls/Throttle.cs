using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Throttle : MonoBehaviour
{
    public event Action onThrottleChange;
    public float ThrottlePercentage { set; get; }
    public bool Grabbed { set; get; }
    public Vector3 ThrottlePosition { get { return transform.InverseTransformPoint(throttle.position); } }

    [SerializeField] private Transform throttle;
    [SerializeField] private float _throttleDeadZone = 0.02f;

    [Header("Joint")]
    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private float jointLimit;

    protected float maxDistance;
    protected float lowerLimit;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = jointLimit * 2;
        lowerLimit = jointLimit * -1;

        // Setup joint
        SoftJointLimit JointLimit = new SoftJointLimit();
        JointLimit.limit = jointLimit;

        joint.linearLimit = JointLimit;

        throttle.localPosition = new Vector3(throttle.localPosition.x, throttle.localPosition.y, lowerLimit);

        CalculateThrottlePercentage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Grabbed)
        {
            CalculateThrottlePercentage();
        }
    }

    private void CalculateThrottlePercentage()
    {
        float newPercentage = (ThrottlePosition.z - lowerLimit) / (maxDistance);

        if (newPercentage != ThrottlePercentage)
        {
            ThrottlePercentage = newPercentage;

            if (onThrottleChange != null)
            {
                onThrottleChange.Invoke();
            }
        }
    }

    public void SyncThrottlePosition(Vector3 newHandlePosition)
    {
        throttle.localPosition = newHandlePosition;
        CalculateThrottlePercentage();
    }

    public void ResetThrottle(SelectExitEventArgs args)
    {
        throttle.localPosition = new Vector3(throttle.localPosition.x, throttle.localPosition.y, lowerLimit);
    }
}
