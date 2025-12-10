using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 dir, currentVelocity;
    public PlayerData playerData;

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
        currentVelocity += dir * playerData.acceleration * Time.deltaTime;
        currentVelocity = Vector2.ClampMagnitude(currentVelocity, playerData.maxSpeed);
        rb2d.linearVelocity = currentVelocity * Time.deltaTime;
        if(dir.x <= 0.1 && dir.y <= 0.1)
            currentVelocity *= playerData.speedDrag;

        //rb2d.linearVelocity = dir.normalized * currentVelocity * Time.deltaTime;
    }
}
