using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemigo : MonoBehaviour
{
    public EnemyData enemyData;
    public BulletPool bulletPool;
    //public List<Vector2> targetsPosMovement;
    //public Vector2 spawnPos;
    //private float umbralDistancia = 0.1f, currentShootTime, delayShoot = 2f;
    //private int currentTarget;
    private float currentShootTime;
    private Coroutine moveCoroutine, dieCoroutine;
    public Transform escenario, player;
    protected bool canShoot;
    protected int currentVida;
    public ParticleSystem particleSys;
    public SpriteRenderer sprRend;
    public Collider2D col;
    public LayerMask escenarioMask;
    protected EnemyPool pool;

    public void Init(Vector2 spawnPoint)
    {
        currentVida = enemyData.vida;
        currentShootTime = enemyData.attackSpeed + Random.Range(-enemyData.startFireDelay, enemyData.startFireDelay);
        canShoot = false;
        dieCoroutine = null;
        sprRend.enabled = true;
        col.enabled = false;
        StartCoroutine(MoveSpawn(spawnPoint));
    }

    public void SetEnemyPool(EnemyPool ePool) { pool = ePool; }

    private void Update()
    {
        if (canShoot)
        {
            currentShootTime -= Time.deltaTime;

            if (currentShootTime < 0)
            {
                Shoot();
                currentShootTime = enemyData.attackSpeed;
            }

            Movement();
        }
    }

    public abstract void Shoot();
    public abstract void Movement();

    //public void MoveToNextTarget()
    //{
    //    moveCoroutine = null;

    //    if (currentTarget < targetsPosMovement.Count - 1)
    //        currentTarget++;
    //    else
    //        currentTarget = 0;

    //    moveCoroutine = StartCoroutine(Mover());
    //}

    public void TakeDMG(int dmg)
    {
        currentVida -= dmg;
        if (currentVida <= 0)
        {
            if(dieCoroutine == null)
            {
                StopAllCoroutines();
                dieCoroutine = StartCoroutine(Die());
            }
        }
        else
        {
            SoundMannager.Instance.PlaySFX(enemyData.SFX_recibirDMG);
        }
    }

    IEnumerator Die()
    {
        sprRend.enabled = false;
        canShoot = false;
        col.enabled = false;
        SoundMannager.Instance.PlaySFX(enemyData.SFX_muerte);
        EventBus.EnemigoMuerto(enemyData.score);
        particleSys.Play();
        yield return new WaitForSeconds(2f);
        pool.MeterEnLaPool(this);
        gameObject.SetActive(false);
        yield return null;
    }



    public void MoveToTargetPosition(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target,enemyData.movementSpeed*Time.deltaTime);
    }

    //IEnumerator Mover()
    //{
    //    while (Vector2.Distance(transform.position, targetsPosMovement[currentTarget]) > 0.1f)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        MoveToTargetPosition(targetsPosMovement[currentTarget]);
    //    }
    //    MoveToNextTarget();
    //    yield return null;
    //}

    IEnumerator MoveSpawn(Vector2 pos)
    {
        while (Vector2.Distance(transform.position, pos) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, pos, enemyData.deploySpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //MoveToNextTarget();
        canShoot = true;
        col.enabled = true;
        yield return null;
    }
}
