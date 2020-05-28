﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xMovementMultiplier = 2f;
    public float yMovementMultiplier = 1f;

    public void AssignDelegate(InputHandler inputHandler)
    {
        inputHandler.onKeyPressed += KeyPressed;
    }

    private void KeyPressed(Vector2 direction)
    {
        Vector3 scaledMovement = new Vector3(direction.x * xMovementMultiplier, direction.y * yMovementMultiplier, 0);
        transform.position += scaledMovement;
    }
}