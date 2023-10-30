using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using System.Linq;
using Utility;

public class Enviroment : MonoBehaviour
{
    public static PoolManager pool;
    [HideInInspector]
    public Rect spawnArea;
    public Vector2 areaScale= Vector2.one;
    public float maxInterval, minInterval;
    public static int layerOrder= 32767;

    // Start is called before the first frame update

    protected virtual void Start()
    {
        spawnArea = GameObject.Find("Canvas").GetComponent<Canvas>().pixelRect;
        Rec.Scale(ref spawnArea,areaScale.x,areaScale.y);
        pool = GameObject.Find("Pool Manager").GetComponent<PoolManager>();
        StartCoroutine(NaturalSpawn());
        Debug.Log("Env_layerOrder: " + layerOrder);
    }

    public virtual IEnumerator NaturalSpawn()
    {
        yield return null;
    }
}
