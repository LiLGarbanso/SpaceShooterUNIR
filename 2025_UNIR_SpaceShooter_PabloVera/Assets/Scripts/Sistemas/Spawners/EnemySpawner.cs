using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner : MonoBehaviour
{
    public Collider2D spawnZone;
    public Transform escenario;
    public EnemyPool enemyPool;
    public Grid grid;

    public bool SpawnEnemy()
    {
        Enemigo enemy = enemyPool.SacarDeLaPool();
        if (enemy == null) return false;

        try
        {
            enemy.gameObject.transform.position = transform.position;
            enemy.gameObject.SetActive(true);
            enemy.escenario = escenario;
            enemy.transform.SetParent(escenario);
            Vector2 randomPos = new Vector2(Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x), Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y));
            var cellPos = grid.WorldToCell(randomPos);
            Vector2 sp = new Vector2(grid.CellToWorld(cellPos).x + grid.cellSize.x / 2, grid.CellToWorld(cellPos).y + grid.cellSize.y / 2);
            AdditionalConfig(enemy);
            enemy.Init(sp);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public abstract void AdditionalConfig(Enemigo e);
}
