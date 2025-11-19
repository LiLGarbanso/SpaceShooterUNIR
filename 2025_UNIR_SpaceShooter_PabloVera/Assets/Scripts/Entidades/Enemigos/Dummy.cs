using UnityEngine;

public class Dummy : Enemigo
{
    public Transform cannon;
    public override void Shoot()
    {
        Bullet bull = bulletPool.SacarDeLaPool();
        bull.gameObject.transform.SetParent(escenario);
        bull.Init(cannon.up, cannon);
        bull.gameObject.SetActive(true);
        SoundMannager.Instance.PlaySFX(enemyData.SFX_disparo);
    }
}
