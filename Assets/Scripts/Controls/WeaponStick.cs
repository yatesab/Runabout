using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class WeaponStick : MonoBehaviour
{
    public InputActionReference primaryLeftActionReference;
    public InputActionReference primaryRightActionReference;

    public MainWeapon mainWeapon;
    public SecondaryWeapon secondaryWeapon;

    public bool StickGrabbed { get; set; }

    private XRGrabInteractable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponentInChildren<XRGrabInteractable>();

        interactable.selectExited.AddListener(HandleReleaseStick);

        primaryLeftActionReference.action.started += HandlePrimaryButton;
        primaryRightActionReference.action.started += HandlePrimaryButton;
    }

    // Update is called once per frame
    void Update()
    {
        if(StickGrabbed)
        {
            mainWeapon.UpdateRotation(interactable.transform.localRotation);
            secondaryWeapon.UpdateRotation(interactable.transform.localRotation);
        }
    }

    public void HandleReleaseStick(SelectExitEventArgs args)
    {
        Vector3 newPosition = new Vector3(0, 0, 0);
        Quaternion newRotation = new Quaternion(0, 0, 0, 0);

        interactable.transform.localPosition = newPosition;
        interactable.transform.localRotation = newRotation;

        mainWeapon.UpdateRotation(newRotation);
        secondaryWeapon.UpdateRotation(newRotation);
    }

    private void HandlePrimaryButton(InputAction.CallbackContext obj)
    {
        if(StickGrabbed)
        {
            secondaryWeapon.FireSecondaryWeapon();
        }
    }
}
