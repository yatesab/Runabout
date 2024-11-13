using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private GameObject[] boxSegments;
    [SerializeField] private Material transparentMaterial;

    private Material originalMaterial;
    private Rigidbody boxBody;

    // Start is called before the first frame update
    void Start()
    {
        boxBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateBoxSegments()
    {
        boxBody.useGravity = false;
        boxBody.isKinematic = false;

        foreach (GameObject boxSegment in boxSegments)
        {
            BoxCollider collider = boxSegment.GetComponent<BoxCollider>();
            collider.enabled = false;

            MeshRenderer mesh = boxSegment.GetComponent<MeshRenderer>();

            originalMaterial = mesh.material;
            mesh.material = transparentMaterial;
        }
    }

    public void ReactivateBoxSegments()
    {
        foreach (GameObject boxSegment in boxSegments)
        {
            BoxCollider collider = boxSegment.GetComponent<BoxCollider>();
            collider.enabled = true;

            MeshRenderer mesh = boxSegment.GetComponent<MeshRenderer>();

            mesh.material = originalMaterial;
        }

        boxBody.useGravity = true;
        boxBody.isKinematic = true;
    }
}
