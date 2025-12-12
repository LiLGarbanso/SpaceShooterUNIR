using UnityEngine;

public class EnemyBullet : Bullet
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.TryGetComponent<Player>(out var plyr))
            {
                plyr.TakeDMG(dmg);
                Desactivar();
            }
            else if(collision.gameObject.TryGetComponent<CruceroEstelar>(out var crucero))
            {
                crucero.TakeDMG(dmg);
                Desactivar();
            }
            else if (collision.gameObject.TryGetComponent<Barrera>(out var barrera))
            {
                barrera.TakeDMG(dmg);
                Desactivar();
            }
            else if (collision.gameObject.CompareTag("tope"))
            {
                Desactivar();
            }
        }
    }
}
