using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : Entidad
{
    public EnemyData enemyData;
    public BulletPool bulletPool;
    public List<Vector2> targetsPosMovement;
    private float umbralDistancia = 0.1f, currentShootTime, delayShoot = 2f;
    private int currentTarget;
    private Coroutine moveCoroutine;
    public Transform escenario;

    public override void Start()
    {
        currentVida = enemyData.vida;
        currentTarget = 0;
        currentShootTime = enemyData.attackSpeed + Random.Range(-delayShoot, delayShoot);
        moveCoroutine = StartCoroutine(Mover());
    }
    private void Update()
    {
        currentShootTime -= Time.deltaTime;

        if (currentShootTime < 0)
        {
            Shoot();
            currentShootTime = enemyData.attackSpeed;
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

    public void MoveToTargetPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetsPosMovement[currentTarget],enemyData.movementSpeed*Time.deltaTime);
    }

    IEnumerator Mover()
    {
        while (Vector2.Distance(transform.position, targetsPosMovement[currentTarget]) > umbralDistancia)
        {
            yield return new WaitForEndOfFrame();
            MoveToTargetPosition();
        }
        MoveToNextTarget();
        yield return null;
    }
}
