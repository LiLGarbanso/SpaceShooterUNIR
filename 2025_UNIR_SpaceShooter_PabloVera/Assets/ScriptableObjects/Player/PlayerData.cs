using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("SFX")]
    public AudioClip SFX_disparo;
    public AudioClip SFX_recibirDMG, SFX_muerte, SFX_escudo, SFX_DetenerProyectil, SFX_CooldownEscudo;

    [Header("Stats")]
    public int vida;
    public int dmg;
    public float maxSpeed, acceleration, speedDrag, shootCadency, baseBulletSpeed, shieldTime, shieldCooldown;
}
