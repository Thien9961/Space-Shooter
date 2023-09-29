using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class Enviroment : MonoBehaviour
{
    public PoolManager pool;
    public static readonly int[] poolIndex = { 0, 1, 2 };
    public static Rect screenBound;
    public int maxAsteroid;
    // Start is called before the first frame update

    void Start()
    {
        screenBound= GameObject.Find("Canvas").GetComponent<Canvas>().pixelRect;
        pool = GameObject.Find("Pool Manager").GetComponent<PoolManager>();
        InvokeRepeating("Spawn", 1, 0.1f);
    }

    public List<Asteroid> GetActiveAsteroid()
    {
        List<Asteroid> l=new List<Asteroid> ();
        foreach (Transform t in transform)
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
                asteroidClone.GetComponent<Asteroid>().pool = pool.GetPool<Transform>(rng);
                Vector2 v2 = Camera.main.ScreenToWorldPoint(GameManager.RandomLocInRect(screenBound));
                Vector3 v3 = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0.002f);
                asteroidClone.GetComponent<Asteroid>().IntialState(v2, v3, Random.Range(-1f, 1f));
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
