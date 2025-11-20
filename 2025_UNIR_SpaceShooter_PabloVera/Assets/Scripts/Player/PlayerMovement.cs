using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField]private float aceleration, maxSpeed, drag;
    private Vector2 dir, currentVelocity;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentVelocity = Vector2.zero;
    }

    public void Moverse(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        currentVelocity += dir * aceleration * Time.deltaTime;
        currentVelocity = Vector2.ClampMagnitude(currentVelocity, maxSpeed);
        rb2d.linearVelocity = currentVelocity * Time.deltaTime;
        currentVelocity *= drag;

        //rb2d.linearVelocity = dir.normalized * speed*Time.deltaTime;
    }
}
