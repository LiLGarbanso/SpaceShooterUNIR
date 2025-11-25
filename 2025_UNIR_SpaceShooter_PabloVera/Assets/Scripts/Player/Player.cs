using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;
    private int currentHP;
    private bool canTakeDmg;
    public GameObject shieldGO;

    private void Start()
    {
        currentHP = playerData.vida;
        canTakeDmg = true;
    }

    public void SetInvulnerability(bool invulnerability)
    {
        canTakeDmg = !invulnerability;
        shieldGO.SetActive(invulnerability);
    }

    public void TakeDMG(int dmg)
    {
        if (canTakeDmg)
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
        else
        {
            SoundMannager.Instance.PlaySFX(playerData.SFX_DetenerProyectil);
        }
    }

    public void Die()
    {
        SoundMannager.Instance.PlaySFX(playerData.SFX_muerte);
        gameObject.SetActive(false);
    }
}
