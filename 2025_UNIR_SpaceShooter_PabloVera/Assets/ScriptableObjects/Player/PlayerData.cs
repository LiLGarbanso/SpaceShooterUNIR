using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public AudioClip SFX_disparo, SFX_recibirDMG, SFX_muerte;
    public int vida, dmg;
    public float moveSpeed, shootCadency;
}
