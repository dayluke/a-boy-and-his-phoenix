using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Input")]
    public List<InputKey> inputs = new List<InputKey>();
    public PlayerMovement playerMovement;
    public bool inputEnabled = true;

    private void Update()
    {
        if (inputEnabled)
        {
            foreach (InputKey input in inputs)
            {
                if (Input.GetKeyDown(input.key))
                {
                    input.MethodToInvoke(playerMovement);
                    break;
                }
            }
        }
    }
}
