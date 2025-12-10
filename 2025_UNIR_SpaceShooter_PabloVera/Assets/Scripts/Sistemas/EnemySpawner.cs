using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Collider2D> spawnZones;
    public Transform escenario, player;
    public EnemyPool enemyPool;
    public Grid grid;

    public bool SpawnEnemy()
    {
        Enemigo enemy = enemyPool.SacarDeLaPool();
        if (enemy == null) return false;

        try
        {
            Collider2D spawnZone = spawnZones[enemy.enemyData.spawnZone];
            enemy.gameObject.SetActive(true);
            enemy.escenario = escenario;
            enemy.player = player;
            enemy.transform.SetParent(escenario);
            Vector2 randomPos = new Vector2(Random.Range(spawnZone.bounds.min.x, spawnZones[enemy.enemyData.spawnZone].bounds.max.x), Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y));
            var cellPos = grid.WorldToCell(randomPos);
            Vector2 sp = new Vector2(grid.CellToWorld(cellPos).x + grid.cellSize.x / 2, grid.CellToWorld(cellPos).y + grid.cellSize.y / 2);
            enemy.Init(sp);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
