using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 dir, currentVelocity;
    public PlayerData playerData;
    public float currentAcceleration, currrentMaxSpeed;
    private int timesSpeedIncreased, timesAccelerationIncreased;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentVelocity = Vector2.zero;
        currentAcceleration = playerData.acceleration;
        currrentMaxSpeed = playerData.maxSpeed;
        timesAccelerationIncreased = 0;
        timesSpeedIncreased = 0;
    }

    //----------MEJORAS------------------------------------//

    public void IncreaseMaxSpeed(float increment)
    {
        //Solo se puede mejorar hasta 5 veces la velocidad
        currrentMaxSpeed = Mathf.Clamp(currrentMaxSpeed +  increment, 0, playerData.maxSpeed + increment*5);
        timesSpeedIncreased++;
    }
    public int GetTimesSpeedIncreased() { return  timesSpeedIncreased; }

    public void IncreaseAcceleration(float increment)
    {
        //Solo se puede mejorar hasta 5 veces la velocidad
        currentAcceleration = Mathf.Clamp(currentAcceleration + increment, 0, playerData.acceleration + increment * 5);
        timesAccelerationIncreased++;
    }
    public int GetTimesAccelerationIncreased() { return timesAccelerationIncreased; }

    //-----------------------------------------------------//

    public void Moverse(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        currentVelocity += dir * currentAcceleration * Time.deltaTime;
        currentVelocity = Vector2.ClampMagnitude(currentVelocity, currrentMaxSpeed);
        rb2d.linearVelocity = currentVelocity * Time.deltaTime;
        if(dir.x <= 0.1 && dir.y <= 0.1)
            currentVelocity *= playerData.speedDrag;

        //rb2d.linearVelocity = dir.normalized * currentVelocity * Time.deltaTime;
    }
}
