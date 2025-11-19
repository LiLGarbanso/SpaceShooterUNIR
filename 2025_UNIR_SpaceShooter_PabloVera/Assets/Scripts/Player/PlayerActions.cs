using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public BulletPool poolProyectiles;
    public Transform escenario, cannon;
    public PlayerData playerData;
    private bool canShoot;
    private float currentShootCadency;

    private void Start()
    {
        canShoot = true;
        currentShootCadency = 0;
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
        if (context.started && canShoot)
        {
            canShoot = false;
            currentShootCadency = 0;
            Bullet bull = poolProyectiles.SacarDeLaPool();
            bull.gameObject.transform.SetParent(escenario);
            bull.Init(cannon.up, cannon);
            bull.gameObject.SetActive(true);
            SoundMannager.Instance.PlaySFX(playerData.SFX_disparo);
        }
    }
}
