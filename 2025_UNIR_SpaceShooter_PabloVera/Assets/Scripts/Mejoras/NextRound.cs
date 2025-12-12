using UnityEngine;

public class NextRound : Mejora
{
    public PoolMejoras poolMejoras;
    public GameObject txtRound;
    public override void AplicarMejora()
    {
        poolMejoras.FinalizarCompras();
    }

    public override bool RequisitosMejora()
    {
        return true;
    }
}
