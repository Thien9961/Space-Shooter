using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AsteroidField : Enviroment
{
    public int asteroidLimit, maxAsteroid;
    public float spawnInterval, asteroidMaxTorque, asteroidSize;
    public Vector3 asteroidMaxVelocity;
    public static Transform asteroidContainer;
    public int[] objectPool;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        asteroidContainer = GetComponent<Transform>();
    }

    public static List<Asteroid> GetActiveAsteroid()
    {
        List<Asteroid> l = new List<Asteroid>();
        foreach (Transform t in asteroidContainer)
            if (t.gameObject.activeSelf)
                l.Add(t.GetComponent<Asteroid>());
        return l;
    }
    public override IEnumerator NaturalSpawn()
    {
        yield return new WaitForSeconds(Random.Range(0, spawnInterval));
        var max = Random.Range(1, maxAsteroid);
        for (int i = 0; i < max; i++)
        {
            if (GetActiveAsteroid().Count < asteroidLimit)
            {
                
                int rng = Random.Range(0, objectPool.Length);
                var asteroidClone = pool.GetFromPool<Transform>(objectPool[rng]);
                if (asteroidClone != null)
                {
                    Asteroid a = asteroidClone.GetComponent<Asteroid>();
                    a.poolIndex = objectPool[rng];
                    a.maxSize = asteroidSize;
                    Vector2 v1 = GameManager.RandomLocInRect(screenBound);
                    Vector2 v2 = Camera.main.ScreenToWorldPoint(new Vector3(v1.x, v1.y, 0));
                    Vector3 v3 = new Vector3(Random.Range(-asteroidMaxVelocity.x, asteroidMaxVelocity.x), Random.Range(-asteroidMaxVelocity.y, asteroidMaxVelocity.y), Random.Range(-asteroidMaxVelocity.z, asteroidMaxVelocity.z));
                    a.IntialState(v2, v3, Random.Range(-asteroidMaxTorque, asteroidMaxTorque), Vector3.zero);
                }
            }
        }
        StartCoroutine(NaturalSpawn());
    }
}
