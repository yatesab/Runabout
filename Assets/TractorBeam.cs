using System;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    [SerializeField] private float tractorBeamScanRadius = 50f;
    [SerializeField] private float tractorBeamStrength = 10f;

    public event Action scanAction;
    public List<Collider> Targets { get { return grabColliders; } }
    private List<Collider> grabColliders = new List<Collider>();
    
    private bool beamActivated = false;
    private Collider currentTarget;
    private Vector3 localTractorPosition;
    private LineRenderer lineRenderer;

    public void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(beamActivated)
        {
            currentTarget.transform.position = Vector3.Slerp(currentTarget.transform.position, transform.TransformPoint(localTractorPosition), tractorBeamStrength * Time.fixedDeltaTime);
        }
    }

    public void LateUpdate()
    {
        if(beamActivated)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentTarget.transform.position);
        }
    }

    public void StartTractorBeam(int colliderIndex)
    {
        currentTarget = grabColliders[colliderIndex];
        beamActivated = true;

        currentTarget.GetComponent<Rigidbody>().isKinematic = true;

        localTractorPosition = transform.InverseTransformPoint(currentTarget.transform.position);

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, currentTarget.transform.position);
    }

    public void StopTractorBeam()
    {
        beamActivated = false;
        currentTarget.GetComponent<Rigidbody>().isKinematic = false;

        currentTarget = null;

        lineRenderer.positionCount = 0;
    }

    public void ScanArea()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, tractorBeamScanRadius);

        grabColliders.Clear();
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Grab")
            {
                grabColliders.Add(hitCollider);
            }
        }

        scanAction.Invoke();
    }
}
