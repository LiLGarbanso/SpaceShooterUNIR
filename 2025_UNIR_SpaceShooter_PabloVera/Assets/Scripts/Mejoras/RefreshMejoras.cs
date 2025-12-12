using System.Collections;
using UnityEngine;

public class RefreshMejoras : Mejora
{
    public PoolMejoras poolMejoras;
    public override void AplicarMejora()
    {
        poolMejoras.Refresh();
    }

    public override bool RequisitosMejora()
    {
        return true;
    }

    public void Reactivar()
    {
        StartCoroutine(ReactiveDelay());
    }

    IEnumerator ReactiveDelay()
    {
        yield return new WaitForSeconds(3f);
        Activar();
        yield return null;
    }
}
