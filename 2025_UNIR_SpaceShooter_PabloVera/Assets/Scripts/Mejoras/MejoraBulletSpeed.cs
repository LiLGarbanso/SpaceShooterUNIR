using UnityEngine;

public class MejoraBulletSpeed : Mejora
{
    public PlayerActions playerActions;
    public int maxUpgrades;
    public float increment;
    public override void AplicarMejora()
    {
        playerActions.IncreaseBulletSpeed(increment);
    }

    public override bool RequisitosMejora()
    {
        if (playerActions.GetTimesIncreasedBulletSpeed() >= maxUpgrades)
            return false;
        else
            return true;
    }
}
