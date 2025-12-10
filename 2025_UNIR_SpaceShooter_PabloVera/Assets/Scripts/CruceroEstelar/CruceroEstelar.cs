using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CruceroEstelar : MonoBehaviour
{
    private int currentHP;
    private float currentLaserCooldown;
    public CruceroData cruceroData;
    public RectTransform uiHpCrucero;
    public Image hpBar;
    private float totalHeight;

    private void Start()
    {
        currentLaserCooldown = cruceroData.laserCooldown;
        currentHP = cruceroData.HP;
        totalHeight = uiHpCrucero.rect.height;
    }

    public void LaserCannon(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentLaserCooldown < 0)
            {
                //Disparar laser
                currentLaserCooldown = cruceroData.laserCooldown;
            }
        }
    }

    public void RecuperarSalud(int salud)
    {
        currentHP += salud;
        if (currentHP > cruceroData.HP)
            currentHP = cruceroData.HP;
        UpdateUiCrucero();
    }

    private void Update()
    {
        currentLaserCooldown -= Time.deltaTime;
    }

    public void TakeDMG(int dmg)
    {
        currentHP -= dmg;
        UpdateUiCrucero();

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            SoundMannager.Instance.PlaySFX(cruceroData.SFX_recibirDMG);
        }
    }

    public void UpdateUiCrucero()
    {
        uiHpCrucero.sizeDelta = new Vector2(uiHpCrucero.sizeDelta.x, Mathf.Lerp(0, totalHeight, (float)currentHP / cruceroData.HP));

        hpBar.color = Color.purple;

        if (currentHP < (cruceroData.HP / 4) * 3)
            hpBar.color = Color.yellow;

        if (currentHP < cruceroData.HP / 2)
            hpBar.color = Color.orange;

        if (currentHP < cruceroData.HP / 4)
            hpBar.color = Color.red;
    }

    private void Die()
    {
        EventBus.GameOver();
    }
}
