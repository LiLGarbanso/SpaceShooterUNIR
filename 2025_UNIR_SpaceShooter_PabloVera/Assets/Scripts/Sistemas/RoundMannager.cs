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
    public PlayerMovement player;
    public Transform start;
    public AudioClip musicaTienda, finRonda;
    public List<AudioClip> temazos;
    public string escenaVictoria;

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
        
        currentRonda++;
        
        if (currentRonda >= maxRounds)
        {
            RondaFinal();
        }
        else
        {
            SoundMannager.Instance.PlayMusic(temazos[currentRonda]);
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
            StartCoroutine(FinRonda());
        }
    }

    IEnumerator FinRonda()
    {
        player.SetMovement(false);
        SoundMannager.Instance.PararSonido();
        SoundMannager.Instance.PlaySFX(finRonda);
        EventBus.RondaFinalizada();
        while (Vector2.Distance(start.position, player.gameObject.transform.position) > 0.1)
        {
            player.gameObject.transform.position = Vector2.MoveTowards(player.gameObject.transform.position, start.position, 0.1f);
            yield return null;
        }
        player.SetMovement(true);
        upgradeMarket.MostrarMejorasRand();
        SoundMannager.Instance.PlayMusic(musicaTienda);
        yield return null;
    }

    private int CalcularTotalEnemigos()
    {
        return rondas[currentRonda].numDummies + rondas[currentRonda].numDashers + rondas[currentRonda].numSnipper + rondas[currentRonda].numKamimazes;
    }

    private void RondaFinal()
    {
        SceneManager.LoadScene(escenaVictoria);
    }

    private void OnDisable()
    {
        EventBus.OnEnemigoMuerto -= EnemigoDerrotado;
    }
}
