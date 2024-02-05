using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using TMPro;

public class Lever : MonoBehaviour
{
    public MeshMirror leverMirror;

    [SerializeField] private Transform lever;
    [SerializeField] private TMP_Text leverPercentageText;
    public float leverPercentage = 0f;
    public bool Grabbed { set; get; }
    public Transform handlePoint;
    public Transform endPoint;

    private float maxDistance;
    private XRGrabInteractable _grabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        _grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        _grabInteractable.hoverEntered.AddListener(HandleHoverEnter);
        _grabInteractable.hoverExited.AddListener(HandleHoverExit);
        _grabInteractable.selectEntered.AddListener(HandleStartGrab);
        _grabInteractable.selectExited.AddListener(HandleStopGrab);

        maxDistance = Vector3.Distance(handlePoint.position, endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Grabbed)
        {
            leverMirror.MirrorRotation(Quaternion.Inverse(transform.rotation) * lever.rotation);

            float currentDistance = maxDistance - Vector3.Distance(handlePoint.position, endPoint.position);
            leverPercentage = currentDistance / maxDistance;
            leverPercentageText.text = Mathf.Round(leverPercentage * 100).ToString() + " %";
        }
    }

    private void HandleHoverEnter(HoverEnterEventArgs args)
    {
        // Do hover things here
        if (leverMirror.hasHover)
        {
            leverMirror.ActivateHover();
        }
    }

    private void HandleHoverExit(HoverExitEventArgs args)
    {
        // Do hover things here
        if (leverMirror.hasHover)
        {
            leverMirror.DeactivateHover();
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
