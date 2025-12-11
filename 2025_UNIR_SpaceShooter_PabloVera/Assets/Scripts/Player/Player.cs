using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;
    private int currentHP;
    private bool canTakeDmg;
    public GameObject shieldGO;
    public RectTransform uiHpPlayer;
    private float totalWidth;

    private void Start()
    {
        currentHP = playerData.vida;
        canTakeDmg = true;
        totalWidth = uiHpPlayer.rect.width;
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
            uiHpPlayer.sizeDelta = new Vector2(Mathf.Lerp(0, totalWidth, (float)currentHP / playerData.vida), uiHpPlayer.sizeDelta.y);
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
        EventBus.GameOver();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.TryGetComponent<Mejora>(out var mejora))
            {
                mejora.TrySeleccionarMejora();
            }
        }
    }
}
