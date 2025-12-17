using UnityEngine;

public class KamikazeSpawner : EnemySpawner
{
    public Transform cruceroTransform;
    public override void AdditionalConfig(Enemigo e)
    {
        Kamikaze k = (Kamikaze)e;
        k.SetCruceroCollider(cruceroTransform);
    }

}
