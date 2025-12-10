using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public BulletPool poolProyectiles;
    public Transform escenario, cannon;
    public PlayerData playerData;
    private Player playerScript;
    private bool canShoot, activeShield;
    private float currentShootCadency, currentShieldTime;
    private Coroutine coroutineEscudo;

    private void Start()
    {
        playerScript = gameObject.GetComponent<Player>();
        canShoot = true;
        currentShootCadency = 0;
        activeShield = false;
        currentShieldTime = playerData.shieldTime;
    }

    private void Update()
    {
        currentShootCadency += Time.deltaTime;
        if (currentShootCadency > playerData.shootCadency)
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
            Bullet bull = poolProyectiles.SacarDeLaPool();
            bull.gameObject.transform.SetParent(escenario);
            bull.Init(cannon.up, cannon, escenario, playerData.dmg);
            bull.gameObject.SetActive(true);
            SoundMannager.Instance.PlaySFX(playerData.SFX_disparo);
        }
    }

    public void Shield(InputAction.CallbackContext context)
    {
        if (context.started)
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
