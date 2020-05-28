using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public List<InputKey> inputs = new List<InputKey>();
    public delegate void OnKeyPressed(Vector2 dir);
    public OnKeyPressed onKeyPressed;
    public PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement.AssignDelegate(this);
    }

    private void Update()
    {
        foreach (InputKey input in inputs)
        {
            if (Input.GetKeyDown(input.key))
            {
                onKeyPressed?.Invoke(input.direction);
            }
        }    
    }
}

[System.Serializable]
public class InputKey
{
    public KeyCode key;
    public Vector2 direction;
}
