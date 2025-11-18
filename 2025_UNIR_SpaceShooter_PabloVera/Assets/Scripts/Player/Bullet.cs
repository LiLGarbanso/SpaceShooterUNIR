using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float ttl, speed;
    [SerializeField] private int dmg;
    [SerializeField] private Vector2 dir;
    private float currentTtl;
    public Transform initPos;
    private BulletPool pool;
    private Rigidbody2D rb2d;

    public void SetBulletPool(BulletPool Bpool) { pool = Bpool; }

    private void Start()
    {
        transform.position = initPos.position;
        currentTtl = ttl;
        rb2d = GetComponent<Rigidbody2D>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.TryGetComponent<Entidad>(out var target))
            {
                target.TakeDMG(dmg);
            }
            Desactivar();
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider != null)
    //    {
    //        if (collision.collider.gameObject.TryGetComponent<Entidad>(out var target))
    //        {
    //            target.TakeDMG(dmg);
    //        }
    //        Desactivar();
    //    }
    //}
}
