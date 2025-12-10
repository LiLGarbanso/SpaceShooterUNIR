using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Dasher : Enemigo
{
    private Collider2D dashZone;
    private Grid gridEscenario;
    public float waitTime, fireDelay;
    public LineRenderer lineRenderer;
    public int dashes;
    private int currentDashes;

    private void Start()
    {
        lineRenderer.enabled = false;
        currentDashes = Random.Range(1, currentDashes + 1);
    }

    public void SetDashZone(Collider2D dZ) { dashZone = dZ; }
    public void SetGridEscenario(Grid g) { gridEscenario = g; }
    public override void Movement()
    {
    }

    public override void Shoot()
    {
        StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
        canShoot = false;
        Vector2 pos = CalcularNuevaPosicion();
        Vector2 initPos = transform.position;
        lineRenderer.enabled = true;
        while (currentDashes > 0)
        {
            lineRenderer.enabled = true;
            while (Vector2.Distance(transform.position, pos) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, pos, enemyData.movementSpeed * Time.deltaTime);
                lineRenderer.SetPosition(0, initPos);
                lineRenderer.SetPosition(1, transform.position);
                yield return new WaitForEndOfFrame();
            }
            pos = CalcularNuevaPosicion();
            initPos = transform.position;
            lineRenderer.enabled = false;
            currentDashes--;
            yield return new WaitForSeconds(0.5f);
        }
        
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(waitTime);
        Bullet b1 = bulletPool.SacarDeLaPool();
        b1.Init(Vector2.down, cannon, escenario, enemyData.dmg);
        SoundMannager.Instance.PlaySFX(enemyData.SFX_disparo);
        yield return new WaitForSeconds(fireDelay);
        Bullet b2 = bulletPool.SacarDeLaPool();
        b2.Init(Vector2.down, cannon, escenario, enemyData.dmg);
        SoundMannager.Instance.PlaySFX(enemyData.SFX_disparo);
        canShoot = true;
        currentDashes = dashes;
        yield return null;
    }

    public Vector2 CalcularNuevaPosicion()
    {
        Vector2 randomPos = new Vector2(Random.Range(dashZone.bounds.min.x, dashZone.bounds.max.x), Random.Range(dashZone.bounds.min.y, dashZone.bounds.max.y));
        var cellPos = gridEscenario.WorldToCell(randomPos);
        return new Vector2(gridEscenario.CellToWorld(cellPos).x + gridEscenario.cellSize.x / 2, gridEscenario.CellToWorld(cellPos).y + gridEscenario.cellSize.y / 2);
    }
}
