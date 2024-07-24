using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class ControlDial : MonoBehaviour
{
    public bool Grabbed {  get; private set; }
    public int Step {  get; private set; }

    [SerializeField] private bool snapToStep = false;
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private Transform dialTracker;
    [Range(1f, 180f)]
    [SerializeField] private float rotationLimit = 90f;
    [SerializeField] private int startStep = 0;
    [SerializeField] private int stepMax = 1;
    [SerializeField] private int stepMin = -1;

    public UnityEvent<int> DialStepEvent;

    private int stepCount = 0;
    protected float maxDistance;
    private float lowerLimit;
    private float currentAngle;
    private int currentStepNumber;
    private float stepAmount;
    private float lowAngle;
    private float highAngle;
    private float angleTolerance;

    public void Start()
    {
        lowerLimit = grabInteractable.transform.localEulerAngles.y - rotationLimit;
        maxDistance = rotationLimit * 2;

        JointSetup();

        for (int i = stepMin; i < stepMax; i++)
        {
            stepCount++;
        }

        Step = startStep;
        stepAmount = maxDistance / stepCount;
        angleTolerance = stepAmount / 2;
        CalculateCurrentAngle();
        SetDialByStep();

        MatchRotation();
    }

    // Update is called once per frame
    public void Update()
    {
        MatchRotation();

        if (Grabbed)
        {
            if(CheckDialStep(dialTracker.localEulerAngles.y))
            {
                CalculateCurrentAngle();
                CalculateHighAndLowAngle();
                DialStepEvent.Invoke(Step);
            }
        }
    }

    private void JointSetup()
    {
        SoftJointLimit JointLimit = new SoftJointLimit();
        JointLimit.limit = rotationLimit;

        joint.angularYLimit = JointLimit;
    }

    private void MatchRotation()
    {
        dialTracker.rotation = grabInteractable.transform.rotation;
    }

    private bool CheckDialStep(float dialAngle)
    {
        if (dialAngle < lowAngle)
        {
            if (Step > stepMin)
            {
                Step--;
                return true;
            }
        } else if (dialAngle > highAngle)
        {
            if (Step < stepMax)
            {
                Step++;
                return true;
            }
        }

        return false;
    }

    private void CalculateCurrentAngle()
    {
        currentStepNumber = 0;
        for (int i = stepMin; i < Step; i++)
        {
            currentStepNumber++;
        }

        currentAngle = lowerLimit + (stepAmount * currentStepNumber);
    }

    private void SetDialByStep()
    {
        grabInteractable.transform.localEulerAngles = new Vector3(grabInteractable.transform.localEulerAngles.x, currentAngle, grabInteractable.transform.localEulerAngles.z);
    }

    private void CalculateHighAndLowAngle()
    {
        lowAngle = currentAngle - angleTolerance;
        if (lowAngle < 0)
        {
            lowAngle += 360f;
        }

        highAngle = currentAngle + angleTolerance;
        if (highAngle > 360f)
        {
            highAngle -= 360f;
        }
    }

    public void HandleGrab(SelectEnterEventArgs args)
    {
        Grabbed = true;

        CalculateHighAndLowAngle();
    }


    public void HandleLetgo(SelectExitEventArgs args)
    {
        Grabbed = false;

        if (snapToStep)
        {
            //Check what step to snap to
            SetDialByStep();
        }
    }
}