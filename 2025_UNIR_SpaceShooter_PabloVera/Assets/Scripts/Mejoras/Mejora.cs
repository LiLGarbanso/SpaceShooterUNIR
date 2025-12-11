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
    public ParticleSystem pSys;
    private bool active;
    public SpriteRenderer spRend;

    private void Start()
    {
        txtPrecio.text = "" + coste;
        active = true;
    }

    public void TrySeleccionarMejora()
    {
        if (active && gm.GetPuntosJugador() >= coste)
        {
            if (RequisitosMejora())
            {
                gm.RestarPuntos(coste);
                SoundMannager.Instance.PlaySFX(sfxMejora);
                AplicarMejora();
                active = false;
                StartCoroutine(AnimCompra());
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

    IEnumerator AnimCompra()
    {
        spRend.enabled = false;
        pSys.Play();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        spRend.enabled = true;
        active = true;
    }

    public abstract bool RequisitosMejora();
    public abstract void AplicarMejora();
}
