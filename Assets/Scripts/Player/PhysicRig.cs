using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRig : MonoBehaviour
{
    public Transform playerHead;
    public Transform leftController;
    public Transform rightController;

    public Transform bodyHead;
    public Transform bodyLeftHand;
    public Transform bodyRightHand;

    [Header("Height Settings")]
    public float bodyHeightMin = 0.5f;
    public float bodyHeightMax = 2;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController.height = Mathf.Clamp(bodyHead.localPosition.y, bodyHeightMin, bodyHeightMax);
        characterController.center = new Vector3(bodyHead.localPosition.x, characterController.height / 2, bodyHead.localPosition.z);

        //leftController.localPosition = bodyLeftHand.localPosition;
        //leftController.localRotation = bodyLeftHand.localRotation;

        //rightController.localPosition = bodyRightHand.localPosition;
        //rightController.localRotation = bodyRightHand.localRotation;

        bodyHead.localPosition = playerHead.localPosition;
        bodyHead.localRotation = playerHead.localRotation;
    }
}
