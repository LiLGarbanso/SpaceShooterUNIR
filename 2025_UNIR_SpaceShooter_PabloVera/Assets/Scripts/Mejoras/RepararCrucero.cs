using UnityEngine;

public class RepararCrucero : Mejora
{
    public CruceroEstelar cruceroEstelar;
    public int saludRecuperada;
    public override void AplicarMejora()
    {
        cruceroEstelar.RecuperarSalud(saludRecuperada);
    }

    public override bool RequisitosMejora()
    {
        if (cruceroEstelar.GetCurrentHP() == cruceroEstelar.cruceroData.HP)
            return false;
        else 
            return true;
    }
}
