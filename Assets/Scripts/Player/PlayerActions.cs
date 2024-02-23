using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public enum PlayerStation
{
    None,
    Helm,
    Engineering,
    Weapons,
    Cargo
}

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private InputActionReference _takeSeatLeft;
    [SerializeField] private InputActionReference _takeSeatRight;

    public float switchSeatTime = 5f;

    public Transform playerMesh;

    public PlayerSeat helmSeat;
    public PlayerSeat engineeringSeat;
    public PlayerSeat weaponsSeat;
    public PlayerSeat cargoSeat;

    public PlayerStation _CurrentStation = PlayerStation.None;

    public LayerMask seatLayer;
    public Transform shipPhysicsCenter;
    public Transform shipMeshCenter;
    public GameObject shipSeatMesh;

    private PlayerSeat _currentSeat;
    private bool isHoldingSitButton = false;
    private float currentSitHoldTime = 0f;
    private Collider[] seatColliders;

    private ActionBasedContinuousMoveProvider continuousMoveProvider;
    private TeleportationProvider teleportationProvider;
    private ShipMoveProvider shipMoveProvider;
    private SnapTurnProvider snapTurnProvider;

    // Start is called before the first frame update
    void Start()
    {
        continuousMoveProvider = GetComponentInChildren<ActionBasedContinuousMoveProvider>();
        teleportationProvider = GetComponentInChildren<TeleportationProvider>();
        shipMoveProvider = GetComponentInChildren<ShipMoveProvider>();
        snapTurnProvider = GetComponentInChildren<SnapTurnProvider>();

        _takeSeatLeft.action.started += HandleHoldSeatButton;
        _takeSeatLeft.action.canceled += HandleReleaseSeatButton;

        _takeSeatRight.action.started += HandleHoldSeatButton;
        _takeSeatRight.action.canceled += HandleReleaseSeatButton;
    }

    // Update is called once per frame
    void Update()
    {
        // update cycle time if butten is being held
        if(isHoldingSitButton && currentSitHoldTime < switchSeatTime)
        {
            currentSitHoldTime += Time.deltaTime;

            // if one of the times is over the threshold then change seats
            if (seatColliders.Length > 0 && currentSitHoldTime >= switchSeatTime)
            {
                if (_currentSeat == null)
                {
                    PlayerSeat selectedSeat = seatColliders[0].GetComponent<PlayerSeat>();

                    if (selectedSeat != null)
                    {
                        LockIntoSeat(selectedSeat);

                        _currentSeat = selectedSeat;    

                        isHoldingSitButton = false;
                        currentSitHoldTime = 0f;
                    }
                }
                else
                {
                    RemoveFromSeat();

                    _currentSeat = null;

                    isHoldingSitButton = false;
                    currentSitHoldTime = 0f;
                }

            }
        }
    }
    
    private void LockIntoSeat(PlayerSeat selectedSeat)
    {
        selectedSeat.AddPlayerToSeat(playerMesh, transform);
        shipSeatMesh.SetActive(true);
        DisableMovement();

        switch (selectedSeat.stationType)
        {
            case PlayerStation.Helm:
                shipMoveProvider.enabled = true;
                _CurrentStation = PlayerStation.Helm;

                break;
            case PlayerStation.Weapons:
                shipMoveProvider.enabled = true;
                _CurrentStation = PlayerStation.Weapons;

                break;
            case PlayerStation.Engineering:
                break;
            case PlayerStation.Cargo:
                break;
        }
    }

    private void RemoveFromSeat()
    {
        switch (_CurrentStation)
        {
            case PlayerStation.Helm:
                shipMoveProvider.enabled = false;

                break;
            case PlayerStation.Weapons:
                shipMoveProvider.enabled = false;

                break;
            case PlayerStation.Engineering:
                break;
            case PlayerStation.Cargo:
                break;
        }

        _CurrentStation = PlayerStation.None;
        playerMesh.parent = shipMeshCenter;
        transform.parent = shipPhysicsCenter;
        shipSeatMesh.SetActive(false);
        EnableMovement();
    }

    private void EnableMovement()
    {
        snapTurnProvider.enabled = true;
        continuousMoveProvider.enabled = true;
    }

    private void DisableMovement()
    {
        continuousMoveProvider.enabled = false;
        snapTurnProvider.enabled = false;
    }

    private void HandleHoldSeatButton(InputAction.CallbackContext obj)
    {
        isHoldingSitButton = true;
        seatColliders = Physics.OverlapSphere(transform.position, 1f, seatLayer, QueryTriggerInteraction.Collide);
    }

    private void HandleReleaseSeatButton(InputAction.CallbackContext obj)
    {
        isHoldingSitButton = false;
        currentSitHoldTime = 0f;
    }
}
