using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Victoria : MonoBehaviour
{
    public string escena;
    public void VolverAlMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene(escena);
        }
    }
}
