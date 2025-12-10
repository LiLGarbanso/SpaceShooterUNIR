using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Collider2D spawnArea, z1, z2, z3;
    public Enemigo prefabE1, prefabE2, prefabE3;
    public Transform escenario, player;
    public List<int> Ronda1, Ronda2, Ronda3;
    private int currentEnemmies;
    public EnemyPool dummyPool;

    private void OnEnable()
    {
        EventBus.OnNextRound += Spawnear;
        EventBus.OnEnemigoMuerto += EnemigoEliminado;
    }

    private void Start()
    {
        currentEnemmies = 0;
    }

    public void Spawnear(int ronda)
    {
        List<int> currentRonda = new List<int>();
        switch (ronda)
        {
            case 1:
                currentRonda = Ronda1;
                break;
            case 2:
                currentRonda = Ronda2;
                break;
            case 3:
                currentRonda = Ronda3;
                break;
            default:
                currentRonda = Ronda1;
                break;
        }
        currentEnemmies = 0;
        for (int i = 0; i < currentRonda.Count; i++)
        {
            SpawnearEnemigoTipo(i + 1, currentRonda[i]);
        }
    }

    private void SpawnearEnemigoTipo(int tipo, int cantidad)
    {
        if (cantidad == 0) return;
        switch (tipo)
        {
            case 1:
                SpawnearEnemigo(prefabE1, z1, cantidad);
                break;
            case 2:
                SpawnearEnemigo(prefabE2, z1, cantidad);
                break;
            case 3:
                SpawnearEnemigo(prefabE3, z1, cantidad);
                break;
            default:
                SpawnearEnemigo(prefabE1, z1, cantidad);
                break;
        }
    }

    private void SpawnearEnemigo(Enemigo prefab, Collider2D zona, int cantidad)
    {
        for(int i = 0; i < cantidad; i++)
        {
            //Enemigo enemy = Instantiate(prefab, spawnArea.bounds.center, Quaternion.identity, escenario);
            Enemigo enemy = dummyPool.SacarDeLaPool();
            enemy.gameObject.SetActive(true);
            enemy.escenario = escenario;
            enemy.transform.SetParent(escenario);
            //enemy.player = player;
            Vector2 spawnPoint = new Vector2(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));
            //enemy.spawnPos = spawnPoint;

            //for (int j = 0; j < enemy.enemyData.movementPoints; j++)
            //{
            //    Vector2 ponit = new Vector2(Random.Range(zona.bounds.min.x, zona.bounds.max.x), Random.Range(zona.bounds.min.y, zona.bounds.max.y));
            //    //enemy.targetsPosMovement.Add(ponit);
            //}
            enemy.Init(spawnPoint);
            currentEnemmies++;
        }
    }

    public void EnemigoEliminado(int score)
    {
        currentEnemmies--;
        if(currentEnemmies <= 0)
        {
            EventBus.FinalizarRonda();
        }
    }

    private void OnDisable()
    {
        EventBus.OnNextRound -= Spawnear;
        EventBus.OnEnemigoMuerto -= EnemigoEliminado;
    }
}
