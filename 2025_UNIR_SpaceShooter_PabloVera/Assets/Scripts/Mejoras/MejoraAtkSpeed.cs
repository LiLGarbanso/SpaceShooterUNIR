using UnityEngine;

public class MejoraAtkSpeed : Mejora
{
    public PlayerActions playerActions;
    public int maxUppgrades;
    public float speedReduction;
    public override void AplicarMejora()
    {
        playerActions.IncreaseAtckSpeed(speedReduction);
    }

    public override bool RequisitosMejora()
    {
        if (playerActions.GetTimesIncreasedAtkSpeed() >= maxUppgrades)
            return false;
        else
            return true;
    }
}
