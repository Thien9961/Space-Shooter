using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Asteroid : FlyingObject
{
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
