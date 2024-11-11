using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerData
{
    public Vector3 Position { 
        get { 
            return new Vector3(playerLocation[0], playerLocation[1], playerLocation[2]); 
        } 
        set {
            playerLocation = new float[3];
            playerLocation[0] = value.x;
            playerLocation[1] = value.y;
            playerLocation[2] = value.z;
        } 
    }
    private float[] playerLocation;

    public Quaternion Rotation
    {
        get
        {
            return new Quaternion(playerRotation[0], playerRotation[1], playerRotation[2], playerRotation[3]);
        }
        set
        {
            playerRotation = new float[4];
            playerRotation[0] = value.x;
            playerRotation[1] = value.y;
            playerRotation[2] = value.z;
            playerRotation[3] = value.w;
        }
    }
    private float[] playerRotation;

    private string YawControls;
    private string PitchControls;
    private string RollControls;

    public PlayerData(Vector3 position, Quaternion rotation)
    {
        playerLocation = new float[3];
        playerLocation[0] = position.x;
        playerLocation[1] = position.y;
        playerLocation[2] = position.z;

        playerRotation = new float[4];
        playerRotation[0] = rotation.x;
        playerRotation[1] = rotation.y;
        playerRotation[2] = rotation.z;
        playerRotation[3] = rotation.w;
    }
}
