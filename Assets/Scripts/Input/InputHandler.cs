using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Input")]
    public List<InputKey> inputs = new List<InputKey>();
    public PlayerController playerController;
    public bool inputEnabled = true;

    private void Update()
    {
        if (inputEnabled)
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
}
