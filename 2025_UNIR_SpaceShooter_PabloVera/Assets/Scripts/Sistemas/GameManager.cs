using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numRondas, initPuntos;
    private int currentRound, currentPuntos;
    public TextMeshProUGUI puntosTxt, messageTxt;
    public GameObject uiDerrota, escenario;
    private Coroutine crtDerrota;
    public RoundMannager roundMannager;
    public Animator pointsAnimator, messageAnimator;
    public float ttlMessage;

    private void OnEnable()
    {
        EventBus.OnGameOver += Derrota;
        EventBus.OnEnemigoMuerto += SumarPuntos;
    }

    public void Init()
    {
        crtDerrota = null;
        uiDerrota.SetActive(false);
        EventBus.EmpezarPartida();
        currentRound = 0;
        //SiguienteRonda();
        SumarPuntos(initPuntos);
        roundMannager.StartRound();
    }

    public void SumarPuntos(int puntos)
    {
        currentPuntos += puntos;
        UpdateUiPuntos();
        pointsAnimator.SetTrigger("sumar");
    }

    public void MostrarMensaje(string mensaje)
    {
        StartCoroutine(MostrarMensajeTmp(mensaje));
    }

    public int GetPuntosJugador()
    {
        return currentPuntos;
    }

    public void RestarPuntos(int sub)
    {
        currentPuntos -= sub;
        if(currentPuntos < 0)
            currentPuntos = 0;
        UpdateUiPuntos();
        pointsAnimator.SetTrigger("restar");
    }

    public void UpdateUiPuntos()
    {
        puntosTxt.text = currentPuntos.ToString();
    }

    public void SiguienteRonda()
    {
        currentRound++;
        //rondaTxt.text = "Ronda " + currentRound.ToString();
        if (currentRound > numRondas)
        {
            //rondaTxt.text = "NIVEL COMPLETADO";
            return;
        }
        //EventBus.NextRound(currentRound);
    }

    public void RondaFinalizada()
    {
        SiguienteRonda();
    }

    public void Derrota()
    {
        if(crtDerrota == null )
            crtDerrota = StartCoroutine(DerrotaAnim());
    }

    public AudioClip audioMuerte;
    IEnumerator DerrotaAnim()
    {
        uiDerrota.SetActive(true);
        escenario.SetActive(false);
        SoundMannager.Instance.PararSonido();
        SoundMannager.Instance.PlaySFX(audioMuerte);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MenuInicial");
        crtDerrota = null;
    }

    IEnumerator MostrarMensajeTmp(string mssg)
    {
        messageTxt.text = mssg;
        messageAnimator.SetTrigger("mensaje");
        yield return new WaitForSeconds(ttlMessage);
        messageTxt.text = "";
        yield return null;
    }

    private void OnDisable()
    {
        EventBus.OnGameOver -= Derrota;
        EventBus.OnEnemigoMuerto -= SumarPuntos;
    }
}
