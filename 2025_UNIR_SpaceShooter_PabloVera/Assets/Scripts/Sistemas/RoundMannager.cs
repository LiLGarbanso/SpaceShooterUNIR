using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundMannager : MonoBehaviour
{
    public List<RoundData> rondas;
    private int currentRonda, currentEnemies, potentialCurrentEnemies;
    public EnemySpawner dummySpawner, snipperSpawner, dasherSpawner, kamikazeSpawner;
    public int maxRounds;
    public List<GameObject> roundIMGs;
    public PoolMejoras upgradeMarket;
    public GameObject player;
    public Transform start;
    public AudioClip musicaBatalla, musicaTienda;

    private void OnEnable()
    {
        EventBus.OnEnemigoMuerto += EnemigoDerrotado;
    }

    private void Start()
    {
        currentRonda = -1;
        currentEnemies = 0;
        potentialCurrentEnemies = 0;
        foreach(GameObject go in roundIMGs)
            go.SetActive(false);
        upgradeMarket.DesactivarMarket();
    }

    public void StartRound()
    {
        //SoundMannager.Instance.PlayMusic(musicaBatalla);
        currentRonda++;
        if (currentRonda >= maxRounds)
        {
            RondaFinal();
        }
        else
        {
            currentEnemies = 0;
            potentialCurrentEnemies = CalcularTotalEnemigos();
            roundIMGs[currentRonda].SetActive(true);
            StartCoroutine(SpawnWave());
        }
    }

    /**
     * Tener en cuenta dos tipos de delay:
     *      Delay entre enemigos spawneados (Ratio)
     *      Delay entre tipos de enemigo (Spawn Delay)
     */
    IEnumerator SpawnWave()
    {
        //El delay de los dummies es irrelevante porque siempre son los primeros
        for (int i = 0; i < rondas[currentRonda].numDummies; i++)
        {
            if (dummySpawner.SpawnEnemy())
                currentEnemies++;
            else
                potentialCurrentEnemies--;
            yield return new WaitForSeconds(rondas[currentRonda].dummyRatio);
        }

        yield return new WaitForSeconds(rondas[currentRonda].dasherSpawnDelay);

        for (int j = 0; j < rondas[currentRonda].numDashers; j++)
        {
            if (dasherSpawner.SpawnEnemy())
                currentEnemies++;
            else
                potentialCurrentEnemies--;
            yield return new WaitForSeconds(rondas[currentRonda].dasherRatio);
        }

        yield return new WaitForSeconds(rondas[currentRonda].snipperSpawnDelay);

        for (int k = 0; k < rondas[currentRonda].numSnipper; k++)
        {
            if (snipperSpawner.SpawnEnemy())
                currentEnemies++;
            else
                potentialCurrentEnemies--;
            yield return new WaitForSeconds(rondas[currentRonda].snipperRatio);
        }

        yield return new WaitForSeconds(rondas[currentRonda].kamikazeSpawnDelay);

        for (int z = 0; z < rondas[currentRonda].numKamimazes; z++)
        {
            if (kamikazeSpawner.SpawnEnemy())
                currentEnemies++;
            else
                potentialCurrentEnemies--;
            yield return new WaitForSeconds(rondas[currentRonda].kamikazeRatio);
        }
    }

    public void EnemigoDerrotado(int score)
    {
        currentEnemies--;
        potentialCurrentEnemies--;

        if(potentialCurrentEnemies  <= 0 && currentEnemies <= 0)
        {
            upgradeMarket.MostrarMejorasRand();
            player.gameObject.transform.position = start.position;
            SoundMannager.Instance.PararSonido();
            SoundMannager.Instance.PlayMusic(musicaTienda);
        }
    }

    private int CalcularTotalEnemigos()
    {
        return rondas[currentRonda].numDummies + rondas[currentRonda].numDashers + rondas[currentRonda].numSnipper + rondas[currentRonda].numKamimazes;
    }

    private void RondaFinal()
    {
        SceneManager.LoadScene("Victoria");
    }

    private void OnDisable()
    {
        EventBus.OnEnemigoMuerto -= EnemigoDerrotado;
    }
}
