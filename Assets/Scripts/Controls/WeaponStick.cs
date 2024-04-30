using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStick : MonoBehaviour
{
    public WeaponControl mainWeapon;
    public WeaponControl secondaryWeapon;

    public bool StickGrabbed { get; set; }

    private Transform interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(StickGrabbed)
        {
            mainWeapon.UpdateRotation(interactable.localRotation);
            secondaryWeapon.UpdateRotation(interactable.localRotation);
        }
    }

    public void HandleGrabStick()
    {
        StickGrabbed = true;
    }

    public void HandleReleaseStick()
    {
        StickGrabbed = false;

        Vector3 newPosition = new Vector3(0, 0, 0);
        Quaternion newRotation = new Quaternion(0, 0, 0, 0);

        interactable.localPosition = newPosition;
        interactable.localRotation = newRotation;

        mainWeapon.UpdateRotation(newRotation);
        secondaryWeapon.UpdateRotation(newRotation);
    }
}
