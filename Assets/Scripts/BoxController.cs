using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private Material transparentMaterial;
    public int BoxID {  get; set; }
    public Item boxItem {  get; set; }

    private Material originalMaterial;
    private Rigidbody boxBody;
    private Collider boxCollider;
    private Renderer boxMesh;

    // Start is called before the first frame update
    void Start()
    {
        boxBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<Collider>();
        boxMesh = GetComponent<Renderer>();
    }

    public void DeactivateBoxSegments()
    {
        boxBody.useGravity = false;
        boxBody.isKinematic = false;

        boxCollider.enabled = false;
        originalMaterial = boxMesh.material;
        boxMesh.material = transparentMaterial;
    }

    public void ReactivateBoxSegments()
    {
        boxBody.useGravity = true;
        boxBody.isKinematic = true;

        boxCollider.enabled = true;
        boxMesh.material = originalMaterial;
    }

    public Vector3 GetBoxBounds()
    {
        Vector3 overallSize = Vector3.one;

        overallSize.x += boxMesh.bounds.size.x;
        overallSize.y = boxMesh.bounds.size.y;
        overallSize.z = boxMesh.bounds.size.z;

        return overallSize;
    }
}
