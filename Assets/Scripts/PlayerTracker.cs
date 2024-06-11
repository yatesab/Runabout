using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Transform playerPlayarea;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform characterBody;
    [SerializeField] private Transform badge;
    [SerializeField] private Transform badgeCollider;

    [Header("Player Camera Diverged Layer Mask")]
    [SerializeField] private LayerMask playerMask;

    [Header("Startup Settings")]
    [SerializeField] private bool startDiverged;
    [SerializeField] private Transform playerPlayareaParent;
    [SerializeField] private GameObject shipPlayarea;
    [SerializeField] private Camera shipCamera;

    private LayerMask originalPlayerMask;
    private bool isDiverged = false;
    private Vector3 characterCenter;
    private Vector3 characterPosition;
    private Vector3 characterRotation;
    private Vector3 badgeLocation;

    public void Start()
    {
        characterCenter = new Vector3(playerCamera.transform.localPosition.x, characterController.height / 2, playerCamera.transform.localPosition.z);
        characterPosition = new Vector3(playerCamera.transform.localPosition.x, 0f, playerCamera.transform.localPosition.z);
        characterRotation = new Vector3(0f, playerCamera.transform.localEulerAngles.y, 0f);
        badgeLocation = new Vector3(badgeCollider.localPosition.x, characterController.height - 0.3f, badgeCollider.localPosition.z);

        if (startDiverged)
        {
            DivergeCamera();
        }
    }

    public void LateUpdate()
    {
        characterController.height = Mathf.Clamp(playerCamera.transform.localPosition.y, 1f, 2f);

        characterCenter.x = playerCamera.transform.localPosition.x;
        characterCenter.z = playerCamera.transform.localPosition.z;
        characterCenter.y = characterController.height / 2;
        characterController.center = characterCenter;

        characterPosition.x = playerCamera.transform.localPosition.x;
        characterPosition.z = playerCamera.transform.localPosition.z;
        characterBody.localPosition = characterPosition;

        characterRotation.y = playerCamera.transform.localEulerAngles.y;
        badge.localEulerAngles = characterRotation;

        badgeLocation.y = characterController.height - 0.3f;
        badgeCollider.localPosition = badgeLocation;

        if (isDiverged)
        {
            shipPlayarea.transform.localPosition = playerPlayareaParent.InverseTransformPoint(playerPlayarea.position);
            shipPlayarea.transform.localRotation = playerPlayarea.localRotation;
        }
    }

    private void SetCameraObjects(CameraSpliter cameraSpliter)
    {
        playerPlayareaParent = cameraSpliter.playerParent;
        shipPlayarea = cameraSpliter.shipPlayarea;
        shipCamera = cameraSpliter.shipCamera;
    }

    public void DivergeCamera()
    {
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

    public void ConvergeCamera()
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
            SetCameraObjects(cameraSpliter);

            DivergeCamera();
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
                SetCameraObjects(cameraSpliter);

                ConvergeCamera();
            }
        }
    }

}
