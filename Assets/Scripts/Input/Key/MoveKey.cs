using UnityEngine;

[CreateAssetMenu(fileName = "Move Key", menuName="Input/New Move Key")]
public class MoveKey : InputKey
{
    public Vector2 direction;
    public override void MethodToInvoke(PlayerMovement playerMovement)
    {
        playerMovement.KeyPressed(direction);
    }
}
