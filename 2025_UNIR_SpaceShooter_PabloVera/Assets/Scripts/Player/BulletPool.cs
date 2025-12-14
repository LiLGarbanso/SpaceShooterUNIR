using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public Queue<Bullet> pool;
    public Bullet prefabBullet;
    [SerializeField]private int poolSize;
    public Transform poolTransform;

    private void Awake()
    {
        pool = new Queue<Bullet>();
        for (int i = 0; i < poolSize; i++)
        {
            CreatePoolObject();
        }
    }

    //---------MEJORAS--------------------------------//
    public void UpgradeBulletSpeed(float increment)
    {
        foreach(Bullet bullet in pool)
        {
            bullet.IncreaseSpeed(increment);
        }
    }
    //-----------------------------------------------//

    public Bullet SacarDeLaPool()
    {
        Bullet bull = pool?.Dequeue();
        if (bull != null)
            return bull;
        else
        {
            //poolSize++;
            //return CreatePoolObject();
            return null;
        }
    }

    public void MeterEnLaPool(Bullet bull)
    {
        pool.Enqueue(bull);
        bull.transform.SetParent(poolTransform);
    }

    public Bullet CreatePoolObject()
    {
        Bullet bull = Instantiate(prefabBullet, poolTransform);
        bull.SetBulletPool(this);
        bull.gameObject.SetActive(false);
        pool.Enqueue(bull);
        return bull;
    }
}
