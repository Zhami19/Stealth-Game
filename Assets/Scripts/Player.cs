using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // movement values
    [Header("Movement Values")]
    [SerializeField] float moveSpeed = 1.0f;

    // references
    Rigidbody rb;

    // variables
    Vector3 movementVector;

    public enum PlayerStates
    {
        Normal,
        Sneaking
    }

    public PlayerStates playerState;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerState = PlayerStates.Normal;

    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        movementVector = new Vector3(inputVector.x, 0, inputVector.y);
    }

    public void OnSneaking(InputAction.CallbackContext context)
    {
        if (playerState == PlayerStates.Normal)
        {
            playerState = PlayerStates.Sneaking;
            moveSpeed /= 2;
        }
        else if (playerState == PlayerStates.Sneaking)
        {
            playerState = PlayerStates.Normal;
            moveSpeed *= 2;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementVector * moveSpeed;
    }
}
