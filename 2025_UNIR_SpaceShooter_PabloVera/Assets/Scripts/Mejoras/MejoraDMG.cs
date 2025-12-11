using UnityEngine;

public class MejoraDMG : Mejora
{
    public PlayerActions playerActions;
    public int maxUppgrades;
    public override void AplicarMejora()
    {
        playerActions.IncreaseDMG();
    }

    public override bool RequisitosMejora()
    {
        if(playerActions.GetTimesIncreasedDMG() >= maxUppgrades)
            return false;
        else
            return true;
    }
}
