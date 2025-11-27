using UnityEngine;

public class RepararCrucero : Mejora
{
    public CruceroEstelar cruceroEstelar;
    public int saludRecuperada;
    public override void AplicarMejora()
    {
        SoundMannager.Instance.PlaySFX(sfxMejora);
        cruceroEstelar.RecuperarSalud(saludRecuperada);
    }
}
