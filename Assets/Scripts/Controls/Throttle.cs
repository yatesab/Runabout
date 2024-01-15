using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;

public enum ThrottleSide
{
    Left,
    Right
}

public class Throttle : MonoBehaviour
{
    public ThrottleSide throttleLocation;
    public bool throttleReset = true;
    public bool useRoll = false;
    public bool usePitch = false;

    public float ThrottlePercentage {set;get;}
    public bool Grabbed {set;get;}

    private Transform _throttleHandle;
    private TMP_Text _screenPercentage;

    // Start is called before the first frame update
    void Start()
    {
        // Get XRGrabInteractable for isGrabbing check
        XRGrabInteractable _grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        _grabInteractable.selectEntered.AddListener(HandleStartGrab);
        _grabInteractable.selectExited.AddListener(HandleStopGrab);

        _throttleHandle = GetComponentInChildren<Rigidbody>().transform;
        _screenPercentage = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ThrottlePercentage = transform.InverseTransformPoint(_throttleHandle.position).z / 0.2f;
        UpdateScreen();
    }

    private void UpdateScreen()
    {
        _screenPercentage.text = Mathf.Round(ThrottlePercentage * 100).ToString() + " %";
    }

    /**
        Grab Interactable Actions
    */
    private void HandleStartGrab(SelectEnterEventArgs args)
    {
        Grabbed = true;
        throttleReset = false;
    }

    private void HandleStopGrab(SelectExitEventArgs args)
    {
        Grabbed = false;
    }

    public void SetThrottlePercentage(float newPercentage)
    {
        _throttleHandle.localPosition = new Vector3(_throttleHandle.localPosition.x, _throttleHandle.localPosition.y, newPercentage * 0.2f);
    }
}
