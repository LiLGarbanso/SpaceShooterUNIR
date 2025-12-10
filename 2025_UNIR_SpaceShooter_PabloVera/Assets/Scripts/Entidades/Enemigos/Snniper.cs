using System.Collections;
using UnityEngine;

public class Snniper : Enemigo
{
    public Transform cannon, player;
    public LineRenderer lineRenderer;

    public override void Movement()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        StartCoroutine(SnniperShoot());
    }

    IEnumerator SnniperShoot()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, player.position);
        Vector2 playerDir = player.position - transform.position;
        yield return new WaitForSeconds(1f);
        lineRenderer.enabled = false;
        Bullet bull = bulletPool.SacarDeLaPool();
        bull.gameObject.transform.SetParent(escenario);
        bull.Init(playerDir, cannon);
        bull.gameObject.SetActive(true);
        SoundMannager.Instance.PlaySFX(enemyData.SFX_disparo);
        yield return null;
    }
}
