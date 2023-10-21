using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DustCloud : Enviroment
{
    public static Transform cloudContainer;
    public int[] objectPool;
    public float spawnInterval, cloudSize;
    public static Cloud cloud;

    protected override void Start()
    {
        base.Start();
        cloudContainer = GetComponent<Transform>();
    }

    public override IEnumerator NaturalSpawn()
    {
        yield return new WaitForSeconds(spawnInterval);
                var rng = Random.Range(0, objectPool.Length);
                var cloudClone = pool.GetFromPool<Transform>(objectPool[rng]);
                cloud = cloudClone.GetComponent<Cloud>();
                cloud.poolIndex = objectPool[rng];
                Vector2 v1 = GameManager.RandomLocInRect(screenBound);
                Vector2 v2 = Camera.main.ScreenToWorldPoint(new Vector3(v1.x, v1.y, 0));
                cloud.maxSize = cloudSize;
                cloud.dustCloud=GetComponent<DustCloud>();
                cloud.GetComponent<SpriteRenderer>().sortingOrder = 100;
                cloud.IntialState(v2,Vector3.zero,0,Vector3.zero,cloud.maxHp,Color.white);
    }
}
