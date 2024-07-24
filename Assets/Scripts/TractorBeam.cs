using System;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    [SerializeField] protected float tractorBeamStrength = 10f;

    public List<Collider> Targets { get { return grabColliders; } }
    protected List<Collider> grabColliders = new List<Collider>();
    
    protected bool beamActivated = false;
    protected Collider currentTarget;
    protected Vector3 targetMoveLocation;
    protected LineRenderer lineRenderer;

    public void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public virtual void LateUpdate()
    {
        if(beamActivated)
        {
            SetLinePositions();
        }
    }

    public void SetLinePositions()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, currentTarget.transform.position);
    }
}
