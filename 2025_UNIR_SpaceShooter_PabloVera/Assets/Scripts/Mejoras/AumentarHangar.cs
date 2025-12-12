using UnityEngine;

public class AumentarHangar : Mejora
{
    public CruceroEstelar cruceroEstelar;

    public override void AplicarMejora()
    {
        cruceroEstelar.MejorarHangar();
        EventBus.EliminarMejoraUnica(this);
    }

    public override bool RequisitosMejora()
    {
        return !cruceroEstelar.HasHangarUpgrade();
    }
}
