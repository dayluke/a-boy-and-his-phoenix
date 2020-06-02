using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public void ActivateAbility(AbilityType abilityType)
    {
        if (abilityType == AbilityType.FIRE) FireAbility();
        else if (abilityType == AbilityType.TEAR) TearAbility();
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

public enum AbilityType
{
    FIRE,
    TEAR
}