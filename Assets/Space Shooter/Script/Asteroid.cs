using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Asteroid : Destrucible
{
    public float speed = 0;
    public IPool<Transform> pool;
    public int mineralBonus;
    void Move()
    {
        if (Mathf.Abs(transform.localScale.x) < 3.5)
            transform.localScale += Vector3.one * speed;
        else
        {
            Death(null);
            if (Enviroment.screenBound.Contains(Camera.main.WorldToScreenPoint(transform.position)) && GameManager.player!=null)
                GameManager.player.TakeDamage(gameObject,10);
        }
            
            
    }

    public override void Death(GameObject killer)
    {
        base.Death(killer);
        pool.Take(GetComponent<Transform>());
        if (killer != null && killer.GetComponent<Ship>() != null )
            killer.GetComponent<Ship>().mineral += mineralBonus;

    }
    public void IntialState(Vector2 position,Vector3 velocity,float torque,Vector3 distance)
    {
        transform.position = position;
        transform.localScale = distance;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddTorque(torque);
        rb.velocity = new Vector2(velocity.x, velocity.y);
        speed = velocity.z;
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
    }
}
