using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float ttl, baseSpeed;
    protected int dmg;
    protected Vector2 dir;
    protected float currentTtl;
    protected Transform initPos;
    protected BulletPool pool;
    protected Rigidbody2D rb2d;
    protected float speed;

    public void SetBulletPool(BulletPool Bpool) { pool = Bpool; }

    public void IncreaseSpeed(float increment)
    {
        speed = Mathf.Clamp(speed+increment, 0, baseSpeed + increment*5);
    }

    private void Start()
    {
        speed = baseSpeed;
    }

    public void Init(Vector2 aim, Transform initPosition, Transform newParent, int bulletDMG)
    {
        initPos = initPosition;
        transform.position = initPos.position;
        transform.SetParent(newParent);
        currentTtl = ttl;
        rb2d = GetComponent<Rigidbody2D>();
        dir = aim;
        gameObject.SetActive(true);
        dmg = bulletDMG;
    }

    private void FixedUpdate()
    {
        currentTtl -= Time.deltaTime;
        if (currentTtl < 0)
            Desactivar();

        //transform.position += dir.normalized * speed*Time.deltaTime;
        rb2d.MovePosition(rb2d.position + dir.normalized * speed * Time.deltaTime);
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
        transform.position = initPos.position;
        currentTtl = ttl;
        pool.MeterEnLaPool(this);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.TryGetComponent<Enemigo>(out var target))
            {
                target.TakeDMG(dmg);
                Desactivar();
            }
        }
    }
}
