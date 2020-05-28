using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xMovementMultiplier = 2f;
    public float yMovementMultiplier = 1f;
    public float transitionTime = 1f;
    private bool isMoving = false;

    private void Start()
    {
        if (GameObject.FindWithTag("Respawn") != null)
            transform.position = GameObject.FindWithTag("Respawn").transform.position;        
    }

    public void AssignDelegate(InputHandler inputHandler)
    {
        inputHandler.onKeyPressed += KeyPressed;
    }

    private void KeyPressed(Vector2 direction)
    {
        if (isMoving) return;
        Vector3 scaledMovement = new Vector3(direction.x * xMovementMultiplier, direction.y * yMovementMultiplier, 0);
        TransitionToCell(scaledMovement);
    }

    private async void TransitionToCell(Vector3 movement)
    {
        isMoving = true;
        Vector3 originalPosition = transform.position;
        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(originalPosition, originalPosition + movement, t / transitionTime);
            await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
        }
        transform.position = originalPosition + movement;
        isMoving = false;
    }
}
