using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float ttl;
    protected int dmg;
    protected Vector2 dir;
    protected float currentTtl;
    protected Transform initPos;
    protected BulletPool pool;
    protected Rigidbody2D rb2d;
    protected float speed;

    private void OnEnable()
    {
        enabled = true;
    }

    private void OnDisable()
    {
        enabled = false;
    }
    public void SetBulletPool(BulletPool Bpool) { pool = Bpool; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 aim, Transform initPosition, Transform newParent, int bulletDMG, float bulletSpeed)
    {
        initPos = initPosition;
        speed = bulletSpeed;
        transform.position = initPos.position;
        transform.SetParent(newParent);
        currentTtl = ttl;
        dir = aim;
        dmg = bulletDMG;
        gameObject.SetActive(true);
        enabled = true;
    }

    private void FixedUpdate()
    {
        currentTtl -= Time.deltaTime;
        if (currentTtl < 0)
            Desactivar();

        //transform.position += dir.normalized * speed*Time.deltaTime;
        rb2d.MovePosition(rb2d.position + dir.normalized * speed * Time.fixedDeltaTime);
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
        transform.position = initPos.position;
        currentTtl = 0;
        pool.MeterEnLaPool(this);
        enabled = false;
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
            else if (collision.gameObject.CompareTag("tope"))
            {
                Desactivar();
            }
        }
    }
}
