using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float xMovementMultiplier = 2f;
    public float yMovementMultiplier = 1f;
    public float transitionTime = 1f;
    public Animator playerAnimator;

    [Header("Moves Text Settings")]
    public Text movesText;
    public int numberOfMovesLeft = 10;
    public Color outOfMovesColor;
    public GameObject resetButton;
    public Vector3 outOfMovesScale;

    private bool isMoving = false;
    private bool isFacingRight = true;
    private bool isTouchingCollider = false;
    private void Start()
    {
        movesText.text = numberOfMovesLeft.ToString();
        if (GameObject.FindWithTag("Respawn") != null)
            transform.position = GameObject.FindWithTag("Respawn").transform.position;        
    }

    public async void KeyPressed(Vector2 direction)
    {
        if (isMoving) return;
        if (numberOfMovesLeft <= 0)
        {
            FlashMovesAndReset();
            return;
        }

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
            movesText.text = numberOfMovesLeft.ToString();
        }
    }

    private async Task TransitionToCell(Vector3 movement)
    {
        if ((movement.x < 0 && isFacingRight) || (movement.x > 0 && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(Vector3.up * 180);
        }

        playerAnimator.SetTrigger("isWalking");
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
        Debug.Log("Collided with " + collision.gameObject.name);
    }

    private async void FlashMovesAndReset()
    {
        float animTime = 0.35f; // for each flash
        Color originalColor = movesText.color;
        Vector3 originalScale = resetButton.transform.localScale;

        for (int numberOfFlashes = 2; numberOfFlashes > 0; numberOfFlashes--)
        {
            for (float t = 0; t < animTime; t += Time.deltaTime)
            {
                float sinValue = Mathf.Sin(t * Mathf.PI / animTime);
                movesText.color = (originalColor * (1 - sinValue)) + (outOfMovesColor * sinValue);
                resetButton.transform.localScale = (originalScale * (1 - sinValue)) + (outOfMovesScale * sinValue);
                await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
            }

            movesText.color = originalColor;
            resetButton.transform.localScale = originalScale;
        }
    }
}
