using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class Enviroment : MonoBehaviour
{
    public static PoolManager pool;
    public static readonly int[] poolIndex = { 0, 1,2,3 };
    public static Transform poolContainer;
    public static Rect screenBound;
    public int maxAsteroid;
    public Vector2 spawnInterval;
    public Vector3 asteroidMaxVelocity;
    public float asteroidMaxTorque;
    // Start is called before the first frame update

    void Start()
    {
        poolContainer = transform;
        screenBound = GameObject.Find("Canvas").GetComponent<Canvas>().pixelRect;
        pool = GameObject.Find("Pool Manager").GetComponent<PoolManager>();
        
    }



    public static List<Asteroid> GetActiveAsteroid()
    {
        List<Asteroid> l=new List<Asteroid> ();
        foreach (Transform t in poolContainer)
            if (t.gameObject.activeSelf)
                l.Add(t.GetComponent<Asteroid>());
        return l;
    }
    Asteroid Spawn()
    {
        if (GetActiveAsteroid().Count < maxAsteroid)
        {
            var rng = Random.Range(0, poolIndex.Length);
            var asteroidClone = pool.GetFromPool<Transform>(rng);
            if(asteroidClone!= null)
            {
                Asteroid a = asteroidClone.GetComponent<Asteroid>();
                a.pool = pool.GetPool<Transform>(rng);
                Vector2 v1 = GameManager.RandomLocInRect(screenBound);
                Vector2 v2 = Camera.main.ScreenToWorldPoint(new Vector3(v1.x,v1.y,0));
                Vector3 v3 = new Vector3(Random.Range(-asteroidMaxVelocity.x, asteroidMaxVelocity.x), Random.Range(-asteroidMaxVelocity.y, asteroidMaxVelocity.y), Random.Range(-asteroidMaxVelocity.z, asteroidMaxVelocity.z));
                a.IntialState(v2, v3, Random.Range(-asteroidMaxTorque, asteroidMaxTorque));
                return asteroidClone.GetComponent<Asteroid>();
            } 
        }
        return null;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
