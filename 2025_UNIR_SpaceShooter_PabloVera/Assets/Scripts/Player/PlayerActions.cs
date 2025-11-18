using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public BulletPool poolProyectiles;
    public Transform escenario, cannon;
    public PlayerData playerData;
    public void Disparar(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Bullet bull = poolProyectiles.SacarDeLaPool();
            bull.gameObject.transform.SetParent(escenario);
            bull.initPos = cannon;
            bull.gameObject.SetActive(true);
            SoundMannager.Instance.PlaySFX(playerData.disparo);
        }
    }
}
