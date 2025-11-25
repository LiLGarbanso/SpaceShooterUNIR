using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private string nivel;
    public void EmpezarPartida(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene(nivel);
        }
    }
}
