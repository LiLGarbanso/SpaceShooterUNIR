using UnityEngine;

public class MejoraShield : Mejora
{
    public PlayerActions playerActions;
    public override void AplicarMejora()
    {
        playerActions.ShieldUpgrade();
    }

    public override bool RequisitosMejora()
    {
        return !playerActions.HasShieldUpgrade();
    }
}
