using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions
{
    private PlayerInputActions _inputActions;

    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action ShootEvent;
    public event Action InteractEvent;
    public event Action PauseEvent;

    void Start()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
            SwitchMap(_inputActions.Player);
        }
    }

    private void SwitchMap(InputActionMap map)
    {
        _inputActions.Disable();
        map.Enable();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent?.Invoke();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ShootEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
        }
    }


}
