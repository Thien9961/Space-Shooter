using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using Utility;

public class Asteroid : FlyingObject
{
    public PowerUp[] powerUps;
    public float damage;
    protected override void Move()
    {
        if (Mathf.Abs(transform.localScale.x) < maxSize)
            transform.localScale += Vector3.one * speed+ Vector3.one * GameManager.player.speed;
        else
        {     
            if (transform.parent.GetComponent<AsteroidField>().spawnArea.Contains(Camera.main.WorldToScreenPoint(transform.position)) && GameManager.player!=null)
                GameManager.player.TakeDamage(gameObject,damage);
            Death(null);
        }     
    }

    public override void Death(GameObject killer)
    {
        GameManager.PlaySfx(onDeathSfx, transform.position);
        AsteroidField.pool.TakeToPool(poolIndex, transform);
        if (killer!=null)
        {
            if (onDeathVfx != null)
                Instantiate(onDeathVfx, transform.position, onDeathVfx.transform.rotation).transform.localScale = transform.localScale;
            foreach (PowerUp p in powerUps)
            {
                if (Random.value * 100 <= p.appearChance)
                {
                    PowerUp pu = Enviroment.pool.GetFromPool<RectTransform>(p.poolIndex).GetComponent<PowerUp>();
                    Vector2 v = Camera.main.WorldToScreenPoint(transform.position);
                    if (pu.TryGetComponent(out MineralBonus mb) && killer.TryGetComponent(out Ship receipent))
                    {
                        mb.Init(v, killer.GetComponent<Ship>());
                        mb.Begin();
                    }
                    if (pu.TryGetComponent(out SuperSpeed ss) && killer.TryGetComponent(out Ship user))
                    {
                        ss.Init(v, user);
                        ss.Begin();
                    }
                    if (pu.TryGetComponent(out Shield shield) && killer.TryGetComponent(out Ship target))
                    {
                        shield.Init(v,target);
                        shield .Begin();
                    }
                    if (pu.TryGetComponent(out Spilt spilt))
                    {

                        Vector3[] arr1 = new Vector3[spilt.qty];
                        Vector2[] arr2 = new Vector2[spilt.qty];
                        for (int i = 0; i < arr1.Length; i++)
                        {
                            arr1[i] = transform.localScale;
                            arr2[i] = Vec.RandomInCircle(new Circle(v, 20));
                        }
                        spilt.Init(v, GetComponent<FlyingObject>(), arr1, arr2);
                        spilt.Begin();
                    }

                }

            }
        }

    }
}
