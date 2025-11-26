using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : Entidad
{
    public EnemyData enemyData;
    public BulletPool bulletPool;
    public List<Vector2> targetsPosMovement;
    public Vector2 spawnPos;
    private float umbralDistancia = 0.1f, currentShootTime, delayShoot = 2f;
    private int currentTarget;
    private Coroutine moveCoroutine;
    public Transform escenario;
    private bool canShoot;

    public override void Start()
    {
        currentVida = enemyData.vida;
        currentTarget = 0;
        currentShootTime = enemyData.attackSpeed + Random.Range(-delayShoot, delayShoot);
        canShoot = false;
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

    public override void Die()
    {
        Debug.Log("HE MUELTO");
        SoundMannager.Instance.PlaySFX(enemyData.SFX_muerte);
        gameObject.SetActive(false);
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
