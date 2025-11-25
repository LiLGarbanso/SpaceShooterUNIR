using UnityEngine;

[CreateAssetMenu(fileName = "CruceroData", menuName = "Scriptable Objects/CruceroData")]
public class CruceroData : ScriptableObject
{
    public AudioClip SFX_recibirDMG, SFX_DispararCannonLaser, SFX_RecuperarHP;
    public int HP, navesIniciales;
    public float laserCooldown;
}
