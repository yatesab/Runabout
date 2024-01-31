using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFinder : MonoBehaviour
{
    public Material forwardCameraMaterial;
    public Material reverseCameraMaterial;
    public Material glassMaterial;


    private MeshRenderer meshRenderer;
    private bool _screenOn = false;
    private Material cameraToUse;

    void Awake()
    {
        cameraToUse = forwardCameraMaterial;
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOnScreen()
    {
        // Change material to camera material
        Material[] materialsCopy = meshRenderer.materials;
        materialsCopy[0] = cameraToUse;
        meshRenderer.materials = materialsCopy;

        _screenOn = true;
    }

    public void TurnOffScreen()
    {
        // Change material back to glass
        Material[] materialsCopy = meshRenderer.materials;
        materialsCopy[0] = glassMaterial;
        meshRenderer.materials = materialsCopy;

        _screenOn = false;
    }

    public void ChangeToReverseCamera()
    {
        cameraToUse = reverseCameraMaterial;

        if (_screenOn)
        {
            TurnOnScreen();
        }
    }

    public void ChangeToForwardCamera()
    {
        cameraToUse = forwardCameraMaterial;

        if (_screenOn)
        {
            TurnOnScreen();
        }
    }
}
