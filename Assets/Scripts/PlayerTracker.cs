using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Transform playerPlayarea;
    [SerializeField] private Camera playerCamera;

    [Header("Player Camera Diverged Layer Mask")]
    [SerializeField] private LayerMask playerMask;

    private LayerMask originalPlayerMask;
    private Transform playerPlayareaParent;
    private GameObject shipPlayarea;
    private Camera shipCamera;
    private bool isDiverged = false;

    void LateUpdate()
    {
        if (isDiverged)
        {
            shipPlayarea.transform.localPosition = playerPlayareaParent.InverseTransformPoint(playerPlayarea.position);
            shipPlayarea.transform.localRotation = playerPlayarea.localRotation;
        }
    }

    public void DivergeCamera(CameraSpliter cameraSpliter)
    {
        playerPlayareaParent = cameraSpliter.playerParent;
        shipPlayarea = cameraSpliter.shipPlayarea;
        shipCamera = cameraSpliter.shipCamera;

        shipPlayarea.SetActive(true);

        // Add limited culling mask settings
        originalPlayerMask = playerCamera.cullingMask;
        playerCamera.cullingMask = playerMask;

        UniversalAdditionalCameraData playerCameraData = playerCamera.GetUniversalAdditionalCameraData();
        playerCameraData.renderType = CameraRenderType.Overlay;

        UniversalAdditionalCameraData shipCameraData = shipCamera.GetUniversalAdditionalCameraData();
        shipCameraData.cameraStack.Add(playerCamera);

        isDiverged = true;
    }

    public void ConvergeCamera(CameraSpliter cameraSpliter)
    {
        shipPlayarea.SetActive(false);

        // Remove limited culling mask settings
        playerCamera.cullingMask = originalPlayerMask;

        UniversalAdditionalCameraData playerCameraData = playerCamera.GetUniversalAdditionalCameraData();
        playerCameraData.renderType = CameraRenderType.Base;

        shipCamera = null;
        shipPlayarea = null;
        playerPlayareaParent = null;

        isDiverged = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isDiverged && other.tag == "Ship")
        {
            CameraSpliter cameraSpliter = other.GetComponent<CameraSpliter>();
            DivergeCamera(cameraSpliter);
        } 
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ship")
        {
            Vector3 direction = playerCamera.transform.position - other.transform.position;

            if(direction.z < 0)
            {
                CameraSpliter cameraSpliter = other.GetComponent<CameraSpliter>();
                ConvergeCamera(cameraSpliter);
            }
        }
    }

}
