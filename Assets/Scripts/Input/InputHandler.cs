using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Input")]
    public PlayerMovement playerMovement;
    public bool inputEnabled = true;

    public void OnArrowPressed(string dir)
    {
        string[] vectors = dir.Split(',');
        if (inputEnabled) playerMovement.KeyPressed(new Vector2(float.Parse(vectors[0]), float.Parse(vectors[1])));
    }
}
