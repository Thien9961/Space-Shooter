using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using System.Linq;
using Utility;
using UnityEngine.SceneManagement;
using UnityEditor.Animations;

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
        //Debug.Log("Env_layerOrder: " + layerOrder);
    }

    public static void ClearAll()
    {
        GameObject cleaner = new GameObject("Cleaner");
        cleaner.AddComponent<Enviroment>().StopCoroutine(nameof(NaturalSpawn));
        GameObject[] list = SceneManager.GetActiveScene().GetRootGameObjects();
        var v = from GameObject g in list
                where g.GetComponent<Enviroment>() != null
                select g;
        foreach (GameObject go in v)
        {
            Debug.Log(go.name);
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if (child.TryGetComponent(out FlyingObject f) && child.GetComponent<Enviroment>() == null)
                {
                    DG.Tweening.DOTween.Kill(child.transform);
                    Destroy(child.gameObject);
                }
                    
            }
        }
        Destroy(cleaner);
    }

    public virtual IEnumerator NaturalSpawn()
    {
        yield return null;
    }
}
