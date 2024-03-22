using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStick : MonoBehaviour
{
    public WeaponControl mainWeapon;
    public WeaponControl secondaryWeapon;

    private Transform interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        mainWeapon.UpdateRotation(interactable.localRotation);
        secondaryWeapon.UpdateRotation(interactable.localRotation);
    }

    public void ResetPosition()
    {
        interactable.localPosition = new Vector3(0, 0, 0);
        interactable.localRotation = new Quaternion(0, 0, 0, 0);
    }
}
