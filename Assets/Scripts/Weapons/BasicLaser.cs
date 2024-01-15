using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLaser : Weapon
{
    [SerializeField] private float _maxLength;
    [SerializeField] private LineRenderer _beam;
    private bool isFireing;

    private void FixedUpdate() {
        if(!_beam.enabled) return;

        // Cast ray and create laser line
        RaycastHit hit;

        bool cast = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxLength);

        Vector3 hitPosition = cast ? hit.point : transform.position + transform.forward * _maxLength;

        _beam.SetPosition(0, transform.position);
        _beam.SetPosition(1, hitPosition);
    }

    public override void FireWeapon()
    {
        _beam.enabled = true;
    }

    public override void StopFireWeapon()
    {
        _beam.enabled = false;
        _beam.SetPosition(0, transform.position);
        _beam.SetPosition(1, transform.position);
    }
}
