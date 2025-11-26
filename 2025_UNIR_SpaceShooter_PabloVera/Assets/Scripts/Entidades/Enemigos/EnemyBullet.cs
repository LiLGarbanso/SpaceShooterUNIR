using UnityEngine;

public class EnemyBullet : Bullet
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.TryGetComponent<Player>(out var target))
            {
                target.TakeDMG(dmg);
                Desactivar();
            }
            else if(collision.gameObject.TryGetComponent<CruceroEstelar>(out var target2))
            {
                target2.TakeDMG(dmg);
                Desactivar();
            }
        }
    }
}
