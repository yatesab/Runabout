using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class TurretArm : GrabPhysics
{
    public Weapon turretWeapon;
    public TurretArmMirror turretArmMirror;

    public Transform turretArm;

    private bool turretActive;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        grabInteractable.activated.AddListener(ActivatePulled);
        grabInteractable.deactivated.AddListener(ActivateReleased);
    }

    // Update is called once per frame
    void Update()
    {
        //Update Handle Position
        base.Update();

        //Update Turret Position
        turretArmMirror.MirrorTurretPosition(turretArm.localPosition);
        turretArmMirror.MirrorTurretRotation(turretArm.localRotation);

        if(turretActive)
        {
            //turretWeapon.TriggerStateChanged(true);
        }
    }

    public void ActivatePulled(ActivateEventArgs args)
    {
        turretActive = true;
    }

    public void ActivateReleased(DeactivateEventArgs args)
    {
        turretActive = false;
        //turretWeapon.TriggerStateChanged(false);
    }
}
