using UnityEngine;
using UnityEngine.Events;

public class SliderStep : ControlSlider
{
    public int Step { get; private set; }

    [Header("Step Settings")]
    [SerializeField] private bool snapToStep = false;
    [SerializeField] private int sliderStartStep = 0;
    [SerializeField] private int stepMin;
    [SerializeField] private int stepMax;

    [SerializeField] private UnityEvent<float> HandleChangeStep;

    private int stepCount = 0;
    private int currentStepNumber;
    private float stepAmount;
    private float currentStepPosition;
    private float stepUpperPosition;
    private float stepLowerPosition;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();

        Step = sliderStartStep;

        for (int i = stepMin; i < stepMax; i++)
        {
            stepCount++;
        }

        stepAmount = maxDistance / stepCount;
        CalculateCurrentStep();
        SetSliderByStep();
        HandleChangeStep.Invoke(Step);
    }

    // Update is called once per frame
    public void Update()
    {
        if(Grabbed)
        {
            float localZPosition = transform.InverseTransformPoint(slider.position).z;

            int oldStep = Step;
            if (localZPosition > stepUpperPosition)
            {
                Step++;
            }
            else if (localZPosition < stepLowerPosition)
            {
                Step--;
            }

            if(Step != oldStep)
            {
                CalculateCurrentStep();
                HandleChangeStep.Invoke(Step);
            }
        }
    }

    public void CalculateCurrentStep()
    {
        // Get Current Step Number
        currentStepNumber = 0;
        for (int i = stepMin; i < Step; i++)
        {
            currentStepNumber++;
        }

        currentStepPosition = lowerLimit + (stepAmount * currentStepNumber);

        stepUpperPosition = currentStepPosition + (stepAmount / 2);
        stepLowerPosition = currentStepPosition - (stepAmount / 2);
    }

    public void SetSliderByStep()
    {
        slider.localPosition = new Vector3(slider.localPosition.x, slider.localPosition.y, currentStepPosition);
    }


    public new void HandleReleased()
    {
        base.HandleReleased();

        if (snapToStep)
        {
            //Check what step to snap to
            SetSliderByStep();
        }
    }
}
