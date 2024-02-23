using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeat : MonoBehaviour
{
    public Transform meshCenter;
    public PlayerStation stationType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayerToSeat(Transform playerMesh, Transform playerPhysics)
    {
        playerPhysics.parent = transform;
        playerPhysics.localPosition = Vector3.zero;
        playerPhysics.localEulerAngles = Vector3.zero;

        playerMesh.parent = meshCenter;
        playerMesh.localPosition = Vector3.zero;
        playerMesh.localEulerAngles = Vector3.zero;
    }
}
