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
    protected override void Move()
    {
        if (Mathf.Abs(transform.localScale.x) < maxSize)
            transform.localScale += Vector3.one * speed+ Vector3.one * GameManager.player.speed;
        else
        {     
            if (transform.parent.GetComponent<AsteroidField>().spawnArea.Contains(Camera.main.WorldToScreenPoint(transform.position)) && GameManager.player!=null)
                GameManager.player.TakeDamage(gameObject,10);
            Death(null);
        }     
    }

    public override void Death(GameObject killer)
    {
        base.Death(killer);
        if(killer!=null)
            foreach(PowerUp p in powerUps)
            {
                if (Random.value*100 <= p.appearChance)
                {
                    PowerUp pu = Enviroment.pool.GetFromPool<RectTransform>(p.poolIndex).GetComponent<PowerUp>();
                    Vector2 v=Camera.main.WorldToScreenPoint(transform.position);
                    Debug.Log("Screen pos:" +v);
                    if (pu.TryGetComponent(out MineralBonus mb) && killer.TryGetComponent(out Ship receipent))
                    {
                        mb.Init(v, killer.GetComponent<Ship>());
                        mb.Begin();
                    }
                    if (pu.TryGetComponent(out SuperSpeed ss) && killer.TryGetComponent(out Ship user))
                    {
                        ss.Init(v, killer.GetComponent<Ship>());
                        ss.Begin();
                    }
                        
                    if (pu.TryGetComponent(out Spilt spilt))
                    {
                        
                        Vector3[] arr1 = new Vector3[spilt.qty];
                        Vector2[] arr2 = new Vector2[spilt.qty];
                        for (int i = 0; i < arr1.Length; i++)
                        {
                            arr1[i] = transform.localScale;
                            arr2[i] = Vec.RandomInCircle(new Circle(v,20));
                        }
                        spilt.Init(v, GetComponent<FlyingObject>(), arr1,arr2);
                        spilt.Begin();
                    }
                        
                }
                
            }
            
    }
}
