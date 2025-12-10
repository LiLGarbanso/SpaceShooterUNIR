using UnityEngine;

public class SnipperSpawner : EnemySpawner
{
    public Transform player;
    public override void AdditionalConfig(Enemigo e)
    {
        Snniper snipper = (Snniper)e;
        snipper.player = player;
    }
}
