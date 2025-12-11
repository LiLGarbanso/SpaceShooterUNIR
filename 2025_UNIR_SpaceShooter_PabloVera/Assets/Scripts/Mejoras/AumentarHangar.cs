using UnityEngine;

public class AumentarHangar : Mejora
{
    public CruceroEstelar cruceroEstelar;

    public override void AplicarMejora()
    {
        cruceroEstelar.MejorarHangar();
    }

    public override bool RequisitosMejora()
    {
        return !cruceroEstelar.HasHangarUpgrade();
    }
}
