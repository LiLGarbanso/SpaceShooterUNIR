using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : EntityData
{
    public int dmg;
    public float attackSpeed, movementSpeed;
    public AudioClip SFX_disparo;
}
