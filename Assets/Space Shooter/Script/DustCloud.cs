using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DustCloud : Enviroment
{
    public static Transform cloudContainer;
    public int[] objectPool;
    public float spawnInterval, cloudSize;
    public static GameObject cloud;

    protected override void Start()
    {
        base.Start();
        cloudContainer = GetComponent<Transform>();
    }

    public override IEnumerator NaturalSpawn()
    {
        yield return new WaitForSeconds(Random.Range(0, spawnInterval));
                var rng = Random.Range(0, objectPool.Length);
                var cloudClone = pool.GetFromPool<Transform>(objectPool[rng]);
                if (cloudClone != null)
                {
                    Cloud c = cloudClone.GetComponent<Cloud>();
                    c.poolIndex = objectPool[rng];
                    Vector2 v1 = GameManager.RandomLocInRect(screenBound);
                    Vector2 v2 = Camera.main.ScreenToWorldPoint(new Vector3(v1.x, v1.y, 0));
                    c.transform.position = v2;
                    c.maxSize = cloudSize;
                    c.dustCloud=GetComponent<DustCloud>();
                    c.GetComponent<SpriteRenderer>().sortingOrder = 100;
                }
        
    }
}
