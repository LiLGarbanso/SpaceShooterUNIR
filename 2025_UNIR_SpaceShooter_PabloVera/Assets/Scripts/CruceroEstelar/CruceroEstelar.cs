using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CruceroEstelar : MonoBehaviour
{
    private int currentHP;
    private float currentLaserCooldown;
    public CruceroData cruceroData;

    private void Start()
    {
        currentLaserCooldown = cruceroData.laserCooldown;
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

    private void Update()
    {
        currentLaserCooldown -= Time.deltaTime;
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
            //SoundMannager.Instance.PlaySFX(playerData.SFX_recibirDMG);
        }
    }

    private void Die()
    {
        Debug.Log("GAME OVER");
    }
}
