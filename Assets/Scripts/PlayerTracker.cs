using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Transform playerPlayareaParent;
    [SerializeField] private Transform playerPlayarea;
    [SerializeField] private Camera playerCamera;

    [SerializeField] private Transform shipPlayareaParent;
    [SerializeField] Transform shipPlayarea;
    [SerializeField] private Camera shipCamera;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask shipMask;
    private LayerMask originalPlayerMask;
    private LayerMask originalShipMask;

    private bool isDiverged = false;

    void LateUpdate()
    {
        if (isDiverged)
        {
            Vector3 playareaLocal = playerPlayareaParent.InverseTransformPoint(playerPlayarea.position);
            shipPlayarea.position = shipPlayareaParent.TransformPoint(playareaLocal);
        } else
        {
            shipPlayarea.position = playerPlayarea.position;

        }

        shipPlayarea.rotation = playerPlayarea.rotation;

        //shipCamera.localPosition = playerCamera.localPosition;
        //shipCamera.localRotation = playerCamera.localRotation;
    }

    public void DivergeCamera()
    {
        // Add limited culling mask settings
        originalPlayerMask = playerCamera.cullingMask;
        playerCamera.cullingMask = playerMask;

        originalShipMask = playerCamera.cullingMask;
        shipCamera.cullingMask = shipMask;

        isDiverged = true;
    }

    public void ConvergeCamera()
    {
        // Remove limited culling mask settings
        playerCamera.cullingMask = originalPlayerMask;
        shipCamera.cullingMask = originalShipMask;

        isDiverged = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isDiverged && other.tag == "Ship")
        {
            DivergeCamera();
        } 
    }

}
