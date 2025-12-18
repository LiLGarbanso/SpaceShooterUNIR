using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Mejora : MonoBehaviour
{
    public int coste;
    public int usos;
    private int currentUsos;
    public GameManager gm;
    public AudioClip sfxMejora, sfxNoMoney;
    public string mensajeRequisitos;
    public TextMeshPro txtPrecio;
    public ParticleSystem pSys;
    private bool active;
    public SpriteRenderer spRend;
    public List<GameObject> imgs;
    public bool hasIcon;
    public Collider2D col;

    private void Start()
    {
        txtPrecio.text = "" + coste;
        active = true;
        currentUsos = 0;
    }

    public void Activar()
    {
        col.enabled = true;
        spRend.enabled = true;
        txtPrecio.enabled = true;
        active = true;
    }

    public void TrySeleccionarMejora()
    {
        if (active && gm.GetPuntosJugador() >= coste)
        {
            if (RequisitosMejora() && currentUsos < usos)
            {
                gm.RestarPuntos(coste);
                SoundMannager.Instance.PlaySFX(sfxMejora);
                AplicarMejora();
                currentUsos++;
                active = false;
                if(hasIcon)
                    imgs[currentUsos-1].SetActive(true);
                StartCoroutine(AnimCompra());
            }
            else
            {
                gm.MostrarMensaje(mensajeRequisitos);
            }
        }
        else
        {
            gm.RestarPuntos(0);
            gm.MostrarMensaje("Dinero Insuficiente");
            SoundMannager.Instance.PlaySFX(sfxNoMoney);
        }
    }

    IEnumerator AnimCompra()
    {
        Desactivar();
        pSys.Play();
        yield return new WaitForSeconds(2f);
    }

    public void Desactivar()
    {
        col.enabled = false;
        spRend.enabled = false;
        txtPrecio.enabled = false;
        active = false;
    }

    public abstract bool RequisitosMejora();
    public abstract void AplicarMejora();
}
