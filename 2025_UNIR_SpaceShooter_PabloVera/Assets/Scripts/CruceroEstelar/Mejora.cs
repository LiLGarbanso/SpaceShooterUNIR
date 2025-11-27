using UnityEngine;
public abstract class Mejora : MonoBehaviour
{
    public int coste;
    public GameManager gm;
    public AudioClip sfxMejora;
    public void TrySeleccionarMejora()
    {
        if(gm.GetPuntosJugador() >= coste)
        { 
            gm.RestarPuntos(coste);
            AplicarMejora();
        }
    }

    public abstract void AplicarMejora();
}
