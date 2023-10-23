using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Asteroid : FlyingObject
{
    public PowerUp[] powerUps; 
    protected override void Move()
    {
        if (Mathf.Abs(transform.localScale.x) < maxSize)
            transform.localScale += Vector3.one * speed+ Vector3.one * GameManager.player.speed;
        else
        {     
            if (Enviroment.screenBound.Contains(Camera.main.WorldToScreenPoint(transform.position)) && GameManager.player!=null)
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
                PowerUp pu=Enviroment.pool.GetFromPool<RectTransform>(p.poolIndex).GetComponent<PowerUp>();
                if(!pu.autoCollect)
                    pu.transform.position=Camera.main.WorldToScreenPoint(transform.position);
                if (pu.TryGetComponent(out MineralBonus mb) && killer.TryGetComponent(out Ship receipent))
                    mb.recipient = receipent;
                if (pu.TryGetComponent(out SuperSpeed ss) && killer.TryGetComponent(out Ship user))
                    ss.user = user;
            }
            
    }
}
