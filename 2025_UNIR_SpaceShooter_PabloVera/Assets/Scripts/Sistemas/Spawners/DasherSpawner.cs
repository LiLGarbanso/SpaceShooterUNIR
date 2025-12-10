using UnityEngine;

public class DasherSpawner : EnemySpawner
{
    public Collider2D dasherZone;
    public override void AdditionalConfig(Enemigo e)
    {
        Dasher dasher = (Dasher)e;
        dasher.SetDashZone(dasherZone);
        dasher.SetGridEscenario(grid);
    }
}
