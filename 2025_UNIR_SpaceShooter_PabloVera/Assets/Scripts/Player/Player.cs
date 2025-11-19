using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;
    private int currentHP;

    private void Start()
    {
        currentHP = playerData.vida;
    }

    public void TakeDMG(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            SoundMannager.Instance.PlaySFX(playerData.SFX_recibirDMG);
        }
    }

    public void Die()
    {
        SoundMannager.Instance.PlaySFX(playerData.SFX_muerte);
        gameObject.SetActive(false);
    }
}
