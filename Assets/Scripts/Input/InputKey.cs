using UnityEngine;

public abstract class InputKey : ScriptableObject
{
    public KeyCode key;

    public abstract void MethodToInvoke(PlayerController playerController);
}