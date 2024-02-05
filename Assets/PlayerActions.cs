using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private InputActionReference _switchSeats;
    public float currentSwitchTime = 0f;
    public float switchSeatTime = 5f;

    public Transform playerPhysics;
    public Transform helmPhysics;
    public Transform engineeringPhysics;
    public Transform helmSeatMesh;
    public Transform engineeringSeatMesh;

    private enum PlayerStation
    {
        Helm,
        Engineering
    }
    private PlayerStation _CurrentStation = PlayerStation.Helm;

    private bool isSwitching = false;

    // Start is called before the first frame update
    void Start()
    {
        _switchSeats.action.started += HandleStartSwitchSeats;
        _switchSeats.action.canceled += HandleStopSwitchSeats;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSwitching && currentSwitchTime < switchSeatTime)
        {
            currentSwitchTime += Time.deltaTime;
        } 

        if(currentSwitchTime >= switchSeatTime)
        {
            //Switch Seats
            if(_CurrentStation == PlayerStation.Engineering)
            {
                transform.parent = helmSeatMesh;
                playerPhysics.parent = helmPhysics;
                _CurrentStation = PlayerStation.Helm;

                ResetPlayerPositionAndRotation();
            } else
            {
                transform.parent = engineeringSeatMesh;
                playerPhysics.parent = engineeringPhysics;
                _CurrentStation = PlayerStation.Engineering;

                ResetPlayerPositionAndRotation();
            }

        }
    }

    private void ResetPlayerPositionAndRotation()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;

        playerPhysics.localPosition = Vector3.zero;
        playerPhysics.localEulerAngles = Vector3.zero;

        // reset bool and time
        currentSwitchTime = 0f;
        isSwitching = false;
    }

    private void HandleStartSwitchSeats(InputAction.CallbackContext obj)
    {
        isSwitching = true;
    }

    private void HandleStopSwitchSeats(InputAction.CallbackContext obj)
    {
        isSwitching = false;
        currentSwitchTime = 0f;
    }
}
