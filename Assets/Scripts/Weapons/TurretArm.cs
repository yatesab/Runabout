using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class TurretArm : GrabPhysics
{

    public TurretArmMirror turretArmMirror;
    public GameObject laserBolt;

    public Transform turretArm;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        grabInteractable.activated.AddListener(HandleFireTurret);
    }

    // Update is called once per frame
    void Update()
    {
        //Update Handle Position
        base.Update();

        //Update Turret Position
        turretArmMirror.MirrorTurretPosition(turretArm.localPosition);
        turretArmMirror.MirrorTurretRotation(turretArm.localRotation);
    }

    private void HandleFireTurret(ActivateEventArgs args)
    {
        GameObject bolt = Instantiate(laserBolt, turretArmMirror.muzzle.position, turretArmMirror.muzzle.rotation);
    }
}
