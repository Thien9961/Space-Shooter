using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using System.Linq;


public class Enviroment : MonoBehaviour
{
    public static PoolManager pool;
    public static Rect screenBound;

    // Start is called before the first frame update

    protected virtual void Start()
    {
        screenBound = GameObject.Find("Canvas").GetComponent<Canvas>().pixelRect;
        pool = GameObject.Find("Pool Manager").GetComponent<PoolManager>();
    }

    public virtual IEnumerator NaturalSpawn()
    {
        yield return null;
    }
}
