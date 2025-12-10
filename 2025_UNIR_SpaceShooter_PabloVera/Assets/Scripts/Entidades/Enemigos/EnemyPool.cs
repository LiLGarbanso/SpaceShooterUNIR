using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public Queue<Enemigo> pool;
    public Enemigo prefabEnemigo;
    [SerializeField] private int poolSize;
    public Transform poolTransform;

    private void Awake()
    {
        pool = new Queue<Enemigo>();
        for (int i = 0; i < poolSize; i++)
        {
            CreatePoolObject();
        }
    }

    public Enemigo SacarDeLaPool()
    {
        Enemigo e = pool?.Dequeue();
        if (e != null)
            return e;
        else
        {
            poolSize++;
            return CreatePoolObject();
        }
    }

    public void MeterEnLaPool(Enemigo e)
    {
        pool.Enqueue(e);
        e.transform.SetParent(poolTransform);
    }

    public Enemigo CreatePoolObject()
    {
        Enemigo e = Instantiate(prefabEnemigo, poolTransform.position, Quaternion.identity);
        e.transform.SetParent(poolTransform);
        e.SetEnemyPool(this);
        e.gameObject.SetActive(false);
        pool.Enqueue(e);
        return e;
    }
}
