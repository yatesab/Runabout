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
    public float _throttleDeadZone = 0.02f;
    public bool isSynced = true;
    public bool useRoll = false;
    public bool usePitch = false;
    public bool useYaw = false;

    public Vector3 MirrorLocalPosition { get { return transform.InverseTransformPoint(_throttleHandle.position); } }
    public float ThrottlePercentage {set;get;}
    public bool Grabbed {set;get;}

    private Transform _throttleHandle;
    public TMP_Text _screenPercentage;

    // Start is called before the first frame update
    void Start()
    {
        // Get XRGrabInteractable for isGrabbing check
        XRGrabInteractable _grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        _grabInteractable.selectEntered.AddListener(HandleStartGrab);
        _grabInteractable.selectExited.AddListener(HandleStopGrab);

        _throttleHandle = GetComponentInChildren<Rigidbody>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        float handlePosition = transform.InverseTransformPoint(_throttleHandle.position).z;

        if(handlePosition > _throttleDeadZone || handlePosition < -_throttleDeadZone)
        {
            ThrottlePercentage = (handlePosition - _throttleDeadZone) / (0.2f - _throttleDeadZone);
        } else 
        {
            ThrottlePercentage = 0f;
        }
        
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
        isSynced = false;
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
