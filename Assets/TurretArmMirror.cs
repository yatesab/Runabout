using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretArmMirror : MeshMirror
{
    public Transform _turretArmToMirror;
    public Transform shipTurret;
    public Transform muzzle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MirrorTurretPosition(Vector3 turretArmPosition)
    {
        _turretArmToMirror.localPosition = turretArmPosition;
    }

    public void MirrorTurretRotation(Quaternion turretArmRotation)
    {
        _turretArmToMirror.localRotation = turretArmRotation;
        shipTurret.localRotation = turretArmRotation;
    }
}
