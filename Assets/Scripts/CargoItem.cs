using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoItem : GrabPhysics
{
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public void Update()
    {
        meshMirror.MirrorPosition(MirrorLocalPosition);
        meshMirror.MirrorRotation(MirrorLocalRotation);
    }
}
