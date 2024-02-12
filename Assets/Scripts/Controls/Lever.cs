using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using TMPro;

public class Lever : GrabPhysics
{
    [SerializeField] private Transform lever;
    [SerializeField] private TMP_Text leverPercentageText;
    public float leverPercentage = 0f;
    public bool Grabbed { set; get; }
    public Transform handlePoint;
    public Transform endPoint;

    private float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        grabInteractable.selectEntered.AddListener(HandleStartGrab);
        grabInteractable.selectExited.AddListener(HandleStopGrab);

        maxDistance = Vector3.Distance(handlePoint.position, endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (Grabbed)
        {
            float currentDistance = maxDistance - Vector3.Distance(handlePoint.position, endPoint.position);
            leverPercentage = currentDistance / maxDistance;
            leverPercentageText.text = Mathf.Round(leverPercentage * 100).ToString() + " %";
        }
    }
    
    private void HandleStartGrab(SelectEnterEventArgs args)
    {
        Grabbed = true;
    }

    private void HandleStopGrab(SelectExitEventArgs args)
    {
        Grabbed = false;
    }
}
