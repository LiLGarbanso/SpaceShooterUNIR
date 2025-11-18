using UnityEngine;

public class Dummy : Entidad
{
    public AudioClip muerte;
    public override void Die()
    {
        Debug.Log("HE MUELTO");
        SoundMannager.Instance.PlaySFX(muerte);
    }
}
