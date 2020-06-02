using UnityEngine;

[CreateAssetMenu(fileName = "Ability Key", menuName="Input/New Ability Key")]
public class AbilityKey : InputKey
{
    public AbilityType abilityType;
    public override void MethodToInvoke(PlayerController playerController)
    {
        playerController.playerAbility.ActivateAbility(abilityType);
    }
}
