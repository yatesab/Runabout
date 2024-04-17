using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ViewFinder : MonoBehaviour
{
    public Material forwardCameraMaterial;
    public Material reverseCameraMaterial;


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

    public void TurnOnScreen()
    {
        // Change material to camera material
        Material[] materialsCopy = meshRenderer.materials;
        materialsCopy[0] = cameraToUse;
        meshRenderer.materials = materialsCopy;

        _screenOn = true;
        meshRenderer.enabled = true;
    }

    public void TurnOffScreen()
    {
        _screenOn = false;
        meshRenderer.enabled = false;
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
