using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShipConditionManager : MonoBehaviour
{
    public static ShipConditionManager instance { get; private set; }
    [SerializeField] private Vector3 transformOriginOffset;
    [SerializeField] private int deliveryPoints = 0;
    [SerializeField] private Button warpButton;

    private Rigidbody physicsShip;
    private Vector3 resetPosition = new Vector3(0, 0, 0);
    private Quaternion resetRotation = new Quaternion(0, 0, 0, 0);

    public WorldWrapper worldWrapper;
    public bool nearWarpGate = false;
    public SceneField warpScene;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Player Condition Manager");
        }
        instance = this;

        physicsShip = GetComponentInChildren<Rigidbody>();
        transformOriginOffset = new Vector3(0, 0, 0);

        LoadShipData();
    }

    // TEST THIS CODE
    // WORLD REPOSITION CODE NOT WORKING
    //public void LateUpdate()
    //{
    //    if (worldWrapper)
    //    {
    //        float movementAmount = Mathf.Abs(physicsShip.position.x);
    //        movementAmount += Mathf.Abs(physicsShip.position.y);
    //        movementAmount += Mathf.Abs(physicsShip.position.z);

    //        if (movementAmount > 100)
    //        {
    //            // Used to keep ship at origin and avoid floating point origin problems
    //            //update world with oposite position and rotation
    //            transformOriginOffset += physicsShip.position * -1;
    //            worldWrapper.transform.localPosition = transformOriginOffset;

    //            //reset back to origin
    //            physicsShip.position = resetPosition;
    //        }
    //    }
    //}

    public void SaveShipData()
    {
        GameSaveManager.SaveShipData(physicsShip.transform);
    }

    public void LoadShipData()
    {
        ShipData shipData = GameSaveManager.LoadShipData();

        SetShipPosition(shipData.Position);
        SetShipRotation(shipData.Rotation);
    }

    public void NearWarpGate(SceneField newScene)
    {
        nearWarpGate = true;
        warpScene = newScene;
        warpButton.interactable = true;
    }

    public void LeaveWarpGate()
    {
        nearWarpGate = false;
        warpScene = null;
        warpButton.interactable = false;
    }

    public void WarpShip()
    {
        if(warpScene != null)
        {
            GameSceneManager.instance.InitiateWarp(warpScene);
        }
    }

    public void MoveShip(Vector3 newPosition)
    {
        SetShipPosition(newPosition);
    }

    private void SetShipPosition(Vector3 newPosition)
    {
        physicsShip.position = newPosition;
    }

    private void SetShipRotation(Quaternion newRotation)
    {
        physicsShip.rotation = newRotation;
    }
}
