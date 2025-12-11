using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CruceroEstelar : MonoBehaviour
{
    private int currentHP, currentNaves;
    private float currentLaserCooldown;
    public CruceroData cruceroData;
    public RectTransform uiHpCrucero;
    public Image hpBar;
    private float totalHeight;
    public List<GameObject> navesIMGs;
    public List<Barrera> barreras;

    public int GetCurrentHP() {  return currentHP; }
    public int GetCurrentNaves() { return currentNaves; }
    private void Start()
    {
        currentLaserCooldown = cruceroData.laserCooldown;
        currentHP = cruceroData.HP;
        totalHeight = uiHpCrucero.rect.height;
        currentNaves = cruceroData.navesIniciales;
        UpdateNavesUI();
        UpdateUiCrucero();
        foreach (Barrera b in barreras)
            b.gameObject.SetActive(false);
    }

    //----------MEJORAS-----------------------------------------------//
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

    public void ConstruirNave()
    {
        if (currentNaves < cruceroData.navesIniciales)
            currentNaves++;

        UpdateNavesUI();
    }

    public void ActivarBarreras()
    {
        foreach (Barrera b in barreras)
        {
            b.gameObject.SetActive(true);
            b.Init();
        }
    }

    //---------------------------------------------------------------//

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

    public void UpdateNavesUI()
    {
        //SFX construir nave
        foreach(GameObject go in navesIMGs)
            go.SetActive(false);

        for (int i = 0; i < currentNaves; i++)
            navesIMGs[i].SetActive(true);
    }

    private void Die()
    {
        //Anim destrucción crucero
        EventBus.GameOver();
    }
}
