using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour
{
    public EnemyData enemyData;
    public BulletPool bulletPool;
    public List<Vector2> targetsPosMovement;
    public Vector2 spawnPos;
    private float umbralDistancia = 0.1f, currentShootTime, delayShoot = 2f;
    private int currentTarget;
    private Coroutine moveCoroutine, dieCoroutine;
    public Transform escenario, player;
    private bool canShoot;
    protected int currentVida;
    public ParticleSystem particleSys;
    private SpriteRenderer sprRend;
    private Collider2D col;

    public void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        currentVida = enemyData.vida;
        currentTarget = 0;
        currentShootTime = enemyData.attackSpeed + Random.Range(-delayShoot, delayShoot);
        canShoot = false;
        dieCoroutine = null;
        StartCoroutine(MoveSpawn());
    }
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
        }
    }

    public abstract void Shoot();

    public void MoveToNextTarget()
    {
        moveCoroutine = null;

        if (currentTarget < targetsPosMovement.Count - 1)
            currentTarget++;
        else
            currentTarget = 0;

        moveCoroutine = StartCoroutine(Mover());
    }

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
        gameObject.SetActive(false);
        yield return null;
    }



    public void MoveToTargetPosition(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target,enemyData.movementSpeed*Time.deltaTime);
    }

    IEnumerator Mover()
    {
        while (Vector2.Distance(transform.position, targetsPosMovement[currentTarget]) > umbralDistancia)
        {
            yield return new WaitForEndOfFrame();
            MoveToTargetPosition(targetsPosMovement[currentTarget]);
        }
        MoveToNextTarget();
        yield return null;
    }

    IEnumerator MoveSpawn()
    {
        while (Vector2.Distance(transform.position, spawnPos) > umbralDistancia)
        {
            yield return new WaitForEndOfFrame();
            MoveToTargetPosition(spawnPos);
        }
        MoveToNextTarget();
        canShoot = true;
        yield return null;
    }
}
