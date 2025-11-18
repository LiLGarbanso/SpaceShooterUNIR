using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField]private float movSpeed;
    private Vector2 dir;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Moverse(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb2d.linearVelocity = dir.normalized * movSpeed;
    }
}
