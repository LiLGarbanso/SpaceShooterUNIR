using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public BulletPool poolProyectiles;
    public Transform escenario, cannon, cannon2, cannon3;
    public PlayerData playerData;
    private Player playerScript;
    private bool canShoot, activeShield, canShield, canDobleShoot, canTripleShoot;
    private float currentShootCadency, currentShieldTime, atckSpeed;
    private Coroutine coroutineEscudo;
    private int extraDMG, timesDmgUpgrade, timesAtkSpeedUpgrade, timesBulletSpeedUpgrade;

    private void Start()
    {
        playerScript = gameObject.GetComponent<Player>();
        canShoot = true;
        currentShootCadency = 0;
        extraDMG = 0;
        activeShield = false;
        currentShieldTime = playerData.shieldTime;
        atckSpeed = playerData.shootCadency;
        canShield = false;
        canDobleShoot = false;
        canTripleShoot = false;
    }

    //-----------MEJORAS-------------------------------//

    public void IncreaseDMG()
    {
        timesDmgUpgrade++;
        extraDMG++;
    }
    public int GetTimesIncreasedDMG() { return timesDmgUpgrade; }

    public void IncreaseAtckSpeed(float descrement)
    {
        atckSpeed = Mathf.Clamp(atckSpeed -= descrement, 0.2f, 2);
        timesAtkSpeedUpgrade++;
    }
    public int GetTimesIncreasedAtkSpeed() { return timesAtkSpeedUpgrade; }

    public void ShieldUpgrade() { canShield = true; }
    public bool HasShieldUpgrade() { return canShield; }

    public void IncreaseBulletSpeed( float increment)
    {
        timesBulletSpeedUpgrade++;
        poolProyectiles.UpgradeBulletSpeed(increment);
    }
    public int GetTimesIncreasedBulletSpeed() { return timesBulletSpeedUpgrade; }

    public void MultiShootUpgrade()
    {
        if(!canDobleShoot)
            canDobleShoot=true;
        else if(!canTripleShoot)
            canTripleShoot=true;
    }
    public bool HasMultiShootUpgrade() { return canTripleShoot; }

    //------------------------------------------------//

    private void Update()
    {
        currentShootCadency += Time.deltaTime;
        if (currentShootCadency > atckSpeed)
        {
            canShoot = true;
        }
    }
    public void Disparar(InputAction.CallbackContext context)
    {
        if (context.performed && canShoot)
        {
            canShoot = false;
            currentShootCadency = 0;
            if (canTripleShoot)
            {
                Bullet b1 = poolProyectiles.SacarDeLaPool();
                Bullet b2 = poolProyectiles.SacarDeLaPool();
                Bullet b3 = poolProyectiles.SacarDeLaPool();
                b1.gameObject.transform.SetParent(escenario);
                b2.gameObject.transform.SetParent(escenario);
                b3.gameObject.transform.SetParent(escenario);
                b1.Init(cannon2.up, cannon, escenario, playerData.dmg + extraDMG);
                b2.Init(cannon3.up, cannon2, escenario, playerData.dmg + extraDMG);
                b3.Init(cannon3.up, cannon3, escenario, playerData.dmg + extraDMG);
                b1.gameObject.SetActive(true);
                b2.gameObject.SetActive(true);
                b3.gameObject.SetActive(true);
            }
            else if(canDobleShoot)
            {
                Bullet b1 = poolProyectiles.SacarDeLaPool();
                Bullet b2 = poolProyectiles.SacarDeLaPool();
                b1.gameObject.transform.SetParent(escenario);
                b2.gameObject.transform.SetParent(escenario);
                b1.Init(cannon2.up, cannon2, escenario, playerData.dmg + extraDMG);
                b2.Init(cannon3.up, cannon3, escenario, playerData.dmg + extraDMG);
                b1.gameObject.SetActive(true);
                b2.gameObject.SetActive(true);
            }
            else
            {
                Bullet bull = poolProyectiles.SacarDeLaPool();
                bull.gameObject.transform.SetParent(escenario);
                bull.Init(cannon.up, cannon, escenario, playerData.dmg + extraDMG);
                bull.gameObject.SetActive(true);
            }
            SoundMannager.Instance.PlaySFX(playerData.SFX_disparo);
        }
    }

    public void Shield(InputAction.CallbackContext context)
    {
        if (context.started && canShield)
        {
            if(!activeShield)
            {
                coroutineEscudo = StartCoroutine(ActivarEscudo());
            }
        }
    }

    IEnumerator ActivarEscudo()
    {
        activeShield = true;
        SoundMannager.Instance.PlaySFX(playerData.SFX_escudo);
        playerScript.SetInvulnerability(true);
        yield return new WaitForSeconds(playerData.shieldTime);
        playerScript.SetInvulnerability(false);
        yield return new WaitForSeconds(playerData.shieldCooldown);
        activeShield = false;
        yield return null;
    }
}
