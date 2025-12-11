using UnityEngine;

public class ConstruirNave : Mejora
{
    public CruceroEstelar cruceroEstelar;
    public override void AplicarMejora()
    {
        cruceroEstelar.ConstruirNave();
    }

    public override bool RequisitosMejora()
    {
        if (cruceroEstelar.GetCurrentNaves() == cruceroEstelar.cruceroData.navesIniciales)
            return false;
        else
            return true;
    }
}
