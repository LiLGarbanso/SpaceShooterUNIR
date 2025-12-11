using UnityEngine;

public class MejoraMultiShoot : Mejora
{
    public PlayerActions playerActions;

    public override void AplicarMejora()
    {
        playerActions.MultiShootUpgrade();
    }

    public override bool RequisitosMejora()
    {
        return !playerActions.HasMultiShootUpgrade();
    }
}
