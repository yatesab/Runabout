using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMoveProvider : MonoBehaviour
{

    public PropulsionSystem propulsionSystem;
    public enum StickMovementDirection
    {
        PitchYaw,
        Roll
    }
    public StickMovementDirection leftStickMovementSetting;
    public StickMovementDirection rightStickMovementSetting;

    [SerializeField] private InputActionReference _leftStickAction;
    [SerializeField] private InputActionReference _rightStickAction;

    // Start is called before the first frame update
    void OnEnable()
    {
        // Set up Stick Actions
        _leftStickAction.action.performed += OnLeftStickPressed;
        _leftStickAction.action.canceled += OnLeftStickStopped;

        _rightStickAction.action.performed += OnRightStickPressed;
        _rightStickAction.action.canceled += OnRightStickStopped;
    }

    // Start is called before the first frame update
    void OnDisable()
    {
        // Set up Stick Actions
        _leftStickAction.action.performed -= OnLeftStickPressed;
        _leftStickAction.action.canceled -= OnLeftStickStopped;

        _rightStickAction.action.performed -= OnRightStickPressed;
        _rightStickAction.action.canceled -= OnRightStickStopped;
    }

    private void OnLeftStickPressed(InputAction.CallbackContext obj)
    {
        switch (leftStickMovementSetting)
        {
            case StickMovementDirection.PitchYaw:
                propulsionSystem.PitchYaw = obj.ReadValue<Vector2>(); break;
            case StickMovementDirection.Roll:
                propulsionSystem.Roll = obj.ReadValue<Vector2>().x; break;
        }
    }

    private void OnLeftStickStopped(InputAction.CallbackContext obj)
    {
        switch (leftStickMovementSetting)
        {
            case StickMovementDirection.PitchYaw:
                propulsionSystem.PitchYaw = Vector2.zero; break;
            case StickMovementDirection.Roll:
                propulsionSystem.Roll = 0f; break;
        }
    }

    private void OnRightStickPressed(InputAction.CallbackContext obj)
    {
        switch (rightStickMovementSetting)
        {
            case StickMovementDirection.PitchYaw:
                propulsionSystem.PitchYaw = obj.ReadValue<Vector2>(); break;
            case StickMovementDirection.Roll:
                propulsionSystem.Roll = obj.ReadValue<Vector2>().x; break;
        }
    }

    private void OnRightStickStopped(InputAction.CallbackContext obj)
    {
        switch (rightStickMovementSetting)
        {
            case StickMovementDirection.PitchYaw:
                propulsionSystem.PitchYaw = Vector2.zero; break;
            case StickMovementDirection.Roll:
                propulsionSystem.Roll = 0f; break;
        }
    }
}
