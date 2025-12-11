using UnityEngine;

public class KamikazeSpawner : EnemySpawner
{
    public Collider2D cruceroCollider;
    public override void AdditionalConfig(Enemigo e)
    {
        Kamikaze k = (Kamikaze)e;
        k.SetCruceroCollider(cruceroCollider);
    }

}
