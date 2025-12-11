using UnityEngine;

public class MejoraAcceleration : Mejora
{
    public PlayerMovement playerMovement;
    public int maxUpgrades;
    public float increment;
    public override void AplicarMejora()
    {
        playerMovement.IncreaseAcceleration(increment);
    }

    public override bool RequisitosMejora()
    {
        if (playerMovement.GetTimesAccelerationIncreased() >= maxUpgrades)
            return false;
        else
            return true;
    }
}
