using UnityEngine;

public class ActivarBarreras : Mejora
{
    public CruceroEstelar cruceroEstelar;
    public override void AplicarMejora()
    {
        cruceroEstelar.ActivarBarreras();
    }

    public override bool RequisitosMejora()
    {
        //throw new System.NotImplementedException();
        return true;
    }
}
