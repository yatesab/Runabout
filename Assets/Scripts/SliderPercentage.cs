using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPercentage : ControlSlider
{
    public float Percentage { get; private set; }

    [Range(0f, 1f)]
    [SerializeField] private float sliderStartPercentage = 0.5f;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();

        Percentage = sliderStartPercentage;

        SetSliderByPercentage();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Grabbed)
        {
            //Set Percentage
            FindSliderPercentage();
        }
    }

    public void FindSliderPercentage()
    {
        Percentage = transform.InverseTransformPoint(slider.position).z / maxDistance;
    }

    public void SetSliderByPercentage()
    {
        float zPosition = maxDistance * Percentage;

        slider.localPosition = new Vector3(slider.localPosition.x, slider.localPosition.y, lowerLimit + zPosition);
    }
}
