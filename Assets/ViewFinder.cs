using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFinder : MonoBehaviour
{
    public Material cameraMaterial;
    public Material glassMaterial;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FlipSwitchOnEvent()
    {
        // Change material to camera material
        Material[] materialsCopy = meshRenderer.materials;
        materialsCopy[0] = cameraMaterial;
        meshRenderer.materials = materialsCopy;
    }

    public void FlipSwitchOffEvent()
    {
        // Change material back to glass
        Material[] materialsCopy = meshRenderer.materials;
        materialsCopy[0] = glassMaterial;
        meshRenderer.materials = materialsCopy;
    }
}
