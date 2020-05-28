using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public void AssignDelegate(InputHandler inputHandler)
    {
        inputHandler.onFireAbility += FireAbility;
        inputHandler.onTearAbility += TearAbility;
    }

    private void FireAbility()
    {
        Debug.Log("FIRE");
    }

    private void TearAbility()
    {
        Debug.Log("TEAR");
    }
}
