using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Kamikaze : Enemigo
{
    public AudioClip SFX_Rush;
    private Collider2D cruceroEstelar;
    [SerializeField]private float waitForRush, rushSpeed, f, a;
    //public Vector3 lineRendOffSet;
    private Vector3 movDir;

    public void SetCruceroCollider(Collider2D c) { cruceroEstelar = c; }

    private void Start()
    {
    
        if (Random.Range(0, 2) > 0)
            movDir = Vector2.right;
        else
            movDir = Vector2.left;
    }

    public override void Movement()
    {
        //movDir = new Vector3 (movDir.x * Time.deltaTime, Mathf.Sin(Time.time * f) * a);
        transform.position += movDir * enemyData.movementSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y - Mathf.Sin(Time.time * f) * a, 0);

        RaycastHit2D rayRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, escenarioMask);
        RaycastHit2D rayLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, escenarioMask);

        if (rayRight.collider != null || rayLeft.collider != null)
            movDir *= Vector2.left;
    }

    public override void Shoot()
    {
        canShoot = false;
        StartCoroutine(Rush());
    }

    IEnumerator Rush()
    {
        SoundMannager.Instance.PlaySFX(SFX_Rush);
        Vector2 cruceroPos = new Vector2(Random.Range(cruceroEstelar.bounds.min.x, cruceroEstelar.bounds.max.x), Random.Range(cruceroEstelar.bounds.min.y, cruceroEstelar.bounds.max.y));
        //transform.localRotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(cruceroPos, -transform.up));
        //transform.localRotation = Quaternion.AngleAxis(Vector2.SignedAngle(cruceroPos, -transform.up), new Vector3(0,0,1f));
        //transform.localRotation =  Quaternion.LookRotation(cruceroPos, transform.up);
        yield return new WaitForSeconds(waitForRush);

        while (Vector2.Distance(transform.position, cruceroPos) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, cruceroPos, enemyData.movementSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.TryGetComponent<Player>(out var naveJugador))
            {
                naveJugador.TakeDMG(enemyData.dmg);
                StopAllCoroutines();
                StartCoroutine(Die());
            }
            else if (collision.gameObject.TryGetComponent<CruceroEstelar>(out var crucero))
            {
                crucero.TakeDMG(enemyData.dmg);
                StopAllCoroutines();
                StartCoroutine(Die());
            }
        }
        
    }
}
