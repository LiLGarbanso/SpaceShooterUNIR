using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("SFX")]
    public AudioClip SFX_disparo;
    public AudioClip SFX_recibirDMG, SFX_muerte;

    [Header("Stats")]
    public int vida;
    public int dmg, score;
    public float attackSpeed, startFireDelay, movementSpeed, deploySpeed;
}
