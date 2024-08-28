using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] private Transform badge;
    [SerializeField] private Transform badgeCollider;

    [Header("Player Camera Diverged Layer Mask")]
    [SerializeField] private LayerMask playerMask;

    [Header("Startup Settings")]
    [SerializeField] private Transform playerPlayareaParent;
    [SerializeField] private GameObject shipPlayarea;
    [SerializeField] private Camera shipCamera;

    public float fadeDuration { get { return fadeScreen.fadeDuration; } }

    public bool isDiverged { get; private set; } = false;

    private LayerMask originalPlayerMask;
    private Vector3 characterCenter;
    private Vector3 characterPosition;
    private Vector3 characterRotation;
    private Vector3 badgeLocation;

    private CharacterController characterController;
    private FadeScreen fadeScreen;
    private Camera playerCamera;

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();

        playerCamera = GetComponentInChildren<Camera>();
        fadeScreen = GetComponentInChildren<FadeScreen>();
    }

    public void Start()
    {
        SetCharacterCenter(playerCamera.transform.localPosition, characterController.height);
        SetCharacterPosition(playerCamera.transform.localPosition);
        SetCharacterRotation(playerCamera.transform.localEulerAngles);

        badgeLocation = new Vector3(badgeCollider.localPosition.x, characterController.height - 0.3f, badgeCollider.localPosition.z);
    }

    public void LateUpdate()
    {
        characterController.height = Mathf.Clamp(playerCamera.transform.localPosition.y, 1f, 2f);

        SetCharacterCenter(playerCamera.transform.localPosition, characterController.height);
        characterController.center = characterCenter;

        SetCharacterPosition(playerCamera.transform.localPosition);
        characterBody.localPosition = characterPosition;

        SetCharacterRotation(playerCamera.transform.localEulerAngles);
        badge.localEulerAngles = characterRotation;

        badgeLocation.y = characterController.height - 0.3f;
        badgeCollider.localPosition = badgeLocation;

        if (isDiverged)
        {
            shipPlayarea.transform.localPosition = playerPlayareaParent.InverseTransformPoint(transform.position);
            shipPlayarea.transform.localRotation = transform.localRotation;
        }
    }

    private void SetCharacterCenter(Vector3 newCenter, float cameraHeight)
    {
        characterCenter = new Vector3(newCenter.x, cameraHeight / 2, newCenter.z);
    }

    private void SetCharacterPosition(Vector3 newPosition)
    {
        characterPosition = new Vector3(newPosition.x, 0f, newPosition.z);
    }

    private void SetCharacterRotation(Vector3 newRotation)
    {
        characterRotation = new Vector3(0f, newRotation.y, 0f);
    }

    public void SetCameraObjects(CameraSpliter cameraSpliter)
    {
        playerPlayareaParent = cameraSpliter.playerParent;
        shipPlayarea = cameraSpliter.shipPlayarea;
        shipCamera = cameraSpliter.shipCamera;
    }

    public void DivergeCamera()
    {
        if (isDiverged == false)
        {
            isDiverged = true;

            shipPlayarea.SetActive(true);

            // Add limited culling mask settings
            originalPlayerMask = playerCamera.cullingMask;
            playerCamera.cullingMask = playerMask;

            UniversalAdditionalCameraData playerCameraData = playerCamera.GetUniversalAdditionalCameraData();
            playerCameraData.renderType = CameraRenderType.Overlay;

            UniversalAdditionalCameraData shipCameraData = shipCamera.GetUniversalAdditionalCameraData();
            shipCameraData.cameraStack.Add(playerCamera);
        }
    }

    public void ConvergeCamera()
    {
        if (isDiverged)
        {
            isDiverged = false;

            shipPlayarea.SetActive(false);

            playerPlayareaParent = null;
            shipPlayarea = null;
            shipCamera = null;

            // Remove limited culling mask settings
            playerCamera.cullingMask = originalPlayerMask;

            UniversalAdditionalCameraData playerCameraData = playerCamera.GetUniversalAdditionalCameraData();
            playerCameraData.renderType = CameraRenderType.Base;
        }
    }

    public void PlayerFadeOut()
    {
        fadeScreen.FadeOut();
        PlayerConditionManager.instance.SetPlayerMovement(false);
    }

    public void PlayerFadeIn()
    {
        fadeScreen.FadeIn();
        PlayerConditionManager.instance.SetPlayerMovement(true);
    }

    public bool SetupSplitCamera()
    {
        GameObject cameraSpliter = GameObject.Find("Camera Spliter");
        if (cameraSpliter != null)
        {
            CameraSpliter spliterObject = cameraSpliter.GetComponent<CameraSpliter>();
            SetCameraObjects(spliterObject);
            return true;
        }

        return false;
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
