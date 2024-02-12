using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private InputActionReference _cycleSeatsForward;
    [SerializeField] private InputActionReference _cycleSeatsBack;

    public float switchSeatTime = 5f;

    public Transform playerPhysics;

    public Transform helmPhysics;
    public Transform engineeringPhysics;
    public Transform weaponsPhysics;
    public Transform cargoPhysics;

    public Transform helmMesh;
    public Transform engineeringMesh;
    public Transform weaponsMesh;
    public Transform cargoMesh;

    public enum PlayerStation
    {
        Helm,
        Engineering,
        Weapons,
        Cargo
    }
    public PlayerStation _CurrentStation = PlayerStation.Helm;

    private bool isCyclingForward = false;
    private float currentCycleForwardTime = 0f;

    private bool isCyclingBack = false;
    private float currentCycleBackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _cycleSeatsForward.action.started += HandleStartCycleForward;
        _cycleSeatsForward.action.canceled += HandleEndCycleForward;

        _cycleSeatsBack.action.started += HandleStartCycleBack;
        _cycleSeatsBack.action.canceled += HandleEndCycleBack;
    }

    // Update is called once per frame
    void Update()
    {
        // update cycle time if butten is being held
        if(isCyclingForward && currentCycleForwardTime < switchSeatTime)
        {
            currentCycleForwardTime += Time.deltaTime;
        }
        else if (isCyclingBack && currentCycleBackTime < switchSeatTime)
        {
            currentCycleBackTime += Time.deltaTime;
        }

        // if one of the times is over the threshold then change seats
        if (currentCycleForwardTime >= switchSeatTime)
        {
            switch (_CurrentStation)
            {
                case PlayerStation.Helm:
                    ChangePlayerSeat(PlayerStation.Weapons); break;
                case PlayerStation.Weapons:
                    ChangePlayerSeat(PlayerStation.Engineering); break;
                case PlayerStation.Engineering:
                    ChangePlayerSeat(PlayerStation.Cargo); break;
                case PlayerStation.Cargo:
                    ChangePlayerSeat(PlayerStation.Helm); break;
            }

            // reset bool and time
            currentCycleForwardTime = 0f;
            isCyclingForward = false;
        } else if (currentCycleBackTime >= switchSeatTime)
        {
            switch (_CurrentStation)
            {
                case PlayerStation.Helm:
                    ChangePlayerSeat(PlayerStation.Cargo); break;
                case PlayerStation.Weapons:
                    ChangePlayerSeat(PlayerStation.Helm); break;
                case PlayerStation.Engineering:
                    ChangePlayerSeat(PlayerStation.Weapons); break;
                case PlayerStation.Cargo:
                    ChangePlayerSeat(PlayerStation.Engineering); break;
            }

            // reset bool and time
            currentCycleBackTime = 0f;
            isCyclingBack = false;
        }
    }

    private void ChangePlayerSeat(PlayerStation selectedStation)
    {
        //Switch Seats
        switch (selectedStation)
        {
            case PlayerStation.Helm:
                transform.parent = helmMesh;
                playerPhysics.parent = helmPhysics;
                _CurrentStation = PlayerStation.Helm;

                break;
            case PlayerStation.Weapons:
                transform.parent = weaponsMesh;
                playerPhysics.parent = weaponsPhysics;
                _CurrentStation = PlayerStation.Weapons;

                break;
            case PlayerStation.Engineering:
                transform.parent = engineeringMesh;
                playerPhysics.parent = engineeringPhysics;
                _CurrentStation = PlayerStation.Engineering;

                break;
            case PlayerStation.Cargo:
                transform.parent = cargoMesh;
                playerPhysics.parent = cargoPhysics;
                _CurrentStation = PlayerStation.Cargo;

                break;
        }

        ResetPlayerPositionAndRotation();
    }

    private void ResetPlayerPositionAndRotation()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;

        playerPhysics.localPosition = Vector3.zero;
        playerPhysics.localEulerAngles = Vector3.zero;
    }

    private void HandleStartCycleForward(InputAction.CallbackContext obj)
    {
        if(!isCyclingBack)
        {
            isCyclingForward = true;
        }
    }

    private void HandleEndCycleForward(InputAction.CallbackContext obj)
    {
        isCyclingForward = false;
        currentCycleForwardTime = 0f;
    }

    private void HandleStartCycleBack(InputAction.CallbackContext obj)
    {
        if (!isCyclingForward)
        {
            isCyclingBack = true;
        }
    }

    private void HandleEndCycleBack(InputAction.CallbackContext obj)
    {
        isCyclingBack = false;
        currentCycleBackTime = 0f;
    }
}
