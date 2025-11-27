using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int vida, dmg, movementPoints, score;
    public float attackSpeed, movementSpeed;
    public AudioClip SFX_disparo, SFX_recibirDMG, SFX_muerte;
}
