using Unity.VisualScripting;
using UnityEngine;

public class Dummy : Enemigo
{
    private Vector3 movDir;
    private bool movingLeft;

    private void Start()
    {
        if (Random.Range(0, 2) > 0)
        { 
            movDir = Vector2.right;
            movingLeft = false;
        }
        else
        {
            movDir = Vector2.left;
            movingLeft = true;
        }
    }

    public override void Movement()
    {
        RaycastHit2D rayRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, escenarioMask);
        RaycastHit2D rayLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, escenarioMask);

        if (rayRight.collider != null)
        {
            movingLeft = true;
        }
        if(rayLeft.collider != null)
        {
            movingLeft = false;
        }

        if (movingLeft)
            movDir = Vector2.left;
        else
            movDir = Vector2.right;

        transform.position += enemyData.movementSpeed * Time.deltaTime * movDir;
    }

    public override void Shoot()
    {
        Bullet bull = bulletPool.SacarDeLaPool();
        bull.Init(cannon.up, cannon, escenario, enemyData.dmg, enemyData.bulletSpeed);
        SoundMannager.Instance.PlaySFX(enemyData.SFX_disparo);
    }
}
