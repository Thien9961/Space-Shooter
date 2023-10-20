using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : Destrucible
{
    public float speed = 0,maxSize;
    public int poolIndex;
    protected virtual void Move()
    {
        if (Mathf.Abs(transform.localScale.x) < maxSize)
            transform.localScale += Vector3.one * speed+Vector3.one*GameManager.player.speed;
        else
            Death(null);
    }

    public override void Death(GameObject killer)
    {
        base.Death(killer);
        AsteroidField.pool.TakeToPool(poolIndex,transform);
    }
}
