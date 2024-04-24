using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxcamera : MonoBehaviour
{

    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform shipParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localRotation = playerCamera.localRotation;
        transform.localPosition = shipParent.InverseTransformPoint(playerCamera.position);
    }
}
