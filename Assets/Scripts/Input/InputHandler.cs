using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Input")]
    public List<InputKey> inputs = new List<InputKey>();
    public PlayerController playerController;

    private void Update()
    {
        foreach (InputKey input in inputs)
        {
            if (Input.GetKeyDown(input.key))
            {
                input.MethodToInvoke(playerController);
                break;
            }
        }
    }
}
