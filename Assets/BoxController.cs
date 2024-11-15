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
        boxBody.useGravity = true;
        boxBody.isKinematic = true;

        foreach (GameObject boxSegment in boxSegments)
        {
            BoxCollider collider = boxSegment.GetComponent<BoxCollider>();
            collider.enabled = true;

            MeshRenderer mesh = boxSegment.GetComponent<MeshRenderer>();

            mesh.material = originalMaterial;
        }
    }

    public Vector3 GetBoxBounds()
    {
        Vector3 overallSize = Vector3.one;
        foreach(GameObject boxSegment in boxSegments)
        {
            Renderer mesh = boxSegment.GetComponent<Renderer>();

            overallSize.x += mesh.bounds.size.x;
            overallSize.y = mesh.bounds.size.y;
            overallSize.z = mesh.bounds.size.z;
        }

        return overallSize;
    }
}
