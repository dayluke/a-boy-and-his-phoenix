using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int numberOfMovesLeft = 10;
    public float xMovementMultiplier = 2f;
    public float yMovementMultiplier = 1f;
    public float transitionTime = 1f;
    private bool isMoving = false;
    private bool isTouchingCollider = false;

    private void Start()
    {
        if (GameObject.FindWithTag("Respawn") != null)
            transform.position = GameObject.FindWithTag("Respawn").transform.position;        
    }

    public void AssignDelegate(InputHandler inputHandler)
    {
        inputHandler.onKeyPressed += KeyPressed;
    }

    private async void KeyPressed(Vector2 direction)
    {
        if (isMoving || numberOfMovesLeft <= 0) return;
        Vector3 scaledMovement = new Vector3(direction.x * xMovementMultiplier, direction.y * yMovementMultiplier, 0);
        await TransitionToCell(scaledMovement);
        
        if (isTouchingCollider)
        {
            await TransitionToCell(-scaledMovement);
            isTouchingCollider = false;
        }
        else
        {
            numberOfMovesLeft--;
        }
    }

    private async Task TransitionToCell(Vector3 movement)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isTouchingCollider = true;
    }
}
