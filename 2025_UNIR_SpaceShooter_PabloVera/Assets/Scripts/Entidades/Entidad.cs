using JetBrains.Annotations;
using UnityEngine;

/*
 *  Representa cualquier cosa del juego que pueda recibir daño
 */
public abstract class Entidad : MonoBehaviour
{
    private int currentVida;
    public EntityData entityData;

    private void Start()
    {
        currentVida = entityData.vida;
    }

    public void TakeDMG(int dmg)
    {
        currentVida -= dmg;
        if (currentVida <= 0)
        {
            Die();
        }
        else
        {
            SoundMannager.Instance.PlaySFX(entityData.recibirDMG);
        }
    }

    public abstract void Die();
}
