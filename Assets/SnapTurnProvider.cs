using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class SnapTurnProvider : LocomotionProvider
{
    [SerializeField]
    [Tooltip("The number of degrees clockwise to rotate when snap turning clockwise.")]
    float m_TurnAmount = 45f;
    /// <summary>
    /// The number of degrees clockwise Unity rotates the rig when snap turning clockwise.
    /// </summary>
    public float turnAmount
    {
        get => m_TurnAmount;
        set => m_TurnAmount = value;
    }

    [SerializeField]
    [Tooltip("The amount of time that the system will wait before starting another snap turn.")]
    float m_DebounceTime = 0.5f;
    /// <summary>
    /// The amount of time that Unity waits before starting another snap turn.
    /// </summary>
    public float debounceTime
    {
        get => m_DebounceTime;
        set => m_DebounceTime = value;
    }

    [SerializeField]
    [Tooltip("Controls whether to enable left & right snap turns.")]
    bool m_EnableTurnLeftRight = true;
    /// <summary>
    /// Controls whether to enable left and right snap turns.
    /// </summary>
    /// <seealso cref="enableTurnAround"/>
    public bool enableTurnLeftRight
    {
        get => m_EnableTurnLeftRight;
        set => m_EnableTurnLeftRight = value;
    }

    [SerializeField]
    [Tooltip("Controls whether to enable 180° snap turns.")]
    bool m_EnableTurnAround = true;
    /// <summary>
    /// Controls whether to enable 180° snap turns.
    /// </summary>
    /// <seealso cref="enableTurnLeftRight"/>
    public bool enableTurnAround
    {
        get => m_EnableTurnAround;
        set => m_EnableTurnAround = value;
    }

    [SerializeField]
    [Tooltip("The time (in seconds) to delay the first turn after receiving initial input for the turn.")]
    float m_DelayTime;

    [SerializeField]
    [Tooltip("The Input System Action that will be used to read Snap Turn data from the left hand controller. Must be a Value Vector2 Control.")]
    InputActionProperty m_LeftHandSnapTurnAction = new InputActionProperty(new InputAction("Left Hand Snap Turn", expectedControlType: "Vector2"));
    /// <summary>
    /// The Input System Action that Unity uses to read Snap Turn data sent from the left hand controller. Must be a <see cref="InputActionType.Value"/> <see cref="Vector2Control"/> Control.
    /// </summary>
    public InputActionProperty leftHandSnapTurnAction
    {
        get => m_LeftHandSnapTurnAction;
        set => SetInputActionProperty(ref m_LeftHandSnapTurnAction, value);
    }

    [SerializeField]
    [Tooltip("The Input System Action that will be used to read Snap Turn data from the right hand controller. Must be a Value Vector2 Control.")]
    InputActionProperty m_RightHandSnapTurnAction = new InputActionProperty(new InputAction("Right Hand Snap Turn", expectedControlType: "Vector2"));
    /// <summary>
    /// The Input System Action that Unity uses to read Snap Turn data sent from the right hand controller. Must be a <see cref="InputActionType.Value"/> <see cref="Vector2Control"/> Control.
    /// </summary>
    public InputActionProperty rightHandSnapTurnAction
    {
        get => m_RightHandSnapTurnAction;
        set => SetInputActionProperty(ref m_RightHandSnapTurnAction, value);
    }

    /// <summary>
    /// The time (in seconds) to delay the first turn after receiving initial input for the turn.
    /// Subsequent turns while holding down input are delayed by the <see cref="debounceTime"/>, not the delay time.
    /// This delay can be used, for example, as time to set a tunneling vignette effect as a VR comfort option.
    /// </summary>
    public float delayTime
    {
        get => m_DelayTime;
        set => m_DelayTime = value;
    }

    public Transform physicsCameraObject;

    float m_CurrentTurnAmount;
    float m_TimeStarted;
    float m_DelayStartTime;

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void Update()
    {
        // Wait for a certain amount of time before allowing another turn.
        if (m_TimeStarted > 0f && (m_TimeStarted + m_DebounceTime < Time.time))
        {
            m_TimeStarted = 0f;
            return;
        }

        // Reset to Idle state at the beginning of the update loop (rather than the end)
        // so that anything that needs to be aware of the Done state can trigger, such as
        // the vignette provider or another comfort mode option.
        if (locomotionPhase == LocomotionPhase.Done)
            locomotionPhase = LocomotionPhase.Idle;

        var input = ReadInput();
        var amount = GetTurnAmount(input);
        if (Mathf.Abs(amount) > 0f || locomotionPhase == LocomotionPhase.Started)
        {
            StartTurn(amount);
        }
        else if (Mathf.Approximately(m_CurrentTurnAmount, 0f) && locomotionPhase == LocomotionPhase.Moving)
        {
            locomotionPhase = LocomotionPhase.Done;
        }

        if (locomotionPhase == LocomotionPhase.Moving && Math.Abs(m_CurrentTurnAmount) > 0f && BeginLocomotion())
        {
            var xrOrigin = system.xrOrigin;
            if (xrOrigin != null)
            {
                xrOrigin.transform.RotateAround(physicsCameraObject.position, Vector3.up, amount);
            }
            else
            {
                locomotionPhase = LocomotionPhase.Done;
            }
            m_CurrentTurnAmount = 0f;
            EndLocomotion();

            if (Mathf.Approximately(amount, 0f))
                locomotionPhase = LocomotionPhase.Done;
        }
    }

    /// <inheritdoc />
    protected override void Awake()
    {
        base.Awake();
        if (system != null && m_DelayTime > 0f && m_DelayTime > system.timeout)
            Debug.LogWarning($"Delay Time ({m_DelayTime}) is longer than the Locomotion System's Timeout ({system.timeout}).", this);
    }

    /// <summary>
    /// Determines the turn amount in degrees for the given <paramref name="input"/> vector.
    /// </summary>
    /// <param name="input">Input vector, such as from a thumbstick.</param>
    /// <returns>Returns the turn amount in degrees for the given <paramref name="input"/> vector.</returns>
    protected virtual float GetTurnAmount(Vector2 input)
    {
        if (input == Vector2.zero)
            return 0f;

        var cardinal = CardinalUtility.GetNearestCardinal(input);
        switch (cardinal)
        {
            case Cardinal.North:
                break;
            case Cardinal.South:
                if (m_EnableTurnAround)
                    return 180f;
                break;
            case Cardinal.East:
                if (m_EnableTurnLeftRight)
                    return m_TurnAmount;
                break;
            case Cardinal.West:
                if (m_EnableTurnLeftRight)
                    return -m_TurnAmount;
                break;
            default:
                Assert.IsTrue(false, $"Unhandled {nameof(Cardinal)}={cardinal}");
                break;
        }

        return 0f;
    }

    /// <summary>
    /// Begins turning locomotion.
    /// </summary>
    /// <param name="amount">Amount to turn.</param>
    protected void StartTurn(float amount)
    {
        if (m_TimeStarted > 0f)
            return;

        if (!CanBeginLocomotion())
            return;

        if (locomotionPhase == LocomotionPhase.Idle)
        {
            locomotionPhase = LocomotionPhase.Started;
            m_DelayStartTime = Time.time;
        }

        // We set the m_CurrentTurnAmount here so we can still trigger the turn
        // in the case where the input is released before the delay timeout happens.
        if (Math.Abs(amount) > 0f)
            m_CurrentTurnAmount = amount;

        // Wait for configured Delay Time
        if (m_DelayTime > 0f && Time.time - m_DelayStartTime < m_DelayTime)
            return;

        locomotionPhase = LocomotionPhase.Moving;
        m_TimeStarted = Time.time;
    }

    internal void FakeStartTurn(bool isLeft)
    {
        StartTurn(isLeft ? -m_TurnAmount : m_TurnAmount);
    }

    internal void FakeStartTurnAround()
    {
        StartTurn(180f);
    }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void OnEnable()
    {
        m_LeftHandSnapTurnAction.EnableDirectAction();
        m_RightHandSnapTurnAction.EnableDirectAction();
    }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void OnDisable()
    {
        m_LeftHandSnapTurnAction.DisableDirectAction();
        m_RightHandSnapTurnAction.DisableDirectAction();
    }

    /// <inheritdoc />
    protected Vector2 ReadInput()
    {
        var leftHandValue = m_LeftHandSnapTurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        var rightHandValue = m_RightHandSnapTurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

        return leftHandValue + rightHandValue;
    }

    void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
    {
        if (Application.isPlaying)
            property.DisableDirectAction();

        property = value;

        if (Application.isPlaying && isActiveAndEnabled)
            property.EnableDirectAction();
    }
}