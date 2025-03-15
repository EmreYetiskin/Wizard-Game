using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    //Events
    public event EventHandler OnJumpAction;
    public event EventHandler OnBasicAttackAction;
    public event EventHandler OnSelfHealAction;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        playerInputActions.Enable();
        playerInputActions.Gameplay.Jump.performed += Jump_performed;
        playerInputActions.Gameplay.BasicAttack.performed += BasicAttack_performed;
        playerInputActions.Gameplay.SelfHeal.performed += SelfHeal_performed;
    }

    private void SelfHeal_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSelfHealAction?.Invoke(this, EventArgs.Empty);
    }

    private void BasicAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnBasicAttackAction?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Gameplay.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
