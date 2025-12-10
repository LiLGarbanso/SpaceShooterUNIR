using Unity.VisualScripting;
using UnityEngine;

public class Dummy : Enemigo
{
    public Transform cannon;
    private Vector3 movDir;

    private void Start()
    {
        if (Random.Range(0, 2) > 0)
            movDir = Vector2.right;
        else 
            movDir = Vector2.left;
    }

    public override void Movement()
    {
        transform.position += enemyData.movementSpeed * Time.deltaTime * movDir;
        RaycastHit2D rayRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, escenarioMask);
        RaycastHit2D rayLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, escenarioMask);

        if (rayRight.collider != null || rayLeft.collider != null)
            movDir *= Vector2.left;
    }

    public override void Shoot()
    {
        Bullet bull = bulletPool.SacarDeLaPool();
        bull.gameObject.transform.SetParent(escenario);
        bull.Init(cannon.up, cannon);
        bull.gameObject.SetActive(true);
        SoundMannager.Instance.PlaySFX(enemyData.SFX_disparo);
    }
}
