using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Input")]
    public List<InputKey> inputs = new List<InputKey>();
    public KeyCode fireAbilityKey = KeyCode.E;
    public KeyCode tearAbilityKey = KeyCode.Q;

    #region delegates

    public delegate void OnKeyPressed(Vector2 dir);
    public OnKeyPressed onKeyPressed;
    public delegate void OnFireAbility();
    public OnFireAbility onFireAbility;
    public delegate void OnTearAbility();
    public OnTearAbility onTearAbility;

    #endregion

    public PlayerMovement playerMovement;
    public PlayerAbility playerAbility;

    private void Start()
    {
        playerMovement.AssignDelegate(this);
        playerAbility.AssignDelegate(this);
    }

    private void Update()
    {
        foreach (InputKey input in inputs)
        {
            if (Input.GetKeyDown(input.key))
            {
                onKeyPressed?.Invoke(input.direction);
                break;
            }
        }

        if (Input.GetKeyDown(fireAbilityKey))
        {
            onFireAbility?.Invoke();
            return;
        }

        if (Input.GetKeyDown(tearAbilityKey))
        {
            onTearAbility?.Invoke();
            return;
        }
    }
}

[System.Serializable]
public class InputKey
{
    public KeyCode key;
    public Vector2 direction;
}
