using System.Collections.Generic;
using UnityEngine;

public class PoolMejoras : MonoBehaviour
{
    public List<Mejora> mejorasUnicas, mejorasNave, mejorasCrucero, currentPool;
    public List<Transform> puntosVenta;
    private int mejorasActivas;
    public RefreshMejoras refresh;
    public RoundMannager rm;
    public NextRound nxt;

    private void Start()
    {
        //MostrarMejorasRand();
        refresh.Desactivar();
        nxt.Desactivar();
        nxt.txtRound.SetActive(false);
    }

    private void OnEnable()
    {
        EventBus.OnEliminarMejoraUnica += QuitarMejoraUnica;
    }

    private void OnDisable()
    {
        EventBus.OnEliminarMejoraUnica -= QuitarMejoraUnica;
    }

    public void MostrarMejorasRand()
    {
        refresh.Activar();
        nxt.Activar();
        nxt.txtRound.SetActive(true);
        DesactivarMarket();
        mejorasActivas = mejorasUnicas.Count + mejorasNave.Count + mejorasCrucero.Count;
        currentPool.Clear();
        currentPool.AddRange(mejorasUnicas);
        currentPool.AddRange(mejorasNave);
        currentPool.AddRange(mejorasCrucero);

        int tope = 4;
        if (mejorasActivas < 4)
            tope = 3;

        for (int i = 0; i < tope; i++)
        {
            int index = Random.Range(0, currentPool.Count);
            currentPool[index].Activar();
            currentPool[index].transform.position = puntosVenta[i].position;
            currentPool.RemoveAt(index);
        }
    }

    public void Refresh()
    {
        MostrarMejorasRand();
        refresh.Reactivar();
    }

    public void QuitarMejoraUnica(Mejora m)
    {
        mejorasUnicas.Remove(m);
    }

    public void QuitarMejoraNave(int idx)
    {
        mejorasNave.RemoveAt(idx);
    }

    public void QuitarMejoraCrucero(int idx)
    {
        mejorasCrucero.RemoveAt(idx);
    }

    public void DesactivarMarket()
    {
        foreach (Mejora mu in mejorasUnicas)
            mu.Desactivar();
        foreach (Mejora mn in mejorasNave)
            mn.Desactivar();
        foreach (Mejora mc in mejorasCrucero)
            mc.Desactivar();
    }
    public void FinalizarCompras()
    {
        DesactivarMarket();
        refresh.Desactivar();
        nxt.Desactivar();
        nxt.txtRound.SetActive(false);
        rm.StartRound();
    }
}
