using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class TurretArm : MonoBehaviour
{

    public TurretArmMirror turretArmMirror;
    public GameObject laserBolt;

    public Transform handle;
    public Transform turretArm;
    public Vector3 MirrorLocalPosition { get { return transform.InverseTransformPoint(handle.position); } }

    private XRGrabInteractable _grabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        _grabInteractable = GetComponentInChildren<XRGrabInteractable>();

        _grabInteractable.activated.AddListener(HandleFireTurret);
    }

    // Update is called once per frame
    void Update()
    {
        turretArmMirror.MirrorPosition(MirrorLocalPosition);
        turretArmMirror.MirrorTurretPosition(turretArm.localPosition);

        turretArmMirror.MirrorTurretRotation(turretArm.localRotation);
    }

    private void HandleFireTurret(ActivateEventArgs args)
    {
        GameObject bolt = Instantiate(laserBolt, turretArmMirror.muzzle.position, turretArmMirror.muzzle.rotation);
    }
}
