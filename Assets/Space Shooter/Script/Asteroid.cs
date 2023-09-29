using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Asteroid : Destrucible
{
    public float speed=0, amplification=1;
    public IPool<Transform> pool;
    void Move()
    {
        if (transform.localScale.x < 5)
            transform.localScale += Vector3.one * speed;
        else
        {
            pool.Take(GetComponent<Transform>());
            if (Enviroment.screenBound.Contains(Camera.main.WorldToScreenPoint(transform.position)))
                Debug.Log($"Player tooks {5} damages from {name}");
            else
                Debug.Log($"Player was not take damage from {name}");
            //GameManager.player.TakeDamage(10);
        }
            
    }

    public void IntialState(Vector2 position,Vector3 velocity,float torque)
    {
        transform.position = position;
        transform.localScale = Vector3.zero;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddTorque(torque);
        rb.velocity = new Vector2(velocity.x, velocity.y);
        speed = velocity.z;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        Move();
    }
}
