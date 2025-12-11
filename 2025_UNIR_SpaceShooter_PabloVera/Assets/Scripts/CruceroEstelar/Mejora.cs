using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Mejora : MonoBehaviour
{
    public int coste;
    public GameManager gm;
    public AudioClip sfxMejora;
    public string mensajeRequisitos;
    public TextMeshPro txtPrecio;

    private void Start()
    {
        txtPrecio.text = "" + coste;
    }

    public void TrySeleccionarMejora()
    {
        if (gm.GetPuntosJugador() >= coste)
        {
            if (RequisitosMejora())
            {
                gm.RestarPuntos(coste);
                SoundMannager.Instance.PlaySFX(sfxMejora);
                AplicarMejora();
            }
            else
            {
                gm.MostrarMensaje(mensajeRequisitos);
            }
        }
        else
        {
            //SFX no se puede comprar
        }
    }

    public abstract bool RequisitosMejora();
    public abstract void AplicarMejora();
}
