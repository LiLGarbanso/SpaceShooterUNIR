using UnityEngine;

[CreateAssetMenu(fileName = "CruceroData", menuName = "Scriptable Objects/CruceroData")]
public class CruceroData : ScriptableObject
{
    public AudioClip SFX_recibirDMG, SFX_DispararCannonLaser, SFX_RecuperarHP, SFX_PEM;
    public int HP, navesIniciales;
    public float laserCooldown;
}
