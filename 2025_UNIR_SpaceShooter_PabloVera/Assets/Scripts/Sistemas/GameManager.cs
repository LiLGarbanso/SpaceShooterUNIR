using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numRondas;
    private int currentRound, currentPuntos;
    public Text puntosTxt;
    public GameObject uiDerrota;
    private Coroutine crtDerrota;
    public RoundMannager roundMannager;

    private void OnEnable()
    {
        EventBus.OnRondaFinalizada += RondaFinalizada;
        EventBus.OnGameOver += Derrota;
        EventBus.OnEnemigoMuerto += SumarPuntos;
    }

    void Start()
    {
        crtDerrota = null;
        uiDerrota.SetActive(false);
        EventBus.EmpezarPartida();
        currentRound = 0;
        //SiguienteRonda();
        roundMannager.StartRound();
    }

    public void SumarPuntos(int puntos)
    {
        currentPuntos += puntos;
        UpdateUiPuntos();
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
        EventBus.NextRound(currentRound);
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

    IEnumerator DerrotaAnim()
    {
        uiDerrota.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MenuInicial");
        crtDerrota = null;
    }

    private void OnDisable()
    {
        EventBus.OnRondaFinalizada -= RondaFinalizada;
        EventBus.OnGameOver -= Derrota;
        EventBus.OnEnemigoMuerto -= SumarPuntos;
    }
}
