using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputController : MonoBehaviour
{
    private InputSystem_Actions player_input;

    public event EventHandler onPanStartAction;
    public event EventHandler onPanEndAction;

    public event EventHandler onDragStartAction;

    private void Awake()
    {
        player_input = new InputSystem_Actions();
        player_input.Player.Enable();

        player_input.Player.RightClick.performed += ctx => onPanStartAction?.Invoke(this, EventArgs.Empty);
        player_input.Player.RightClick.canceled += ctx => onPanEndAction?.Invoke(this, EventArgs.Empty);

        player_input.Player.LeftClick.performed += ctx => onDragStartAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 getCameraMovement()
    {
        Vector2 movement_vector = player_input.Player.Move.ReadValue<Vector2>();
        movement_vector = movement_vector.normalized;
        return movement_vector;
    }

    public Vector2 getMousePosition()
    {
        Debug.Log(player_input.Player.PointerPosition.ReadValue<Vector2>());
        return player_input.Player.PointerPosition.ReadValue<Vector2>();
    }
}
