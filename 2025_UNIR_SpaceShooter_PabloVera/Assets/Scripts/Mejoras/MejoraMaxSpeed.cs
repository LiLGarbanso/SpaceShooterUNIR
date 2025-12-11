using UnityEngine;

public class MejoraMaxSpeed : Mejora
{
    public PlayerMovement playerMovement;
    public int maxUpgrades;
    public float increment;
    public override void AplicarMejora()
    {
        playerMovement.IncreaseMaxSpeed(increment);
    }

    public override bool RequisitosMejora()
    {
        if (playerMovement.GetTimesSpeedIncreased() >= maxUpgrades)
            return false;
        else
            return true;
    }
}
