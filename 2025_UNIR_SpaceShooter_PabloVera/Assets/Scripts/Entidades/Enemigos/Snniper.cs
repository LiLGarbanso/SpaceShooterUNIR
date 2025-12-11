using System.Collections;
using UnityEngine;

public class Snniper : Enemigo
{
    public Transform player;
    public LineRenderer lineRenderer;
    private Vector3 movDir;
    [SerializeField]private float aimTime;

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
        StartCoroutine(SnniperShoot());
    }

    IEnumerator SnniperShoot()
    {
        canShoot = false;   //Desabilitar movimiento
        lineRenderer.enabled = true;
        //transform.LookAt(player.position);
        Vector2 playerDir = player.position - transform.position;
        //Vector2.Angle(playerDir, -transform.up);
        transform.localRotation = Quaternion.Euler(0f, 0f, -Vector2.SignedAngle(playerDir, -transform.up));
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, player.position);
        
        yield return new WaitForSeconds(aimTime);
        lineRenderer.enabled = false;
        Bullet bull = bulletPool.SacarDeLaPool();
        bull.Init(playerDir, cannon, escenario, enemyData.dmg);
        SoundMannager.Instance.PlaySFX(enemyData.SFX_disparo);
        transform.localRotation = Quaternion.identity;
        canShoot = true;
        yield return null;
    }

    public override IEnumerator Die()
    {
        sprRend.enabled = false;
        canShoot = false;
        col.enabled = false;
        lineRenderer.enabled = false;
        SoundMannager.Instance.PlaySFX(enemyData.SFX_muerte);
        EventBus.EnemigoMuerto(enemyData.score);
        particleSys.Play();
        yield return new WaitForSeconds(2f);
        pool.MeterEnLaPool(this);
        gameObject.SetActive(false);
        yield return null;
    }
}
